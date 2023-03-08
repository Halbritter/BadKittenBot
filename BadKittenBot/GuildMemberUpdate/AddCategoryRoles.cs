using Discord;
using Discord.WebSocket;

namespace BadKittenBot.GuildMemberUpdate;

public class AddCategoryRoles : IGuildMemberUpdate
{
    private readonly DiscordSocketClient _client;

    public AddCategoryRoles(DiscordSocketClient client)
    {
        _client = client;
    }

    public void Execute(SocketGuildUser user)
    {
        IEnumerable<SocketRole> caRoles = user.Guild.Roles.OrderBy(o => o.Position).Reverse();

        IRole? curentCat = null;
        foreach (SocketRole role in caRoles)
        {
            //  if (role.Name.StartsWith("\u3164")) curentCat = role;
            //  if ((!role.Name.StartsWith("\u3164")) && curentCat is not null) user.AddRoleAsync(curentCat);
        }

        curentCat = null;
        bool shouldBeActiv = false;
        foreach (SocketRole role in caRoles)
        {
            //   if (role.Name.StartsWith("\u3164")) curentCat = role;
            //   if (!role.Name.StartsWith("\u3164")) ;
        }
    }
}