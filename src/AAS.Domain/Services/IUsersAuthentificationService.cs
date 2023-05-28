using AAS.Domain.Users;
using AAS.Domain.Users.SystemUsers;
using AAS.Tools.Types.Results;

namespace AAS.Domain.Services;

public interface IUsersAuthentificationService
{
    Result Authenticate(string token);
    SystemUser? GetSystemUser(string token);
    DataResult<UserToken?> LogIn(string? email, string? password);
    DataResult<UserToken?> RegisterUser(UserRegistrationBlank userRegistrationBlank);
    void LogOut(string token);
}
