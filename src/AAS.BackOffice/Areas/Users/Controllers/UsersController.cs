#region

using AAS.BackOffice.Controllers;
using AAS.BackOffice.Filters;
using AAS.Domain.AccessPolicies;
using AAS.Domain.Services;
using AAS.Domain.Users;
using AAS.Domain.Users.Permissions;
using AAS.Domain.Users.Roles;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace AAS.BackOffice.Areas.Users.Controllers;

public class UsersController : BaseController
{
    private readonly IUsersService _usersService;
    private readonly IUsersManagementService _usersManagementService;

    public UsersController(IUsersService usersService, IUsersManagementService usersManagementService)
    {
        _usersService = usersService;
        _usersManagementService = usersManagementService;
    }

    #region Users

    [HttpPost("users/save")]
    [IsAuthorized(AccessPolicy.UsersRead)]
    public Task<Result> SaveUser([FromBody] UserBlank userBlank)
    {
        return _usersManagementService.SaveUser(userBlank, SystemUser.Id);
    }

    [HttpGet("users/get_by_id")]
    [IsAuthorized(AccessPolicy.UsersRead)]
    public User? GetUser(ID id)
    {
        return _usersService.GetUser(id);
    }

    [HttpGet("users/get_details_for_editor")]
    [IsAuthorized(AccessPolicy.UsersUpdate)]
    public object? GetUserDetailsForEditor(ID userId)
    {
        User? user = _usersService.GetUser(userId);

        if (user is null) return null;

        UserPermission? userPermission = SystemUser.HasAccess(AccessPolicy.UserRolesRead)
            ? _usersService.GetUserPermission(userId)
            : null;

        return new { user, userPermission };
    }

    [HttpGet("users/get_all")]
    [IsAuthorized(AccessPolicy.UsersRead)]
    public User[] GetAllUsers()
    {
        return _usersService.GetUsers();
    }

    public record ChangeUserPasswordRequest(ID UserId, string? Password, string? RePassword);

    [HttpPost("users/change_password")]
    [IsAuthorized(AccessPolicy.UsersUpdate)]
    public Result ChangeUserPassword(ChangeUserPasswordRequest changeUserPasswordRequest)
    {
        return _usersManagementService.ChangeUserPassword(
            changeUserPasswordRequest.UserId,
            changeUserPasswordRequest.Password,
            changeUserPasswordRequest.RePassword,
            SystemUser.Id
        );
    }

    [HttpGet("users/remove")]
    [IsAuthorized(AccessPolicy.UsersUpdate)]
    public Result RemoveUser(ID userId)
    {
        return _usersService.RemoveUser(userId, SystemUser.Id);
    }

    #endregion

    [HttpGet("users/get_all_roles")]
    [IsAuthorized(AccessPolicy.UserRolesRead)]
    public UserRole[] GetUserRoles()
    {
        return _usersService.GetUserRoles();
    }

    [HttpGet("users/get_role_by_user_id")]
    [IsAuthorized(AccessPolicy.UsersRead)]
    public UserRole? GetRole(ID userId)
    {
        return _usersService.GetUserRoleByUserId(userId);
    }
}