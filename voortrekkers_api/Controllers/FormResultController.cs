using DataBaseModles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repos.Interface;

namespace voortrekkers_api.Controllers;

public class FormResultController : Controller
{
    private IBasicFormRepo _basicFormRepo;
    private IBasicStudentFormRepo _basicStudentFormRepo;

    public FormResultController(IBasicFormRepo basicFormRepo ,IBasicStudentFormRepo basicStudentFormRepo)
    {
        _basicFormRepo = basicFormRepo;
        _basicStudentFormRepo = basicStudentFormRepo;
    }
    
    [Authorize]
    [HttpGet]
    [Route("GetFormByEventId")]
    public IActionResult GetFormByEventId(string id)
    {
        return Ok(_basicFormRepo.GetFormByEventId(id));
    }
    
    [Authorize]
    [HttpGet]
    [Route("GetStudentFormByEventId")]
    public IActionResult GetStudentFormByEventId(string id)
    {
        return Ok(_basicStudentFormRepo.GetFormByEventId(id));
    }
    
    
    [Authorize]
    [HttpPost]
    [Route("MarkBasicAsPaid")]
    public IActionResult MarkBasicAsPaid(string id)
    {
        return Ok(_basicFormRepo.MarkAsPaid(id));
    }
    
    [Authorize]
    [HttpPost]
    [Route("MarkBasicAsAttended")]
    public IActionResult MarkBasicAsAttended(string id)
    {
        return Ok(_basicFormRepo.MarkAsAttended(id));
    }
    
    [Authorize]
    [HttpPost]
    [Route("MarkStudentFormAsPaid")]
    public IActionResult MarkStudentFormAsPaid(string id)
    {
        return Ok(_basicStudentFormRepo.MarkAsPaid(id));
    }
    
    [Authorize]
    [HttpPost]
    [Route("MarkStudentFormAsAttended")]
    public IActionResult MarkStudentFormAsAttended(string id)
    {
        return Ok(_basicStudentFormRepo.MarkAsAttended(id));
    }
    
    
}