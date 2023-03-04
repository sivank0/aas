using AAS.Tools.Types.IDs;

namespace AAS.Services.Users.Models;

public class UserTokenDb
{
    public ID UserId { get; set; }
    public String Token { get; set; }

    public DateTime DateTimeUtc { get; set; }

    public UserTokenDb(ID userId, string token, DateTime dateTimeUtc)
    {
        UserId = userId;
        Token = token;
        DateTimeUtc = dateTimeUtc;
    }
}
