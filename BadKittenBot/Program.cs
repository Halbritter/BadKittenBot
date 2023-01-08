using System.Configuration;
using System.Reflection;
using BadKittenBot.ButtonClicks;
using BadKittenBot.ReactionListeners;
using BadKittenBot.SlashCommands;
using Discord;
using Discord.WebSocket;

namespace BadKittenBot;

public class Program
{
    private static DiscordSocketClient _client;
    private ulong _testGuildId;

    private string _token;

    public static Task Main(string[] args)
    {
        return new Program().MainAsync();
    }

    public async Task MainAsync()
    {
        var config = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.AllUnprivileged |
                             GatewayIntents.MessageContent |
                             GatewayIntents.GuildMessages |
                             GatewayIntents.GuildMessageReactions |
                             GatewayIntents.GuildMembers | GatewayIntents.Guilds
        };
        _token= ConfigurationManager.AppSettings["token"];

        _client = new DiscordSocketClient(config);
        _client.Log += Utils.Log;


        await _client.LoginAsync(TokenType.Bot, _token);
        await _client.StartAsync();
        SlashCommandHandler commandHandler = new SlashCommandHandler(_client);
        JoinEventHandler joinEventHandler = new JoinEventHandler(_client);
        ButtonClickHandler buttonClickHandler = new ButtonClickHandler(_client);
        // _client.MessageReceived      += ClientOnMessageReceived;
        // _client.ReactionAdded        += ClientOnReactionAdded;
        _client.UserJoined += joinEventHandler.EventListener;
        _client.SlashCommandExecuted += commandHandler.CommandListener;
        _client.Ready += commandHandler.RegisterComands;
        _client.ButtonExecuted += buttonClickHandler.ButtonListener;
        await Task.Delay(-1);
    }
}

public class ButtonClickHandler
{
    private readonly DiscordSocketClient _client;
    private readonly List<IButton> _commands;
    private readonly IEnumerable<Type> _types;

    public ButtonClickHandler(DiscordSocketClient client)
    {
        _client = client;
        _commands = new List<IButton>();
        _types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "BadKittenBot.ButtonClicks")
            .Where(c => c.IsClass && c.IsAssignableTo(typeof(IButton)));
        foreach (Type nestedType in _types)
        {
            IButton instance = Activator.CreateInstance(nestedType) as IButton;
            _commands.Add(instance);
        }
    }

    public Task ButtonListener(SocketMessageComponent arg)
    {
        foreach (IButton command in _commands)
        {
            if(command.Id == arg.Data.CustomId)
            command.Execute(arg);
        }

        return Task.CompletedTask;
    }
}