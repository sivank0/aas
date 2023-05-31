using AAS.BackOffice.Controllers;
using AAS.BackOffice.Filters;
using AAS.Domain.AccessPolicies;
using AAS.Domain.Services;
using AAS.Domain.Users;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;
using Microsoft.AspNetCore.Mvc;

namespace AAS.BackOffice.Areas.Users.Controllers;

public class UserProfileController : BaseController
{
    private readonly IUsersService _usersService;
    private readonly IUsersManagementService _usersManagementService;
    public UserProfileController(IUsersService usersService, IUsersManagementService usersManagementService)
    {
        _usersService = usersService;
        _usersManagementService = usersManagementService;
    }

    [HttpPost("user_profile/save")]
    [IsAuthorized(AccessPolicy.UserProfile)]
    public Task<Result> SaveUser([FromBody] UserBlank userBlank)
    {
        return _usersManagementService.SaveUser(userBlank, SystemUser.Id);
    }

    [HttpGet("user_profile/get_by_id")]
    [IsAuthorized(AccessPolicy.UserProfile)]
    public User? GetUser(ID userId)
    {
        return _usersService.GetUser(userId);
    }
}
