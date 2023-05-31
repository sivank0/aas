using AAS.Domain.Users;
using AAS.Tools.Types.IDs;
using AAS.Tools.Types.Results;

namespace AAS.Domain.Services;

public interface IUsersManagementService
{
    DataResult<UserToken?> RegisterUser(UserRegistrationBlank userRegistrationBlank);
    Task<Result> SaveUser(UserBlank userBlank, ID systenUserId);
    Result ChangeUserPassword(ID userId, string? password, string? rePassword, ID systemUserId);
}
