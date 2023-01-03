using System.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.SQLite;
using Discord.Rest;

namespace BadKittenBot;

public class Database
{
    public SQLiteConnection _connection;
    private static Database _instance;

    public static Database GetInstance()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        if (_instance is null) _instance = new Database();
        return _instance;
    }

    private Database()
    {
        SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
        builder.DataSource = "Data Source=\"C:\\Users\\Henrik\\RiderProjects\\BadKittenBot\\identifier.sqlite\"";
        _connection = new SQLiteConnection(builder.ToString());
        _connection.Open();
    }

    public List<ulong> GetUserJoinsBefore(DateTime date)
    {
        SQLiteCommand? command = _connection.CreateCommand();
        command.CommandText = "SELECT user_id FROM UserJoin uj where uj.join_date < @date;";
        command.Parameters.AddWithValue("date", date);
        List<ulong> list = new List<ulong>();
        using (SQLiteDataReader r = command.ExecuteReader())
        {
            while (r.NextResult())
            {
                list.Add((ulong) r.GetInt64(1));
            }
        }

        return list;
    }

    public void InsertUserJoin(ulong userId)
    {
        SQLiteCommand command = _connection.CreateCommand();
        command.CommandText = "INSERT OR UPDATE INTO UserJoin (user_id,join_date)values(@id,CURRENT_TIMESTAMP)";
    }
}