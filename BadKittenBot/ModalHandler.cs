using System.Reflection;
using BadKittenBot.Modals;
using Discord.WebSocket;

namespace BadKittenBot;

public class ModalHandler
{
    private DiscordSocketClient _client;
    private List<IModal>        _commands;
    private IEnumerable<Type?>  _types;

    public ModalHandler(DiscordSocketClient client)

    {
        _client   = client;
        _commands = new List<IModal>();
        _types    = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "BadKittenBot.Modals").Where(c => c.IsClass && c.IsAssignableTo(typeof(IModal)));
        foreach (Type nestedType in _types)
        {
            IModal instance = Activator.CreateInstance(nestedType) as IModal;
            _commands.Add(instance);
            Console.WriteLine("Loaded Modal: " + instance.ID);
        }
    }

    public Task EventListener(SocketModal command)
    {
        Console.WriteLine("Got Command: " + command.Data.CustomId);
        foreach (IModal candidate in _commands)
        {
            if (candidate.ID == command.Data.CustomId)
            {
                Console.WriteLine("Executing command    ");
                candidate.Execute(command);
            }
        }

        return Task.CompletedTask;
    }
}