using DataBaseModles;

namespace Repos.Interface;

public interface IEventRepo
{
    public dynamic GetAllEvents();
    public bool CreateEvents(EventModel eModel);

    public bool EditEvent(EventModel eModel);

    public bool Delete(EventModel eModel);
}