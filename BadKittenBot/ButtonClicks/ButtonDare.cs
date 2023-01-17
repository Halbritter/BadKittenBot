using BadKittenBot.SlashCommands;
using Discord.WebSocket;

namespace BadKittenBot.ButtonClicks;

public class ButtonDare : IButton
{    public string Id => ID;

    public static string ID
    {
        get => "0FC925C6-5A33-4BAF-88BE-F04AF497D8FE";
    }

    public void Execute(SocketMessageComponent command)
    {
        new DareCommand().SendDare(command);
    }
    public ButtonDare(DiscordSocketClient client)
    {
        
    }
}