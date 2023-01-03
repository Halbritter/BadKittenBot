using BadKittenBot.ReactionListeners;
using BadKittenBot.SlashCommands;
using Discord;
using Discord.WebSocket;

namespace BadKittenBot;

public class Program
{
    private static DiscordSocketClient _client;
    private ulong _testGuildId;

    private string _token = "MTA1MDcwNTYzNjY0NTY3MDk2Mg.GC07gj.Qu6Hg5KVFCJHbcm16uN4cdru15C0Df5pPZsooU";

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

        _client = new DiscordSocketClient(config);
        _client.Log += Utils.Log;


        await _client.LoginAsync(TokenType.Bot, _token);
        await _client.StartAsync();
        SlashCommandHandler commandHandler = new SlashCommandHandler(_client);
        JoinEventHandler joinEventHandler = new JoinEventHandler(_client);
        // _client.MessageReceived      += ClientOnMessageReceived;
        // _client.ReactionAdded        += ClientOnReactionAdded;
        _client.UserJoined += joinEventHandler.EventListener;
        _client.SlashCommandExecuted += commandHandler.CommandListener;
        _client.Ready += commandHandler.RegisterComands;
        await Task.Delay(-1);
    }
}