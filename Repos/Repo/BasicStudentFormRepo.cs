using DataBaseModles;
using DataBaseModles.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using Repos.Interface;

namespace Repos.Repo;

public class BasicStudentFormRepo : IBasicStudentFormRepo
{
    private IEmailRepo _emailList;
    private IEmailHelper _emailHelper;
    private IEventRepo _event;
    private readonly IMongoCollection<BasicStudentForm> _BasicFormCollection;
    
    public BasicStudentFormRepo(IMongoClient client, IEmailRepo emailRepo , IEmailHelper emailHelper, IEventRepo events)
    {
        var database = client.GetDatabase("Voories");
        _BasicFormCollection = database.GetCollection<BasicStudentForm>("BasicStudentForm");
        _emailList = emailRepo;
        _emailHelper = emailHelper;
        _event = events;
    }
    
    public dynamic GetFormByEventId(string eventId)
    {
        var forms = _BasicFormCollection.Find(o => o.IdEvent == eventId)
            .Sort(Builders<BasicStudentForm>.Sort.Ascending(x => x.Koemandoe).Ascending(x =>x.Surname)).ToList();
        if (forms != null)
        {
            return forms;
        }

        return false;
    }
    public bool MarkAsPaid(string id)
    {
        try
        {
            var form = _BasicFormCollection.Find(o => o.IdBasicStudentForm == id).ToList();
            var Event = _event.GetEventNameByEventId(form[0].IdEvent);
            var filter = Builders<BasicStudentForm>.Filter.Eq("IdBasicStudentForm",  id);
            var update = Builders<BasicStudentForm>.Update
                .Set("Paid", true);
            _BasicFormCollection.UpdateOne(filter, update);
            _emailHelper.MarkAsPaid(form[0].EMail, Event);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }

    public bool MarkAsAttended(string id)
    {
        try
        {
            var filter = Builders<BasicStudentForm>.Filter.Eq("IdBasicStudentForm",  id);
            var update = Builders<BasicStudentForm>.Update
                .Set("Attended", true);
            _BasicFormCollection.UpdateOne(filter, update);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
    
    public bool FillInForm(BasicStudentForm form)
    {
        try
        {
            var newForm = new BasicStudentForm()
            {
                IdBasicStudentForm  = ObjectId.GenerateNewId().ToString(),
                Name = form.Name,
                Surname = form.Surname,
                EMail = form.EMail,
                IdEvent = form.IdEvent,
                Koemandoe = form.Koemandoe,
                Graad = form.Graad,
                mobileNumber = form.mobileNumber
                
            };
            _BasicFormCollection.InsertOne(newForm);
            
            // Add to Email List
            var newEmail = new EmailModel()
            {
                IdEmail = ObjectId.GenerateNewId().ToString(),
                Name = form.Name,
                Surname = form.Surname,
                Staatmaker = false,
                Email = form.EMail
            };
            _emailList.AddToEmailList(newEmail);
            var Event = _event.GetEventNameByEventId(newForm.IdEvent);
            var message = _event.GetEventNameByEventId(newForm.IdEvent);
            _emailHelper.InskrywingOntvang(newForm.EMail, Event, message);
            return true;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    
}