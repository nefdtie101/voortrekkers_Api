using Microsoft.AspNetCore.Mvc;
using Repos.Interface;


public class GroepeController : Controller
{
   private IOrganizationRepo _OrganizationRepo;
   private IEventRepo _eventRepo;
   
   public GroepeController(IOrganizationRepo organizationRepo , IEventRepo  eventRepo)
   {
      _OrganizationRepo = organizationRepo;
      _eventRepo = eventRepo; 
   }
   
   
   [HttpGet]
   [Route("GetAllGroupsById")]
   public IActionResult GetAllGroups(string id)
   {
      return Ok(_OrganizationRepo.GetAllOrganizationById(id));
   }
   
   [HttpGet]
   [Route("GetAllEventsByGroupID")]
   public IActionResult GetAllEventsByGroupId(string id)
   {
      return Ok(_eventRepo.GetEventByOrg(id));
   }

   [HttpGet]
   [Route("GetAllGebiedAksies")]
   public IActionResult GetAllGebiedAksies()
   {
      return Ok(_eventRepo.GetAllGebiedAksies());
   }
   
   [HttpGet]
   [Route("GetAllSpesialeAksies")]
   public IActionResult GetAllSpesialeAksies()
   {
      return Ok(_eventRepo.GetAllSpesialeAksies());
   }
   
   [HttpGet]
   [Route("GetAllKampe")]
   public IActionResult GetAllKampe()
   {
      return Ok(_eventRepo.GetAllKampe());
   }
   
   [HttpGet]
   [Route("GetAllKomitees")]
   public IActionResult GetAllKomitees()
   {
      return Ok(_eventRepo.GetAllKomitees());
   }
   
   [HttpGet]
   [Route("GetAllInteneAksies")]
   public IActionResult GetAllInteneAksies()
   {
      return Ok(_eventRepo.GetAllInteneAksies());
   }
   
   
}