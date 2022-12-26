using DataBaseModles.Forms;

namespace Repos.Interface;

public interface IBasicStudentFormRepo
{
    public dynamic GetFormByEventId(string eventId);
    public bool MarkAsPaid(string id);
    public bool MarkAsAttended(string id);
    public bool FillInForm(BasicStudentForm form);
}