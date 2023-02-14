using Discord;
using Discord.WebSocket;

namespace BadKittenBot.SlashCommands;

public class GetAutokickConfig : ISlashCommand
{
    private DiscordSocketClient _client;

    public string Name
    {
        get => "get_autokick_config";
    }

    public void Execute(SocketSlashCommand command)
    {
        (ulong guild, ulong role, int hours, bool enabled) settings = new Database().GetAutokick(command.GuildId);

        if (settings.guild == 0)
        {
            command.RespondAsync("Keine Einstellungen gefunden. Bitte zuerst `/set_autokick` verwenden");
        }
        else
        {
            SocketGuild socketGuild = _client.GetGuild((ulong)command.GuildId!);
            SocketRole  socketRole  = socketGuild.GetRole(settings.role);
            command.RespondAsync($"Aktuelle Config:\n```Rolle: {socketRole.Name}\nZeit:  {settings.hours}\nAktiv: {settings.enabled}```");
        }
    }

    public SlashCommandProperties BuildCommand(DiscordSocketClient client)
    {
        _client = client;
        return new SlashCommandBuilder()
        {
            Name        = Name,
            Description = "Zeigt die aktuellen Autokick Einstellungen an"
        }.Build();
    }
}