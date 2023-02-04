using AAS.Domain.Users;
using AAS.Services.Users.Converters;
using AAS.Services.Users.Models;
using AAS.Services.Users.Repositories.Queries;
using AAS.Tools.DB;
using AAS.Tools.Types.IDs;

namespace AAS.Services.Users.Repositories;

public class UsersRepository : NpgSqlRepository, IUsersRepository
{
    public UsersRepository(String connectionString) : base(connectionString) { }

    //public void SaveUserAccess(UserAccessBlank userAccessBlank, ID userId)
    //{
    //    ExecutionInTransaction(command =>
    //    {

    //        UserAccessDb accessDb = userAccessBlank.ToUserAccessDb(userId);
    //        accessDb.ModifiedDateTimeUtc = accessDb.CreatedDateTimeUtc = DateTime.UtcNow;
    //        accessDb.ModifiedUserId = accessDb.CreatedUserId = userId;

    //        command.Execute(Sql.UserAccesses_Save, accessDb);

    //    });
    //}
    public void SaveUser(UserBlank userBlank)
    {
        SqlParameter[] parameters =
        {
            new("p_id", userBlank.Id!),
            new("p_firstname", userBlank.FirstName!),
            new("p_middlename", userBlank.MiddleName!),
            new("p_lastname", userBlank.LastName!),
            new("p_email", userBlank.Email!),
            new("p_passwordhash", userBlank.PasswordHash!),
            new("p_phonenumber", userBlank.PhoneNumber!),
            new("p_currentdatetimeutc", DateTime.UtcNow)
        };

        Execute(Sql.Users_Save, parameters);
    }

    public User? GetUser(ID userId)
    {
        SqlParameter[] parameters =
        {
            new("p_id", userId),
        };

        return Get<UserDb?>(Sql.Users_GetById, parameters)?.ToUser();
    }

    public User? GetUser(String userName)
    {
        SqlParameter[] parameters =
        {
            new("p_username", userName),
        };

        return Get<UserDb?>(Sql.Users_GetById, parameters)?.ToUser();
    }

    public void RemoveUser(ID userId)
    {
        SqlParameter[] parameters =
        {
            new("p_id", userId),
        };

        Execute(Sql.Users_DeleteByName, parameters);
    }
}
