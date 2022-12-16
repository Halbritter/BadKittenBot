using Discord;
using Discord.WebSocket;

namespace BadKittenBot.SlashCommands;

public interface ISlashCommand
{
    public string Name { get; }
    public void Execute(SocketSlashCommand command);
    public SlashCommandProperties BuildCommand(DiscordSocketClient client);
}