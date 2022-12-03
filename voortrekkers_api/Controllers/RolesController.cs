using DataBaseModles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repos.Interface;

namespace voortrekkers_api.Controllers;

public class RolesController : Controller
{

    private readonly IRoleRepo _rolesRepo;
    public RolesController(IRoleRepo rolesRepo)
    {
        _rolesRepo = rolesRepo;
    }
    
    [Authorize]
    [HttpGet]
    [Route("GetAllRoles")]
    public IActionResult GetAllRoles()
    {
        return Ok(_rolesRepo.GetAllRoles());
    }
    
    [Authorize]
    [HttpPost]
    [Route("AddRoles")]
    public IActionResult AddRoles([FromBody] RoleModel roles)
    {
        return Ok(_rolesRepo.CreateRole(roles));
    }
    
    [Authorize]
    [HttpPut]
    [Route("EditRole")]
    public IActionResult EditRole([FromBody] RoleModel roles)
    {
        return Ok(_rolesRepo.EditRole(roles));
    }
    
    
    [Authorize]
    [HttpPut]
    [Route("DeleteRoles")]
    public IActionResult DeleteRoles([FromBody] RoleModel roles)
    {
        return Ok(_rolesRepo.Delete(roles));
    }
}