using System.Text;
using Discord;
using Discord.WebSocket;

namespace BadKittenBot.SlashCommands;

public class ListAllGuildUserCommand : ISlashCommand
{
    private DiscordSocketClient _client;

    public string Name
    {
        get => "get_all_guild_user";
    }

    public async void Execute(SocketSlashCommand command)
    {
        var socketGuild = _client.GetGuild((ulong) command.GuildId!);
        var asyncEnumerable = socketGuild.GetUsersAsync();
        StringBuilder b = new StringBuilder();
        await foreach (var userCollection in asyncEnumerable)
        {
            foreach (var user in userCollection)
            {
                b.Append(user.DisplayName);
                b.AppendLine();
            }
        }

        command.RespondAsync(b.ToString());
    }

    public SlashCommandProperties BuildCommand(DiscordSocketClient client)
    {
        _client = client;
        Discord.SlashCommandBuilder bulider = new Discord.SlashCommandBuilder()
        {
            Name = this.Name,
            Description = "Gibt eine Liste mit allen Usern des Servers",
        };
        return bulider.Build();
    }
}