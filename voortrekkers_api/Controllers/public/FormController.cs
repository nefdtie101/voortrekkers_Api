using DataBaseModles.Forms;
using Microsoft.AspNetCore.Mvc;
using Repos.Interface;


public class FormController : Controller
{
    private IBasicFormRepo _basicFormRepo;
    private IBasicStudentFormRepo _basicStudentFormRepo;
    
    public FormController(IBasicFormRepo basicFormRepo , IBasicStudentFormRepo basicStudentFormRepo)
    {
        _basicFormRepo = basicFormRepo;
        _basicStudentFormRepo = basicStudentFormRepo;
    }
    
    [HttpPost]
    [Route("FillInBasicForm")]
    public IActionResult FillInBasicForm([FromBody] BasicForm Form)
    {
        return Ok(_basicFormRepo.FillInForm(Form));
    }
    
    [HttpPost]
    [Route("FillInBasicStudentForm")]
    public IActionResult FillInBasicStudentForm([FromBody] BasicStudentForm Form)
    {
        return Ok(_basicStudentFormRepo.FillInForm(Form));
    }
}