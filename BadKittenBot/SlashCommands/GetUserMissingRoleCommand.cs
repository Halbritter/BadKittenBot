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

        var options = command.Data.Options.Select<SocketSlashCommandDataOption, Tuple<string, object, ApplicationCommandOptionType>>(o =>
            new Tuple<string, object, ApplicationCommandOptionType>(o.Name, o.Value, o.Type));
        Tuple<string, object, ApplicationCommandOptionType> roleOption = options.Single(o => o.Item1 == "rolle");

        ;
        SocketGuild guild = _client.GetGuild((ulong)command.GuildId);
        SocketRole  role  = guild.GetRole((roleOption.Item2 as SocketRole).Id);

        IAsyncEnumerable<IReadOnlyCollection<IGuildUser>> asyncEnumerable = guild.GetUsersAsync();
        await foreach (var userCollection in asyncEnumerable)
        {
            IEnumerable<IGuildUser> missingUsers = role.Members.Except(userCollection);

            StringBuilder b = new StringBuilder();
            foreach (var missingUser in missingUsers)
            {
                b.Append("Fehlt: ");
                b.Append(missingUser.DisplayName);
                b.AppendLine();
            }

            Console.WriteLine("Ich konnte folgende Benutzer nicht finden\n" + b.ToString());

            // command.FollowupAsync("Ich konnte folgende Benutzer nicht finden\n" + b.ToString());
        }
    }

    public SlashCommandProperties BuildCommand(DiscordSocketClient client)
    {
        _client = client;
        Discord.SlashCommandBuilder bulider = new Discord.SlashCommandBuilder()
        {
            Name        = this.Name,
            Description = "Gibt eine Liste mit Benutzern die nicht in der Gruppe sind",
            Options = new List<SlashCommandOptionBuilder>()
            {
                new SlashCommandOptionBuilder()
                {
                    Name        = "rolle",
                    Description = "Die zu prüfende Rolle",
                    Type        = ApplicationCommandOptionType.Role,
                    IsRequired  = true
                },
                new SlashCommandOptionBuilder()
                {
                    Name        = "include_bots",
                    Description = "Sollen Bots mit in die Auflistung einbezogen werden?",
                    Type        = ApplicationCommandOptionType.Boolean,
                    IsRequired  = false,
                }
            }
        };
        return bulider.Build();
    }
}