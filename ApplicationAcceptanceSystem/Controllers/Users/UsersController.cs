using AAS.Domain.Users;
using AAS.Services.Users;
using Microsoft.AspNetCore.Mvc;
using PMS.Tools.Types.IDs;

namespace AAS.BackOffice.Controllers.Users;
public class UsersController : Controller
{
    private readonly UsersService _usersService = new UsersService();

    [HttpGet("users/get_user")]
    public User? GetUser(ID id)
    {
        return _usersService.GetUser(id);
    }

    [HttpGet("users/get_by_name")]
    public User? GetUser(String userName)
    { 
        return _usersService.GetUser(userName);
    }

    [HttpDelete("users/delete_by_name")]
    public User? DeleteUser(String userName)
    {
        _usersService.DeleteUser(userName);
        return _usersService.GetUser(userName);
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }
}
