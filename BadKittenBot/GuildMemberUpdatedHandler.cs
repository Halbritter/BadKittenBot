using System.Reflection;
using BadKittenBot.GuildMemberUpdate;
using Discord;
using Discord.WebSocket;

namespace BadKittenBot;

public class GuildMemberUpdatedHandler
{
    private readonly DiscordSocketClient      _client;
    private          List<IGuildMemberUpdate> _commands;
    private          IEnumerable<Type?>       _types;

    public GuildMemberUpdatedHandler(DiscordSocketClient client)
    {
        _client   = client;
        _commands = new List<IGuildMemberUpdate>();
        _types    = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "BadKittenBot.GuildMemberUpdate").Where(c => c.IsClass && c.IsAssignableTo(typeof(IGuildMemberUpdate)));
        foreach (Type nestedType in _types)
        {
            IGuildMemberUpdate instance = Activator.CreateInstance(nestedType, _client) as IGuildMemberUpdate;
            _commands.Add(instance);
        }
    }

    public Task MemberUpdate(Cacheable<SocketGuildUser, ulong> arg1, SocketGuildUser user)
    {
        foreach (IGuildMemberUpdate candidate in _commands)
        {
            Console.WriteLine("ChangedGuildMember");
            candidate.Execute(user);
        }

        return Task.CompletedTask;
    }
}