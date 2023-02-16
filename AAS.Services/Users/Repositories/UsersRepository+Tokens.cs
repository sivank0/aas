using AAS.Domain.Users;
using AAS.Services.Users.Converters;
using AAS.Services.Users.Models;
using AAS.Services.Users.Repositories.Queries;
using AAS.Tools.DB;

namespace AAS.Services.Users.Repositories;

public partial class UsersRepository : NpgSqlRepository, IUsersRepository // + Tokens
{
    public void SaveUserToken(UserToken userToken)
    {
        SqlParameter[] parameters =
        {
            new("p_userid", userToken.UserId),
            new("p_token", userToken.Token),
            new("p_currentdatetimeutc", DateTime.UtcNow)
        };

        Execute(Sql.UserTokens_Save, parameters);
    }

    public UserToken? GetUserToken(String token)
    {
        SqlParameter[] parameters =
        {
            new("p_token", token),
        };

        return Get<UserTokenDb?>(Sql.UserTokens_GetByToken, parameters)?.ToUserToken();
    }
}
