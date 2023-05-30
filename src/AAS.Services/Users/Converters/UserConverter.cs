#region

using AAS.Domain.EmailVerifications;
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
            db.PhoneNumber, db.IsRemoved);
    }

    public static User[] ToUsers(this UserDb[] dbs)
    {
        return dbs.Select(ToUser).ToArray();
    }

    public static (User user, EmailVerification emailVerification) ToUserEmailVerification(this UserEmailVerificationJDb userEmailVerificationJDb)
    {
        Domain.Files.File? userPhoto = userEmailVerificationJDb.PhotoPath is null
            ? null
            : new Domain.Files.File(userEmailVerificationJDb.PhotoPath);

        User user = new(userEmailVerificationJDb.Id, userPhoto, userEmailVerificationJDb.FirstName, userEmailVerificationJDb.MiddleName,
            userEmailVerificationJDb.LastName, userEmailVerificationJDb.Email, userEmailVerificationJDb.PasswordHash,
            userEmailVerificationJDb.PhoneNumber, userEmailVerificationJDb.IsRemoved);

        EmailVerification emailVerification = new(userEmailVerificationJDb.Id, userEmailVerificationJDb.Token, userEmailVerificationJDb.IsVerified);
        return (user, emailVerification);
    }
}