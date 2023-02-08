using Discord;
using Discord.WebSocket;

namespace BadKittenBot;

public static class Utils
{
    public static Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }

    public static object? GetValueFromOption(this SocketSlashCommand command, string name)
    {
        IApplicationCommandInteractionDataOption? applicationCommandInteractionDataOption = command.Data.Options.Where(o => o.Name == name).FirstOrDefault(defaultValue: null);
        if (applicationCommandInteractionDataOption is null)
        {
            return null;
        }

        return applicationCommandInteractionDataOption.Value;
    }
}