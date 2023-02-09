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
        bool validDB = false;

        try
        {
            MySqlCommand mySqlCommand = _connection.CreateCommand();
            mySqlCommand.CommandText = "select * from guilds;";
            mySqlCommand.ExecuteNonQuery();
            validDB = true;
            mySqlCommand.Dispose();
        }
        catch (MySqlConnector.MySqlException e)
        {
            validDB = false;
        }

        if (!validDB)
            initDatabase();
    }


    private void initDatabase()
    {
        using (MySqlCommand guildsCommand = _connection.CreateCommand())
        {
            guildsCommand.CommandText = SQLDefinition.Tables.Guilds;
            guildsCommand.ExecuteNonQuery();
        }


        using (MySqlCommand autokickCommand = _connection.CreateCommand())
        {
            autokickCommand.CommandText = SQLDefinition.Tables.Autokick_Settings;
            autokickCommand.ExecuteNonQuery();
        }

        using (MySqlCommand joinsCommand = _connection.CreateCommand())
        {
            joinsCommand.CommandText = SQLDefinition.Tables.Joins;
            joinsCommand.ExecuteNonQuery();
        }

        using (MySqlCommand strikesCommand = _connection.CreateCommand())
        {
            strikesCommand.CommandText = SQLDefinition.Tables.Strikes;
            strikesCommand.ExecuteNonQuery();
        }
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
        using (MySqlCommand checkGuildCommand = _connection.CreateCommand())
        {
            checkGuildCommand.CommandText = "SELECT id FROM guilds where guild_id = @id limit 0,1";
            MySqlDataReader mySqlDataReader = checkGuildCommand.ExecuteReader();
            if (mySqlDataReader.NextResult())
            {
            }
        }

        if ()

            using (MySqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO  joins (user,date,guild)values(@id,CURRENT_TIMESTAMP,@guild)ON DUPLICATE KEY UPDATE date=CURRENT_TIMESTAMP;";
                command.Parameters.AddWithValue("id", userId);
                command.Parameters.AddWithValue("guild", userId);
                command.ExecuteNonQuery();
            }
    }

    private class SQLDefinition
    {
        internal static class Tables
        {
            internal static string Guilds =
                "create table guilds ( guild_id BIGINT not null comment 'Guild ID', id int auto_increment primary key, constraint guilds_guild_id_uindex unique (guild_id) using hash );";

            internal static string Autokick_Settings =
                "create table autokick_settings ( id int not null, guild int not null, taraget_role BIGINT not null, hours int default 48 not null, enabled tinyint(1) default 0 not null, primary key (id), constraint autokick_settings_Guilds_id_fk foreign key (guild) references guilds (id) );";

            internal static string Joins =
                "create table joins ( guild int not null, user BIGINT not null, date datetime null, primary key (user, guild), constraint joins_user_uindex unique (user), constraint Joins_Guilds_id_fk foreign key (guild) references guilds (id) );";

            internal static string Strikes =
                "create table strikes ( id int not null, guild int not null, user BIGINT not null, reason text null, primary key (id), constraint strikes_Guilds_id_fk foreign key (guild) references guilds (id) );";
        }
    }
}