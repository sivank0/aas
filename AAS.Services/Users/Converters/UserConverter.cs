using AAS.Domain.Users;
using AAS.Services.Users.Models;

namespace AAS.Services.Users.Converters;
public static class UserConverter
{
    public static User ToUser(this UserDb db)
    {
        return new User(db.Id, db.FirstName, db.MiddleName, db.LastName, db.Email, db.PasswordHash, db.PhoneNumber);
    }

    public static User[] ToUsers(this UserDb[] dbs)
    {
        return dbs.Select(ToUser).ToArray();
    }
}
