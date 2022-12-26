using DataBaseModles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repos.Interface;


namespace voortrekkers_api.Controllers;

public class EmailController : Controller
{
    private IEmailHelper _email;
    private IEmailRepo _emailRepo;
    public EmailController(IEmailHelper email , IEmailRepo repo)
    {
        _email = email;
        _emailRepo = repo;
    }
    
    [Authorize]
    [HttpPost]
    [Route("SendBulkEmail")]
    public IActionResult AddOrganization(string Subject, string Message , bool Staatmaker)
    {
        return Ok(_email.SendBulkEmail(Subject,Message,Staatmaker));
    }
    
    
    [HttpPost]
    [Route("UnSubscribe")]
    public IActionResult UnSubscribe(string email)
    {
        return Ok(_emailRepo.UnSubscribeEmail(email));
    }
    
}