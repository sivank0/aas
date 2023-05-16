#region

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

#endregion

namespace AAS.Services.Users;

public partial class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IFileStorageService _fileStorageService;

    public UsersService(IUsersRepository usersRepository, IFileStorageService fileStorageService)
    {
        _usersRepository = usersRepository;
        _fileStorageService = fileStorageService;
    }

    #region Users

    public async Task<Result> SaveUser(UserBlank userBlank, ID systemUserId)
    {
        if (string.IsNullOrWhiteSpace(userBlank.Email)) return Result.Fail("Не введен Email");

        if (string.IsNullOrWhiteSpace(userBlank.FirstName)) return Result.Fail("Не введено имя");

        if (string.IsNullOrWhiteSpace(userBlank.LastName)) return Result.Fail("Не введена фамилия");

        if (string.IsNullOrWhiteSpace(userBlank.PhoneNumber)) return Result.Fail("Не введнен номер телефона");

        if (userBlank.Id is null)
        {
            if (string.IsNullOrWhiteSpace(userBlank.Password)) return Result.Fail("Не введен пароль");

            if (string.IsNullOrWhiteSpace(userBlank.RePassword)) return Result.Fail("Не введен повтор пароля");

            if (userBlank.Password != userBlank.RePassword) return Result.Fail("Пароли не совпадают");
        }

        if (userBlank.RoleId is null)
            return Result.Fail("Не выбрана роль пользователя");

        UserRole? userRole = GetUserRole(userBlank.RoleId.Value);

        if (userRole is null) return Result.Fail("Выбранная роль не существует");

        userBlank.Id ??= ID.New();

        String? userPhotoPath = userBlank.FileBlank?.Path; 
        
        if (userBlank.FileBlank is not null)
        {
            (FileDetailsOfBytes[] fileDetailsOfBytes, String[] removeFilePaths) =
                FileBlank.GetFileDetails(userBlank.FileBlank, userId: userBlank.Id!.Value);

            Result result = await _fileStorageService.SaveAndRemoveFiles(fileDetailsOfBytes, removeFilePaths);

            if (!result.IsSuccess)
            {
                userPhotoPath = null;
                return Result.Fail(result.Errors[0]);
            }
        }

        _usersRepository.SaveUser(userBlank, userPhotoPath, systemUserId);
        return Result.Success();
    }

    public User? GetUser(ID id)
    {
        return _usersRepository.GetUser(id);
    }

    public User? GetUser(string email, string? passwordHash = null)
    {
        return _usersRepository.GetUser(email, passwordHash);
    }

    public User[] GetUsers()
    {
        return _usersRepository.GetUsers();
    }

    public Result ChangeUserPassword(ID userId, string? password, string? rePassword, ID systemUserId)
    {
        User? user = GetUser(userId);

        if (user is null) return Result.Fail("Пользователь, которому меняется пароль не найден");

        if (string.IsNullOrWhiteSpace(password)) return Result.Fail("Не введен пароль");

        if (string.IsNullOrWhiteSpace(rePassword)) return Result.Fail("Не введен повтор пароля");

        if (password != rePassword) return Result.Fail("Пароли должны совпадать");

        string? passwordHash = HashManager.DefinePasswordHash(password);

        if (string.IsNullOrWhiteSpace(passwordHash))
            return Result.Fail("Не удалось изменить пароль, повторите попытку ещё раз");

        _usersRepository.ChangeUserPassword(userId, passwordHash, systemUserId);

        return Result.Success();
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