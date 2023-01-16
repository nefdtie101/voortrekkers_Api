using DataBaseModles;
using DataBaseModles.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using Repos.Interface;

namespace Repos.Repo;

public class BasicFormRepo : IBasicFormRepo
{
    private IEmailRepo _emailList;
    private IEmailHelper _emailHelper;
    private IEventRepo _event;
    private readonly IMongoCollection<BasicForm> _BasicFormCollection;


    public BasicFormRepo(IMongoClient client, IEmailRepo emailRepo, IEmailHelper emailHelper, IEventRepo events )
    {
        var database = client.GetDatabase("Voories");
        _BasicFormCollection = database.GetCollection<BasicForm>("BasicForm");
        _emailList = emailRepo;
        _emailHelper = emailHelper;
        _event = events;
    }
    
    public dynamic GetFormByEventId(string eventId)
    {
        var sort = Builders<BasicForm>.Sort.Ascending("Surname");
        var forms = _BasicFormCollection.Find(o => o.IdEvent == eventId).SortBy(i => i.Surname ).ToList();
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
            var form = _BasicFormCollection.Find(o => o.IdBasicForm == id).ToList();
            var Event = _event.GetEventNameByEventId(form[0].IdEvent);
            var filter = Builders<BasicForm>.Filter.Eq("IdBasicForm",  id);
            var update = Builders<BasicForm>.Update
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
            var filter = Builders<BasicForm>.Filter.Eq("IdBasicForm",  id);
            var update = Builders<BasicForm>.Update
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
    
    public bool FillInForm(BasicForm form)
    {
        try
        {
            var newForm = new BasicForm()
            {
                IdBasicForm = ObjectId.GenerateNewId().ToString(),
                Name = form.Name,
                Surname = form.Surname,
                EMail = form.EMail,
                idOrganization = form.idOrganization,
                IdEvent = form.IdEvent
            };
            _BasicFormCollection.InsertOne(newForm);
             
            // Add to Email List
            var newEmail = new EmailModel()
            {
                IdEmail = ObjectId.GenerateNewId().ToString(),
                Name = form.Name,
                Surname = form.Surname,
                Staatmaker = true,
                Email = form.EMail
            };
            var list = _emailList.AddToEmailList(newEmail);
            // Mark As Staatmaaker 
            if (list == false)
            {
                _emailList.MarkAsStaatmker(newForm.EMail);
            }
            var Event = _event.GetEventNameByEventId(newForm.IdEvent);
            var message = _event.GetEventMessageByEventId(newForm.IdEvent);
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