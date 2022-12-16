using Discord;
using Discord.WebSocket;

namespace BadKittenBot.SlashCommands;

public class AssignRoleToAllUserCommand : ISlashCommand
{
    public string Name
    {
        get => "set_rolle_for_all_user";
    }

    public void Execute(SocketSlashCommand command)
    {
        throw new NotImplementedException();
    }

    public SlashCommandProperties BuildCommand(DiscordSocketClient client)
    {
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
                new SlashCommandOptionBuilder()
                {
                    Name        = "include_bots",
                    Description = "Sollen Bots mit einbezogen werden?",
                    Type        = ApplicationCommandOptionType.Boolean,
                    IsRequired  = false,
                }
            }
        };
        return bulider.Build();
    }
}