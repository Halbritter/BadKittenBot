using System.Reflection;
using BadKittenBot.NewMemberActions;
using Discord.WebSocket;

namespace BadKittenBot;

public class JoinEventHandler
{
    private DiscordSocketClient _client;
    private List<IJoinMemberListener> _commands;
    private IEnumerable<Type?>  _types;

    public JoinEventHandler(DiscordSocketClient client)

    {
        _client   = client;
        _commands = new List<IJoinMemberListener>();
        _types    = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "BadKittenBot.NewMemberActions").Where(c => c.IsClass && c.IsAssignableTo(typeof(IJoinMemberListener)));
        foreach (Type nestedType in _types)
        {
            IJoinMemberListener instance = Activator.CreateInstance(nestedType) as IJoinMemberListener;
            _commands.Add(instance);
        }
    }

    public Task EventListener(SocketGuildUser socketGuildUser)
    {
        Console.WriteLine("Join Event");
       
        foreach (IJoinMemberListener candidate in _commands)
        {
            Console.WriteLine("New Member");
            candidate.Execute(socketGuildUser);
        }

        return Task.CompletedTask;
    }
}