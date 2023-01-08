using Discord;
using Discord.WebSocket;

namespace BadKittenBot.ButtonClicks;

public interface IButton
{
    public static string ID { get; }
    public string Id { get; }
    public void Execute(SocketMessageComponent command);
}