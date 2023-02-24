using AAS.BackOffice.Controllers;
using AAS.BackOffice.Filters;
using AAS.Domain.AccessPolicies;
using AAS.Domain.AccessPolicies.Extensions;
using AAS.Domain.AccessPolicies.Utils;
using AAS.Domain.Services;
using AAS.Domain.Users.Roles;
using AAS.Tools.Extensions;
using AAS.Tools.Types.Results;
using Microsoft.AspNetCore.Mvc;

namespace AAS.BackOffice.Areas.Users.Controllers;

public class UserRolesController : BaseController
{
    private readonly IUsersService _usersService;

    public UserRolesController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpPost("user_roles/save")]
    [IsAuthorized(AccessPolicy.UserRolesUpdate)]
    public Result SaveUserRole(UserRoleBlank userRoleBlank)
    {
        return _usersService.SaveUserRole(userRoleBlank, SystemUser.Id);
    }

    [HttpGet("user_roles/get_details")]
    [IsAuthorized(AccessPolicy.UserRolesRead)]
    public Object GetUserRoleDetails()
    {
        UserRole[] userRoles = _usersService.GetUserRoles();

        AccessPolicyDetails[] accessPoliciesDetails = Enum<AccessPolicy>.GetArray().ToDetails();

        return new { userRoles, accessPoliciesDetails };
    }
}