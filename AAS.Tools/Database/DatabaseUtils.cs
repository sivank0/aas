using Npgsql;

namespace AAS.Tools.Database;
public class DatabaseUtils
{
    const String connectionString = "Server=localhost;Username=postgres;Password=1234;Database=aas";

    public static void UseSqlCommand(Action<NpgsqlCommand> getCommand)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                command.Connection = connection;
                getCommand(command);
            }
        }
    }


    public static T UseSqlCommand<T>(Func<NpgsqlCommand, T> getCommand)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                command.Connection = connection;
                return getCommand(command);
            }
        }
    }
}
