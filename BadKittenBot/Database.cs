using System.Configuration;
using Discord;
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

    public void SetAutockick(IRole role, long hours, ulong guild)
    {
        using (MySqlCommand command = _connection.CreateCommand())
        {
            command.CommandText = "call fn_set_autokick(@guildid,@role,@hours)";
            command.Parameters.AddWithValue("guildid", guild);
            command.Parameters.AddWithValue("role", role.Id);
            command.Parameters.AddWithValue("hours", hours);
            command.ExecuteNonQuery();
        }
    }

    public void EnableAutokick(ulong? guild, bool b)
    {
        using (MySqlCommand command = _connection.CreateCommand())
        {
            command.CommandText = "UPDATE autokick_settings SET enabled = @b where guild = @guild";
            command.Parameters.AddWithValue("guild", guild);
            command.Parameters.AddWithValue("b", b);
            command.ExecuteNonQuery();
        }
    }

    public List<(ulong guild, ulong role, int hours)> GetAutokick()
    {
        List<(ulong guild, ulong role, int hours)> list = new List<(ulong guild, ulong role, int hours)>();
        using (MySqlCommand command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM autokick_settings where enabled = 1";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add((reader.GetUInt64("guild"), reader.GetUInt64("taraget_role"), reader.GetInt32("hours")));
            }
        }

        return list;
    }

    public (ulong guild, ulong role, int hours, bool enabled) GetAutokick(ulong? commandGuildId)
    {
        using (MySqlCommand command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM autokick_settings where guild = @guild";
            command.Parameters.AddWithValue("guild", commandGuildId);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                return (reader.GetUInt64("guild"), reader.GetUInt64("taraget_role"), reader.GetInt32("hours"), reader.GetBoolean("enabled"));
            }
        }

        return (0, 0, 0, false);
    }
}