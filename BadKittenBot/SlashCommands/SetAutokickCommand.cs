using Discord;
using Discord.WebSocket;

namespace BadKittenBot.SlashCommands;

public class SetAutokickCommand : ISlashCommand
{
    public string Name
    {
        get => "set_autokick";
    }

    public async void Execute(SocketSlashCommand command)
    {
        long   hours          = (long)(command.GetValueFromOption("hours") ?? 24);
        IRole? role           = command.GetValueFromOption("role") as IRole;
        ulong  commandGuildId = command.GuildId!.Value;

        new Database().SetAutockick(role, hours, guild: commandGuildId);

        await command.RespondAsync(
            $"Autotick wurde eingerichtete. Mitglieder die nach `{hours}` Stunden die Rolle `{role.Name}` nicht haben werden gekickt.\nAutokick kann nun mit `/enable_autokick true` aktiviert werden");
    }

    public SlashCommandProperties BuildCommand(DiscordSocketClient client)
    {
        return new SlashCommandBuilder()
        {
            Name        = this.Name,
            Description = "Einstellungen für den Autokick",
            Options = new List<SlashCommandOptionBuilder>()
            {
                new SlashCommandOptionBuilder()
                {
                    Name        = "hours",
                    Description = "Anzahl an Stunden die ein user ohne die angegebene Gruppe auf dem Server bleiben darf",
                    Type        = ApplicationCommandOptionType.Integer,
                    IsRequired  = true
                },
                new SlashCommandOptionBuilder()
                {
                    Name        = "role",
                    Description = "Rolle um dem kick zu entgehen",
                    Type        = ApplicationCommandOptionType.Role,
                    IsRequired  = true
                }
            }
        }.Build();
    }
}