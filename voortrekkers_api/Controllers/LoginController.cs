using DataBaseModles;
using Microsoft.AspNetCore.Mvc;
using Repos.Interface;

namespace voortrekkers_api.Controllers;

public class LoginController : Controller
{
  
    private readonly ILoginRepo _loginRepo;
    
    
    public LoginController(ILoginRepo loginRepo)
    {
        _loginRepo = loginRepo;
    }

    [HttpPost]
    [Route("login")]
    public ActionResult Login([FromBody] LoginModel login)
    {
        return Ok(_loginRepo.Login(login));
    }

    [HttpGet]
    [Route("ResetPassword")]
    public ActionResult ResetPassword()
    {
        return Ok(_loginRepo.resetPassword());
    }
    
}