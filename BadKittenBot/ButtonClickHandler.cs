using System.Reflection;
using BadKittenBot.ButtonClicks;
using Discord.WebSocket;

namespace BadKittenBot;

public class ButtonClickHandler
{
    private readonly DiscordSocketClient _client;
    private readonly List<IButton>       _commands;
    private readonly IEnumerable<Type>   _types;

    public ButtonClickHandler(DiscordSocketClient client)
    {
        _client   = client;
        _commands = new List<IButton>();
        _types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "BadKittenBot.ButtonClicks")
            .Where(c => c.IsClass && c.IsAssignableTo(typeof(IButton)));
        foreach (Type nestedType in _types)
        {
            IButton instance = Activator.CreateInstance(nestedType, args: _client) as IButton;
            _commands.Add(instance);
        }
    }

    public Task ButtonListener(SocketMessageComponent arg)
    {
        foreach (IButton command in _commands)
        {
            if (arg.Data.CustomId.StartsWith(command.Id))
                command.Execute(arg);
        }

        return Task.CompletedTask;
    }
}