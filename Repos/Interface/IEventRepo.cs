using DataBaseModles;

namespace Repos.Interface;

public interface IEventRepo
{
    public dynamic GetAllEvents();
    public bool CreateEvents(EventModel eModel);
    public bool EditEvent(EventModel eModel);
    public bool Delete(EventModel eModel);
    public dynamic GetEventByOrg(string idOrg);
    public dynamic GetAllGebiedAksies();
    public dynamic GetAllSpesialeAksies();
    public dynamic GetAllKampe();
    public dynamic GetAllKomitees();
    public dynamic GetAllInteneAksies();
    public string GetEventNameByEventId(string id);
}