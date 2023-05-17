#region

using AAS.Domain.Files;
using AAS.Domain.Users;
using AAS.Services.Users.Models;

#endregion

namespace AAS.Services.Users.Converters;

public static class UserConverter
{
    public static User ToUser(this UserDb db)
    {
        Domain.Files.File? userPhoto = db.PhotoPath is null
            ? null
            : new Domain.Files.File(db.PhotoPath);

        return new User(db.Id, userPhoto, db.FirstName, db.MiddleName, db.LastName, db.Email, db.PasswordHash,
            db.PhoneNumber);
    }

    public static User[] ToUsers(this UserDb[] dbs)
    {
        return dbs.Select(ToUser).ToArray();
    }
}