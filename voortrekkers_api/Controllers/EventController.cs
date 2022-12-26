using DataBaseModles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repos.Interface;

namespace voortrekkers_api.Controllers;

public class EventController : Controller
{
    IEventRepo _eventRepo;
    public EventController(IEventRepo eventRepo)
    {
        _eventRepo = eventRepo;
    }
    
    [Authorize]
    [HttpGet]
    [Route("GetAllEvents")]
    public IActionResult GetAllEvents()
    {
        return Ok(_eventRepo.GetAllEvents());
    }

    [Authorize]
    [HttpPost]
    [Route("AddEvent")]
    public IActionResult AddOrganization([FromBody] EventModel events)
    {
        return Ok(_eventRepo.CreateEvents(events));
    }
    
    [Authorize]
    [HttpPut]
    [Route("EditEvent")]
    public IActionResult EditEvent([FromBody] EventModel events)
    {
        return Ok(_eventRepo.EditEvent(events));
    }
    
    [Authorize]
    [HttpPut]
    [Route("DeleteEvent")]
    public IActionResult DeleteModule([FromBody] EventModel events)
    {
        return Ok(_eventRepo.Delete(events)); 
    }



}


