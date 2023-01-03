using Discord;
using Discord.WebSocket;

namespace BadKittenBot.ReactionListeners;

public class RedXReaction : IReactionListener
{
    public void React(Cacheable<IUserMessage, ulong> userMessage, Cacheable<IMessageChannel, ulong> messageChanel,
        SocketReaction reaction)
    {
        
    }

    public List<byte[]> Filter => new List<byte[]>()
    {
        new byte[] {0x5, 0x10}
    };
}