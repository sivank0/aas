using AAS.Domain.Users;
using AAS.Services.Users.Models;

namespace AAS.Services.Users.Converters;

internal static class UserTokenConverter
{
    public static UserToken ToUserToken(this UserTokenDb db)
    {
        return new UserToken(db.UserId, db.Token);
    }
}
