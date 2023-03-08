using Discord.WebSocket;

namespace BadKittenBot.Modals;

public class StrikeModal : IModal
{
    public async Task Execute(SocketModal modal)
    {
        Console.WriteLine("Gibt haue");
        await modal.RespondAsync("OK");
    }

    public static string ModalID
    {
        get => "ECA2075E-78FF-4A5A-B217-3A2000C7A5C7";
    }

    public string ID
    {
        get => ModalID;
    }
}