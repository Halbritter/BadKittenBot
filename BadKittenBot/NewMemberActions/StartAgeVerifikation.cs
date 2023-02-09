using Discord.WebSocket;

namespace BadKittenBot.NewMemberActions;

public class StartAgeVerifikation : IJoinMemberListener
{
    public void Execute(SocketGuildUser user)
    {
        Database database = new Database();
        database.InsertUserJoin(user.Id, user.Guild.Id);
    }
}