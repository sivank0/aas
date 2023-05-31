using AAS.Configurator;
using AAS.Domain.Files;
using AAS.Domain.Services;
using AAS.Domain.Users;
using AAS.Domain.Users.Roles;
using AAS.Services.Users.Repositories;
using AAS.Tools.Managers;
using AAS.Tools.Types.Files;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;
using System.Text.RegularExpressions;

namespace AAS.Services.Users;

public class UsersManagementService : IUsersManagementService
{
    private readonly IUsersService _usersService;
    private readonly IUsersRepository _usersRepository;
    private readonly IEmailVerificationsService _emailVerificationsService;
    private readonly IFileStorageService _fileStorageService;

    public UsersManagementService(IUsersService usersService, IUsersRepository usersRepository, IEmailVerificationsService emailVerificationsService,
        IFileStorageService fileStorageService)
    {
        _usersService = usersService;
        _usersRepository = usersRepository;
        _emailVerificationsService = emailVerificationsService;
        _fileStorageService = fileStorageService;
    }

    private Result ValidateEmail(String? userEmail, Boolean isNewUser = false)
    {
        if (String.IsNullOrWhiteSpace(userEmail))
            return Result.Fail("Вы не ввели адрес электронной почты");

        Regex emailRegex = new(
           "^[\\w!#$%&'*+\\-/=?\\^_`{|}~]+(\\.[\\w!#$%&'*+\\-/=?\\^_`{|}~]+)*" +
           "@((([\\-\\w]+\\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\\.){3}[0-9]{1,3}))$"
        );

        if (!emailRegex.IsMatch(userEmail))
            return Result.Fail("Введенный адрес электронной почты имеет не действительный формат");

        if (isNewUser)
        {
            User? existingUser = _usersService.GetUser(userEmail);

            if (existingUser is not null)
                return Result.Fail("Пользователь с такой почтой существует");
        }
        return Result.Success();
    }

    private Result ValidatePassword(String? password, String? rePassword)
    {
        if (String.IsNullOrWhiteSpace(password))
            return Result.Fail("Вы не ввели пароль");

        if (String.IsNullOrWhiteSpace(rePassword))
            return Result.Fail("Вы не ввели повтор пароля");

        if (password != rePassword)
            return Result.Fail("Введенные пароли не совпадают");

        Regex passwordRegex = new("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

        if (!passwordRegex.IsMatch(password))
            return Result.Fail("Пароль должен содержать минимум 1 латинскую букву в верхнем " +
                "и нижем регистре, 1 цифру и 1 специальный символ");

        return Result.Success();
    }

    public DataResult<UserToken?> RegisterUser(UserRegistrationBlank userRegistrationBlank)
    {
        if (string.IsNullOrWhiteSpace(userRegistrationBlank.FirstName))
            return DataResult<UserToken?>.Fail("Не введено имя");

        if (string.IsNullOrWhiteSpace(userRegistrationBlank.LastName))
            return DataResult<UserToken?>.Fail("Не введена фамилия");

        if (string.IsNullOrWhiteSpace(userRegistrationBlank.PhoneNumber))
            return DataResult<UserToken?>.Fail("Не введнен номер телефона");

        Result emailValidationResult = ValidateEmail(userRegistrationBlank.Email);

        if (!emailValidationResult.IsSuccess)
            return DataResult<UserToken?>.Fail(emailValidationResult.Errors[0].Message);

        Result passwordValidationResult = ValidatePassword(userRegistrationBlank.Password, userRegistrationBlank.RePassword);

        if (!passwordValidationResult.IsSuccess)
            return DataResult<UserToken?>.Fail(passwordValidationResult.Errors[0].Message);

        userRegistrationBlank.Id = ID.New();

        _usersRepository.RegisterUser(userRegistrationBlank, Configurations.BackOffice.DefaultRoleId);
        _emailVerificationsService.SendVerificationMessage(userRegistrationBlank.Id.Value);

        UserToken? userToken = UserToken.New(userRegistrationBlank.Id!.Value);

        Result savingUserTokenResult = _usersService.SaveUserToken(userToken);

        if (!savingUserTokenResult.IsSuccess)
            return DataResult<UserToken?>.Fail(savingUserTokenResult.Errors[0].Message);

        return DataResult<UserToken?>.Success(userToken);
    }

    public async Task<Result> SaveUser(UserBlank userBlank, ID systemUserId)
    {
        Boolean isNewUser = userBlank.Id is null;

        if (String.IsNullOrWhiteSpace(userBlank.FirstName))
            return Result.Fail("Не введено имя");

        if (String.IsNullOrWhiteSpace(userBlank.LastName))
            return Result.Fail("Не введена фамилия");

        if (String.IsNullOrWhiteSpace(userBlank.PhoneNumber))
            return Result.Fail("Не введнен номер телефона");

        Result emailValidationResult = ValidateEmail(userBlank.Email, isNewUser);

        if (!emailValidationResult.IsSuccess)
            return Result.Fail(emailValidationResult.Errors[0].Message);

        if (isNewUser)
        {
            Result passwordValidationResult = ValidatePassword(userBlank.Password, userBlank.RePassword);

            if (!passwordValidationResult.IsSuccess)
                return Result.Fail(passwordValidationResult.Errors[0].Message);
        }

        if (userBlank.RoleId is null)
            return Result.Fail("Не выбрана роль пользователя");

        UserRole? userRole = _usersService.GetUserRole(userBlank.RoleId.Value);

        if (userRole is null) return Result.Fail("Выбранная роль не существует");

        userBlank.Id ??= ID.New();

        if (userBlank.FileBlank is not null)
        {
            (FileDetailsOfBase64[] fileDetailsOfBytes, String[] removeFilePaths) =
                FileBlank.GetUserFileDetails(userBlank.Id!.Value, userBlank.FileBlank);

            Result result = await _fileStorageService.SaveAndRemoveFiles(fileDetailsOfBytes, removeFilePaths);

            if (!result.IsSuccess)
                return Result.Fail(result.Errors[0]);

            userBlank.FileBlank.Path = fileDetailsOfBytes.Length != 0
                ? fileDetailsOfBytes.FirstOrDefault()?.FullPath
                : null;
        }

        _usersRepository.SaveUser(userBlank, systemUserId);

        if (isNewUser) _emailVerificationsService.SendVerificationMessage(userBlank.Id.Value);

        return Result.Success();
    }

    public Result ChangeUserPassword(ID userId, String? password, String? rePassword, ID systemUserId)
    {
        User? user = _usersService.GetUser(userId);

        if (user is null) return Result.Fail("Пользователь, которому меняется пароль не найден");

        Result passwordValidationResult = ValidatePassword(password, rePassword);

        if (!passwordValidationResult.IsSuccess)
            return Result.Fail(passwordValidationResult.Errors[0].Message);

        string? passwordHash = HashManager.DefinePasswordHash(password);

        if (string.IsNullOrWhiteSpace(passwordHash))
            return Result.Fail("Не удалось изменить пароль, повторите попытку ещё раз");

        _usersRepository.ChangeUserPassword(userId, passwordHash, systemUserId);

        return Result.Success();
    }
}
