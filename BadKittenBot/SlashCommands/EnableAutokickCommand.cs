using Discord;
using Discord.WebSocket;

namespace BadKittenBot.SlashCommands;

public class EnableAutokickCommand : ISlashCommand
{
    public string Name
    {
        get => "enable_autokick";
    }

    public void Execute(SocketSlashCommand command)
    {
        bool b = (bool)(command.GetValueFromOption("enabled") ?? false);
        if (b)
        {
            new Database().EnableAutokick(command.GuildId, true);
            command.RespondAsync("Autokick ist jetzt aktiv. 1 mal pro stunde wernden jetzt die Mitglieder geprüft!");
        }
        else
        {
            new Database().EnableAutokick(command.GuildId, false);

            command.RespondAsync("Autokick ist jetzt deaktivert!");
        }
    }

    public SlashCommandProperties BuildCommand(DiscordSocketClient client)
    {
        return new SlashCommandBuilder()
        {
            Name        = this.Name,
            Description = "Aktiviert oder deaktivert den Autokick",
            Options = new List<SlashCommandOptionBuilder>()
            {
                new SlashCommandOptionBuilder()
                {
                    Name        = "enabled",
                    Description = "Aktivieren?",
                    Type        = ApplicationCommandOptionType.Boolean,
                    IsRequired  = true
                }
            }
        }.Build();
    }
}