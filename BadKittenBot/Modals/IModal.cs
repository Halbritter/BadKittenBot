using Discord.WebSocket;

namespace BadKittenBot.Modals;

public interface IModal
{
    public static string ModalID { get; }
    public        string ID      { get; }
    public Task Execute(SocketModal modal);
}