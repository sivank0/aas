using System.Data;

namespace AAS.Tools.DB.Common;

public static class IDbConnectionExtensions
{
    public static void ExecuteCommand(this IDbConnection connection, Action<IDbCommand> action, IDbTransaction transaction = null)
    {
        using (IDbCommand command = connection.CreateCommand())
        {
            if (transaction != null)
                command.Transaction = transaction;

            action(command);
        }
    }

    public static T ExecuteCommand<T>(this IDbConnection connection, Func<IDbCommand, T> func, IDbTransaction transaction = null)
    {
        using (IDbCommand command = connection.CreateCommand())
        {
            if (transaction != null)
                command.Transaction = transaction;

            return func(command);
        }
    }
}
