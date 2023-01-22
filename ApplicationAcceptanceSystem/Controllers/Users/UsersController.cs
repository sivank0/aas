using Microsoft.AspNetCore.Mvc;

namespace AAS.BackOffice.Controllers.Users;
public class UsersController : Controller
{
    [HttpGet("users/get_user")]
    public String GetUser()
    {
        return "Пососи";
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
