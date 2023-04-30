#region

using AAS.Tools.Types.IDs;

#endregion

namespace AAS.Domain.Users;

public class UserToken
{
    public ID UserId { get; }
    public string Token { get; }

    public UserToken(ID userId, string token)
    {
        UserId = userId;
        Token = token;
    }

    public static UserToken New(ID userId)
    {
        return new UserToken(userId, NewKey());
    }

    public static string NewKey()
    {
        return Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
    }
}