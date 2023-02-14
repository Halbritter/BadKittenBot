using Discord.WebSocket;

namespace BadKittenBot.GuildMemberUpdate;

public interface IGuildMemberUpdate
{
    public void Execute(SocketGuildUser arg2);
}