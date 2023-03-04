using AAS.Domain.Services;
using AAS.Domain.Users;
using AAS.Tools.Types.Results;

namespace AAS.Services.Users;

public partial class UsersService : IUsersService // + Tokens
{ 
    public Result SaveUserToken(UserToken userToken)
    {
        _usersRepository.SaveUserToken(userToken);
        return Result.Success();
    }

    public UserToken? GetUserToken(String token)
    {
        return _usersRepository.GetUserToken(token);
    }
}
