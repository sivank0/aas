using AAS.Domain.Users;
using AAS.Services.Users.Converters;
using AAS.Services.Users.Repositories.Queries;
using AAS.Tools.Database;
using Npgsql;
using PMS.Tools.Types.IDs;

namespace AAS.Services.Users.Repositories;
public class UsersRepositry
{
    //CRUD
    public void SaveUser(UserBlank userBlank)
    {
        DatabaseUtils.UseSqlCommand(command =>
        {
            command.CommandText = Sql.Users_Save;

            command.Parameters.AddWithValue("p_id", userBlank.Id!);
            command.Parameters.AddWithValue("p_firstname", userBlank.FirstName!);
            command.Parameters.AddWithValue("p_middlename", userBlank.MiddleName!);
            command.Parameters.AddWithValue("p_lastname", userBlank.LastName!);
            command.Parameters.AddWithValue("p_email", userBlank.Email!);
            command.Parameters.AddWithValue("p_passwordhash", userBlank.PasswordHash!);
            command.Parameters.AddWithValue("p_phonenumber", userBlank.PhoneNumber!);
            command.Parameters.AddWithValue("p_currentdatetimeutc", DateTime.UtcNow);

            command.ExecuteNonQuery();
        });
    }

    public User? GetUser(ID id)
    {
        return DatabaseUtils.UseSqlCommand(command =>
        {
            command.CommandText = Sql.Users_GetById;

            command.Parameters.AddWithValue("p_id", id);

            using (NpgsqlDataReader reader = command.ExecuteReader())
                while (reader.Read())
                    return reader.ToUser();

            return null;
        });
    }

    public User? GetUser(String userName)
    {
        return DatabaseUtils.UseSqlCommand(command =>
        {
            command.CommandText = Sql.Users_GetByName;

            command.Parameters.AddWithValue("p_name", userName);

            using (NpgsqlDataReader reader = command.ExecuteReader())
                while (reader.Read())
                    return reader.ToUser();

            return null;
        });
    }

    public User? DeleteUser(String userName)
    {
        return DatabaseUtils.UseSqlCommand(command =>
        {
            command.CommandText = Sql.Users_DeleteByName;

            command.Parameters.AddWithValue("p_name", userName);

            using (NpgsqlDataReader reader = command.ExecuteReader())
                while (reader.Read())
                    return reader.ToUser();

            return null;
        });
    }
}
