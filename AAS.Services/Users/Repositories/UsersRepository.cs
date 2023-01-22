using AAS.Domain.Users;
using AAS.Services.Users.Repositories.Queries;
using AAS.Tools.Database;

namespace AAS.Services.Users.Repositories;
public class UsersRepositry
{
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
}
