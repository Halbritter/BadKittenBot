using BadKittenBot.NewMemberActions;
using Discord;
using Discord.WebSocket;

namespace BadKittenBot.NewMemberActions;

public class StartAgeVerifikation : IJoinMemberListener
{
    public void Execute(SocketGuildUser user)
    {
       Database.GetInstance().InsertUserJoin(user.Id);
    }
}