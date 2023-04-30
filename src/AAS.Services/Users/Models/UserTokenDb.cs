#region

using AAS.Tools.Types.IDs;

#endregion

namespace AAS.Services.Users.Models;

public class UserTokenDb
{
    public ID UserId { get; set; }
    public string Token { get; set; }

    public DateTime DateTimeUtc { get; set; }

    public UserTokenDb(ID userId, string token, DateTime dateTimeUtc)
    {
        UserId = userId;
        Token = token;
        DateTimeUtc = dateTimeUtc;
    }
}