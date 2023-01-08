using System.Reflection;
using System.Text;
using BadKittenBot.ReactionListeners;
using BadKittenBot.SlashCommands;
using Discord;
using Discord.WebSocket;

namespace BadKittenBot;

public class ReactionHandler
{
    private DiscordSocketClient _client;
    private List<IReactionListener> _commands;
    private IEnumerable<Type?> _types;

    public ReactionHandler(DiscordSocketClient client)

    {
        _client = client;
        _commands = new List<IReactionListener>();
        _types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "BadKittenBot.ReactionListeners")
            .Where(c => c.IsClass && c.IsAssignableTo(typeof(IReactionListener)));
        foreach (Type nestedType in _types)
        {
            IReactionListener instance = Activator.CreateInstance(nestedType) as IReactionListener;
            _commands.Add(instance);
            Console.WriteLine("Loaded reaction: " +instance.Filter);

        }
    }

    public Task CommandListener(Cacheable<IUserMessage, ulong> userMessage,
        Cacheable<IMessageChannel, ulong> messageChanel,
        SocketReaction reaction)
    {
        foreach (IReactionListener candidate in _commands)
        {
            if (candidate.Filter.Contains(Encoding.UTF8.GetBytes(reaction.Emote.Name)))
            {
                candidate.React(userMessage, messageChanel, reaction);
            }
        }

        return Task.CompletedTask;
    }
}