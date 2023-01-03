using Discord;
using Discord.WebSocket;

namespace BadKittenBot.ReactionListeners;

public interface IReactionListener
{
    public void React(Cacheable<IUserMessage, ulong> userMessage, Cacheable<IMessageChannel, ulong> messageChanel,
        SocketReaction reaction);
    public List<byte[]> Filter { get; }
}