using BadKittenBot.SlashCommands;
using Discord.WebSocket;

namespace BadKittenBot.ButtonClicks;

public class ButtonTruth : IButton
{
    public static string ID
    {
        get => "52D479B6-BFE7-4D4D-9198-30D874E9F113";
    }

    public string Id => ID;

    public void Execute(SocketMessageComponent command)
    {
       new TruthCommand().SendTruth(command);
    }
}