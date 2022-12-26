using Microsoft.AspNetCore.Mvc;
using Repos.Interface;


public class MenuController : Controller
{
    private IOrganizationRepo _OrganizationRepo;

    public MenuController(IOrganizationRepo organizationRepo)
    {
        _OrganizationRepo = organizationRepo;
    }


    [HttpGet]
    [Route("GetAllGroups")]
    public IActionResult GetAllGroups()
    {
        return Ok(_OrganizationRepo.GetAllOrganizations());
    }
}