using Discord.WebSocket;

namespace BadKittenBot.NewMemberActions;

public interface IJoinMemberListener
{
    public void Execute(SocketGuildUser user);
}