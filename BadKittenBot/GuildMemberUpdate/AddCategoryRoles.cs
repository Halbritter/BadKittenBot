using Discord.WebSocket;

namespace BadKittenBot.GuildMemberUpdate;

public class AddCategoryRoles : IGuildMemberUpdate
{
    private readonly DiscordSocketClient _client;

    public AddCategoryRoles(DiscordSocketClient client)
    {
        _client = client;
    }

    public void Execute(SocketGuildUser user)
    {
    }
}