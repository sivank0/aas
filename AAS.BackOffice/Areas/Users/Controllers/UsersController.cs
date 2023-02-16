using AAS.BackOffice.Controllers;
using AAS.BackOffice.Filters;
using AAS.Domain.AccessPolicies;
using AAS.Domain.Services;
using AAS.Domain.Users;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;
using Microsoft.AspNetCore.Mvc;

namespace AAS.BackOffice.Areas.Users.Controllers;
public class UsersController : BaseController
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet("users/get_user")]
    [IsAuthorized(AccessPolicy.UsersRead)]
    public User? GetUser(ID id)
    {
        return _usersService.GetUser(id);
    }

    [HttpGet("users/get_by_name")]
    public User? GetUser(string userName)
    {
        return _usersService.GetUser(userName);
    }

    [HttpGet("users/remove")]
    public Result RemoveUser(ID userId)
    {
        return _usersService.RemoveUser(userId);
    }
}
