using System.Text;
using Discord;
using Discord.WebSocket;

namespace BadKittenBot.SlashCommands;

public class GetUserMissingRoleCommand : ISlashCommand
{
    private DiscordSocketClient _client;

    public string Name
    {
        get => "get_member_missing";
    }

    public async void Execute(SocketSlashCommand command)
    {
        if (!(command.User as IGuildUser)!.GuildPermissions.Administrator)
        {
            throw new Exception("Not permitted to run this command!");
        }

        var msg = command.RespondAsync("Ich mach mich auf die Suche", ephemeral: true);
        var options =
            command.Data.Options
                .Select<SocketSlashCommandDataOption, Tuple<string, object, ApplicationCommandOptionType>>
                    (o => new Tuple<string, object, ApplicationCommandOptionType>(o.Name, o.Value, o.Type));
        Tuple<string, object, ApplicationCommandOptionType> roleOption = options.Single(o => o.Item1 == "rolle");
        SocketGuild guild = _client.GetGuild((ulong) command.GuildId);
        var includeBotsOption = options.FirstOrDefault(o => o.Item1 == "include_bots", null);
        bool includeBots = (bool) (includeBotsOption?.Item2 ?? false);

        IAsyncEnumerable<IReadOnlyCollection<IGuildUser>> asyncEnumerable = guild.GetUsersAsync();
        await foreach (var userCollection in asyncEnumerable)
        {
            StringBuilder b = new StringBuilder();
            foreach (var user in userCollection)
            {
                if (!includeBots)
                {
                    if (user.IsBot) continue;
                }

                if (!user.RoleIds.Contains((ulong) ((SocketRole) options.First(o => o.Item1 == "rolle").Item2).Id))
                {
                    b.Append("Fehlt: ");
                    b.Append(user.DisplayName);
                    b.AppendLine();
                }
            }

            command.FollowupAsync("Ich konnte folgende Benutzer nicht finden\n" + b.ToString());
        }
    }

    public SlashCommandProperties BuildCommand(DiscordSocketClient client)
    {
        _client = client;
        Discord.SlashCommandBuilder bulider = new Discord.SlashCommandBuilder()
        {
            Name = this.Name,
            Description = "Gibt eine Liste mit Benutzern die nicht in der Gruppe sind",
            Options = new List<SlashCommandOptionBuilder>()
            {
                new SlashCommandOptionBuilder()
                {
                    Name = "rolle",
                    Description = "Die zu prüfende Rolle",
                    Type = ApplicationCommandOptionType.Role,
                    IsRequired = true
                },
                new SlashCommandOptionBuilder()
                {
                    Name = "include_bots",
                    Description = "Sollen Bots mit in die Auflistung einbezogen werden? (Standard ist nein)",
                    Type = ApplicationCommandOptionType.Boolean,
                    IsRequired = false,
                }
            }
        };
        return bulider.Build();
    }
}