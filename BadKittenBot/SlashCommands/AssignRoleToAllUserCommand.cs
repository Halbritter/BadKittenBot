using Discord;
using Discord.WebSocket;

namespace BadKittenBot.SlashCommands;

public class AssignRoleToAllUserCommand : ISlashCommand
{
    private DiscordSocketClient _client;

    public string Name
    {
        get => "set_rolle_for_all_user";
    }

    public async void Execute(SocketSlashCommand command)
    {
        command.RespondAsync("OK. Ich mach mich auf die Suche", ephemeral: true);

        IRole?      role            = (IRole?)command.GetValueFromOption("rolle");
        SocketGuild guild           = _client.GetGuild((ulong)command.GuildId);
        var         asyncEnumerable = guild.GetUsersAsync();

        if (role is null)
        {
            command.RespondAsync("Keine Rolle angegben!", ephemeral: true);
            return;
        }

        ulong roleID = role.Id;
        await foreach (IReadOnlyCollection<IGuildUser>? collection in asyncEnumerable)
        {
            foreach (IGuildUser guildUser in collection)
            {
                if (!guildUser.RoleIds.Contains(roleID))
                {
                    guildUser.AddRoleAsync(roleID);
                    command.FollowupAsync(guildUser.Nickname, ephemeral: true);
                }
            }
        }

        command.FollowupAsync("Ich hab fertig!", ephemeral: true);
    }

    public SlashCommandProperties BuildCommand(DiscordSocketClient client)
    {
        _client = client;
        Discord.SlashCommandBuilder bulider = new Discord.SlashCommandBuilder()
        {
            Name        = this.Name,
            Description = "Vergibt eine Rolle an alle Mitglieder",
            Options = new List<SlashCommandOptionBuilder>()
            {
                new SlashCommandOptionBuilder()
                {
                    Name        = "rolle",
                    Description = "Die zu vergebende Rolle",
                    Type        = ApplicationCommandOptionType.Role,
                    IsRequired  = true
                },
            }
        };
        return bulider.Build();
    }
}