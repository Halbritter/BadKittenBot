using System.Configuration;
using MySqlConnector;

namespace BadKittenBot;

public class Database
{
    private MySqlConnection _connection;

    public Database()
    {
        MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
        builder.Server   = ConfigurationManager.AppSettings["db_host"] ?? "localhost";
        builder.UserID   = ConfigurationManager.AppSettings["db_user"] ?? "badkittenbot";
        builder.Password = ConfigurationManager.AppSettings["db_password"] ?? "";
        builder.Database = ConfigurationManager.AppSettings["db_schema"] ?? "badkittenbot";

        _connection = new MySqlConnection(builder.ToString());
        _connection.Open();
        Console.WriteLine("Openend DB at " + ConfigurationManager.AppSettings["db_user"] + "@" + ConfigurationManager.AppSettings["db_host"] + "/" + ConfigurationManager.AppSettings["db_schema"]);
        Console.WriteLine("Connection: " + _connection.State);
    }


    public List<ulong> GetUserJoinsBefore(DateTime date)
    {
        MySqlCommand? command = _connection.CreateCommand();
        command.CommandText = "SELECT user FROM joins uj where uj.date < @date;";
        command.Parameters.AddWithValue("date", date);
        List<ulong> list = new List<ulong>();
        using (MySqlDataReader r = command.ExecuteReader())
        {
            while (r.NextResult())
            {
                list.Add((ulong)r.GetInt64(1));
            }
        }

        return list;
    }

    public void InsertUserJoin(ulong userId, ulong guildId)
    {
        using (MySqlCommand command = _connection.CreateCommand())
        {
            command.CommandText = "call fn_insert_join(@id,@guild)";
            command.Parameters.AddWithValue("id", userId);
            command.Parameters.AddWithValue("guild", guildId);
            command.ExecuteNonQuery();
        }
    }
}