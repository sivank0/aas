using AAS.BackOffice.Controllers;
using AAS.BackOffice.Filters;
using AAS.Domain.AccessPolicies;
using AAS.Domain.Services;
using AAS.Domain.Users;
using AAS.Domain.Users.Roles;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AAS.BackOffice.Areas.Users.Controllers;
public class UsersController : BaseController
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    #region Users
    [HttpPost("users/register_user")]
    public Result RegisterUser([FromBody] UserRegistrationBlank userRegistrationBlank)
    {
        return _usersService.RegisterUser(userRegistrationBlank);
    }

    [HttpGet("users/get_user")]
    [IsAuthorized(AccessPolicy.UsersRead)]
    public User? GetUser(ID id)
    {
        return _usersService.GetUser(id);
    }

    [HttpGet("users/get_all")]
    [IsAuthorized(AccessPolicy.UsersRead)]
    public User[] GetAllUsers()
    {
        return _usersService.GetUsers();
    }

    [HttpGet("users/remove")]
    public Result RemoveUser(ID userId)
    {
        return _usersService.RemoveUser(userId);
    }
    #endregion

    [HttpGet("users/get_role_by_user_id")]
    [IsAuthorized(AccessPolicy.UsersRead)]
    public UserRole GetRole(ID userId)
    {
        return _usersService.GetUserRole(userId);
    }
}
