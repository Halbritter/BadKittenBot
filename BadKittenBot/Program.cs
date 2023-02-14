using System.Configuration;
using Discord;
using Discord.WebSocket;

namespace BadKittenBot;

public class Program
{
    private static DiscordSocketClient _client;
    private        ulong               _testGuildId;


    public static Task Main(string[] args)
    {
        return new Program().MainAsync();
    }

    public async Task MainAsync()
    {
        _client = await CreateClient();
        SlashCommandHandler commandHandler     = new SlashCommandHandler(_client);
        JoinEventHandler    joinEventHandler   = new JoinEventHandler(_client);
        ButtonClickHandler  buttonClickHandler = new ButtonClickHandler(_client);
        // _client.MessageReceived      += ClientOnMessageReceived;
        // _client.ReactionAdded        += ClientOnReactionAdded;
        _client.UserJoined           += joinEventHandler.EventListener;
        _client.SlashCommandExecuted += commandHandler.CommandListener;
        _client.Ready                += commandHandler.RegisterComands;
        _client.ButtonExecuted       += buttonClickHandler.ButtonListener;
        await Task.Delay(-1);
    }

    public static async Task<DiscordSocketClient> CreateClient()
    {
        var config = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.AllUnprivileged |
                             GatewayIntents.MessageContent |
                             GatewayIntents.GuildMessages |
                             GatewayIntents.GuildMessageReactions |
                             GatewayIntents.GuildMembers | GatewayIntents.Guilds | GatewayIntents.GuildPresences
        };
        string token = ConfigurationManager.AppSettings["token"] ?? "NO TOKEN";

        DiscordSocketClient discordSocketClient = new DiscordSocketClient(config);
        discordSocketClient.Log += Utils.Log;


        await discordSocketClient.LoginAsync(TokenType.Bot, token);
        await discordSocketClient.StartAsync();
        return discordSocketClient;
    }
}