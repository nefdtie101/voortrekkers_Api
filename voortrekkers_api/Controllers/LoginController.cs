using DataBaseModles;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Repos.Interface;
using Voortrekkers.Pages.ResetPassword;

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

    [HttpPost]
    [Route("ResetPassword")]
    public ActionResult ResetPassword([FromBody] LoginModel login)
    {
        return Ok(_loginRepo.resetPassword(login));
    }
    
    [Authorize(Policy = "VerifyToken")]
    [HttpPost]
    [Route("CreatePassword")]
    public ActionResult CreatePassword([FromBody] ResetPasswordModel pass)
    {
        return Ok(_loginRepo.CreatePassword(pass,HttpContext));
    }


    [Authorize]
    [HttpGet]
    [Route("CheckToken")]

    public ActionResult CheakToken()
    {
        return Ok(true);
    }

    [Authorize(Policy = "RefreshToken")]
    [HttpGet]
    [Route("Refresh")]

    public async Task<ActionResult> Refresh(string email , string token)
    {
        
        return Ok(_loginRepo.refreshJwtToken(token, email));
        
    }





}