using DataBaseModles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repos.Interface;

namespace voortrekkers_api.Controllers;

public class OrganizationController : Controller
{
    private IOrganizationRepo _OrganizationRepo;

    public OrganizationController(IOrganizationRepo organizationRepo)
    {
        _OrganizationRepo = organizationRepo;
    }
    
    [Authorize]
    [HttpGet]
    [Route("GetAllOrganizations")]
    public IActionResult GetAllOrganizations()
    {
        return Ok(_OrganizationRepo.GetAllOrganizations());
    }

    [Authorize]
    [HttpPost]
    [Route("AddOrganization")]
    public IActionResult AddOrganization([FromBody] Organization organization)
    {
        return Ok(_OrganizationRepo.CreateOrganization(organization));
    }
    
    [Authorize]
    [HttpPut]
    [Route("EditOrganization")]
    public IActionResult EditOrganization([FromBody] Organization organization)
    {
        return Ok(_OrganizationRepo.EditOrganization(organization));
    }
    
    [Authorize]
    [HttpPut]
    [Route("DeleteOrganization")]
    public IActionResult DeleteModule([FromBody] Organization organization)
    {
        return Ok(_OrganizationRepo.Delete(organization));
    }

    
}