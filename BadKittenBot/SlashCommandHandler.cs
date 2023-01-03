using System.Reflection;
using BadKittenBot.SlashCommands;
using Discord.WebSocket;

namespace BadKittenBot;

public class  SlashCommandHandler
{
    private DiscordSocketClient _client;
    private List<ISlashCommand> _commands;
    private IEnumerable<Type?>  _types;

    public SlashCommandHandler(DiscordSocketClient client)

    {
        _client   = client;
        _commands = new List<ISlashCommand>();
        _types    = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "BadKittenBot.SlashCommands").Where(c => c.IsClass && c.IsAssignableTo(typeof(ISlashCommand)));
        foreach (Type nestedType in _types)
        {
            ISlashCommand instance = Activator.CreateInstance(nestedType) as ISlashCommand;
            _commands.Add(instance);
        }
    }

    public Task CommandListener(SocketSlashCommand command)
    {
        Console.WriteLine("Got Command: "+command.CommandName);
        foreach (ISlashCommand candidate in _commands)
        {
            if (candidate.Name == command.CommandName)
            {
                Console.WriteLine("Executing command    ");
                candidate.Execute(command);
            }
        }

        return Task.CompletedTask;
    }

    public Task RegisterComands()
    {
        foreach (ISlashCommand commandClass in _commands)
        {
            _client.CreateGlobalApplicationCommandAsync(commandClass.BuildCommand(_client));
        }

        return Task.CompletedTask;
    }
}