#region

using AAS.Domain.EmailVerifications;
using AAS.Domain.Files;
using AAS.Domain.Services;
using AAS.Domain.Users;
using AAS.Domain.Users.Permissions;
using AAS.Domain.Users.Roles;
using AAS.Services.Users.Repositories;
using AAS.Tools.Managers;
using AAS.Tools.Types.Files;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;
using System.Text.RegularExpressions;

#endregion

namespace AAS.Services.Users;

public partial class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;

    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    #region Users

    public User? GetUser(ID userId, Boolean includeRemoved = false)
    {
        return _usersRepository.GetUser(userId);
    }

    public User? GetUser(string email)
    {
        return _usersRepository.GetUser(email);
    }

    public (User user, EmailVerification emailVerification)? GetUserEmailVerification(ID userId)
    {
        return _usersRepository.GetUserEmailVerification(userId);
    }

    public (User user, EmailVerification emailVerification)? GetUser(string email, string? passwordHash)
    {
        return _usersRepository.GetUser(email, passwordHash);
    }

    public (User user, EmailVerification emailVerification)? GetUserEmailVerification(string userEmailVerificationToken)
    {
        return _usersRepository.GetUserEmailVerification(userEmailVerificationToken);
    }

    public User[] GetUsers()
    {
        return _usersRepository.GetUsers();
    }

    public Result RemoveUser(ID userId, ID systemUserId)
    {
        _usersRepository.RemoveUser(userId, systemUserId);

        return Result.Success();
    }

    #endregion

    #region UserRoles

    public Result SaveUserRole(UserRoleBlank userRoleBlank, ID systemUserId)
    {
        if (string.IsNullOrWhiteSpace(userRoleBlank.Name))
            return Result.Fail("Не введено название роли");

        if (userRoleBlank.AccessPolicies.Length == 0)
            return Result.Fail("Не выбраны политики доступа");

        userRoleBlank.Id ??= ID.New();

        _usersRepository.SaveUserRole(userRoleBlank, systemUserId);

        return Result.Success();
    }

    public UserRole? GetUserRole(ID userRoleId)
    {
        return _usersRepository.GetUserRole(userRoleId);
    }

    public UserRole? GetUserRoleByUserId(ID userId)
    {
        return _usersRepository.GetUserRoleByUserId(userId);
    }

    public UserRole[] GetUserRoles()
    {
        return _usersRepository.GetUserRoles();
    }

    public Result RemoveUserRole(ID userRoleId, ID systemUserId)
    {
        _usersRepository.RemoveUserRole(userRoleId, systemUserId);
        return Result.Success();
    }

    #endregion

    #region Permissions

    public UserPermission? GetUserPermission(ID userId)
    {
        return _usersRepository.GetUserPermission(userId);
    }

    #endregion
}