using AAS.Domain.Users;
using Npgsql;
using PMS.Tools.Types.IDs;

namespace AAS.Services.Users.Converters;
public static class UserConverter
{
    public static User? ToUser(this NpgsqlDataReader reader)
    {
        Object? userId = reader["id"];

        if (userId is null) return null;

        return new User(
            new ID((Byte[])userId),
            (String)reader["firstname"],
            (String)reader["middlename"],
            (String)reader["lastname"],
            (String)reader["email"],
            (String)reader["passwordhash"],
            (String)reader["phonenumber"]
        );
    }
}
