using Discord;
using Discord.WebSocket;

namespace BadKittenBot.ButtonClicks;

public class ButtonKick : IButton
{
    private readonly DiscordSocketClient _client;

    public string Id
    {
        get => ID;
    }

    public static string ID => "627D3FF1-BBE3-4D46-B348-F3959965C1E0";

    public async void Execute(SocketMessageComponent command)
    {
        ulong userID = UInt64.Parse(command.Data.CustomId.Split(":")[1]);
        await foreach (var readOnlyCollection in _client.GetGuild((ulong) command.GuildId).GetUsersAsync())
        foreach (IGuildUser user in readOnlyCollection)
        {
            if (user.Id == userID)
            {
                await user.KickAsync();
                 command.RespondAsync("Bye Bye " + user.DisplayName);

            }
        }
    }

    public ButtonKick(DiscordSocketClient client)
    {
        _client = client;
    }
}