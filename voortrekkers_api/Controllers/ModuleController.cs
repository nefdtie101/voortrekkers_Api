using DataBaseModles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repos.Interface;

namespace voortrekkers_api.Controllers;

public class ModuleController : Controller
{
   private readonly IModuleRepo _moduleRepo;
   public ModuleController(IModuleRepo moduleRepo)
   {
      _moduleRepo = moduleRepo;
   }

   [Authorize]
   [HttpGet]
   [Route("GetAllModules")]
   public IActionResult GetAllModules()
   {
      return Ok(_moduleRepo.GetAllModules());
   }
   
   [Authorize]
   [HttpPost]
   [Route("AddModule")]
   public IActionResult AddModule([FromBody] ModuleModel module)
   {
      return Ok(_moduleRepo.CreateModule(module));
   }
   
   [Authorize]
   [HttpPut]
   [Route("EditModule")]
   public IActionResult EditModules([FromBody] ModuleModel module)
   {
      return Ok(_moduleRepo.EditModule(module));
   }
   
   [Authorize]
   [HttpPut]
   [Route("DeleteModule")]
   public IActionResult DeleteModule([FromBody] ModuleModel module)
   {
      return Ok(_moduleRepo.Delete(module));
   }
   
   
}