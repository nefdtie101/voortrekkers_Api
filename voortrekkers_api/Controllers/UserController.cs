using DataBaseModles;
using Microsoft.AspNetCore.Mvc;
using Repos.Interface;


namespace voortrekkers_api.Controllers;

public class UserController : Controller
{
    private IUserRepo _userRepo;

    public UserController(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }
    
    
    [HttpPost]
    [Route("CreateUser")]
    public IActionResult CreateUser([FromBody] UserModel user)
    {
        return Ok(_userRepo.CreateNewUser(user));
    }
}