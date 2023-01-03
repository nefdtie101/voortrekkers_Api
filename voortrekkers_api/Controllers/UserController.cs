using DataBaseModles;
using DataBaseModles.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
    public ActionResult CreateUser([FromBody] UserView user)
    {
        return Ok(_userRepo.CreateUser(user));
    }

    [Authorize]
    [HttpGet]
    [Route("GetAllUsers")]
    public IActionResult GetAllUsers()
    {
        return Ok(_userRepo.GetGetUsers());
    }

    [Authorize]
    [HttpPut]
    [Route("UpdateUser")]
    public ActionResult UpdateUser([FromBody] UserView user)
    {
        return Ok(_userRepo.EditUser(user));
    }
    
    
    [Authorize]
    [HttpPut]
    [Route("DeleteUser")]
    public ActionResult DeleteUser([FromBody] UserView user)
    {
        return Ok(_userRepo.DeleteUser(user));
    }
    

}