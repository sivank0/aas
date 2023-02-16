using AAS.Tools.Types.IDs;

namespace AAS.Domain.Users;

public class UserToken
{
    public ID UserId { get; }
    public String Token { get; }

    public UserToken(ID userId, String token)
    {
        UserId = userId;
        Token = token;
    }

    public static UserToken New(ID userId)
    {
        return new UserToken(userId, NewKey());
    }

    public static String NewKey() => Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
}
