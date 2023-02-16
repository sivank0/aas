using AAS.Tools.Types.IDs;

namespace AAS.Services.Users.Models;

public class UserTokenDb
{
    public ID UserId { get; set; }
    public String Token { get; set; }

    public DateTime CreatedDateTimeUtc { get; set; }

    public UserTokenDb(ID userId, string token, DateTime createdDateTimeUtc)
    {
        UserId = userId;
        Token = token;
        CreatedDateTimeUtc = createdDateTimeUtc;
    }
}
