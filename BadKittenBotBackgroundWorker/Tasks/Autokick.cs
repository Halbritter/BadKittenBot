using BadKittenBot;
using Discord;
using Discord.WebSocket;
using static BadKittenBotBackgroundWorker.Program;

namespace BadKittenBotBackgroundWorker.Tasks;

public class Autokick : ITimerTask
{
    private readonly DiscordSocketClient _client;

    public Autokick(DiscordSocketClient client)
    {
        _client = client;
    }

    public TimerType TimerType => TimerType.Hourly;


    public async void Execute()
    {
        Log("Executing Timer Task");
        Database database = new Database();

        List<(ulong guild, ulong role, int hours)> valueTuples = database.GetAutokick();
        foreach ((ulong guild, ulong role, int hours) tuple in valueTuples)
        {
            IAsyncEnumerable<IReadOnlyCollection<IGuildUser>> usersAsync = _client.GetGuild(tuple.guild).GetUsersAsync();

            var list = usersAsync.ToListAsync();


            foreach (var readOnlyCollection in list.Result)
            {
                foreach (IGuildUser guildUser in readOnlyCollection)
                {
                    Log("Prüfe " + guildUser.DisplayName + " || " + guildUser.Nickname);
                    if (guildUser.IsBot) continue;
                    if (guildUser.RoleIds.Contains(tuple.role)) continue;
                    if ((DateTime.Now - guildUser.JoinedAt)!.Value.TotalHours > tuple.hours)
                    {
                        Log("Kicke User " + guildUser.DisplayName + " || " + guildUser.Nickname);
                        await guildUser.KickAsync();
                    }
                }
            }
        }
    }
}