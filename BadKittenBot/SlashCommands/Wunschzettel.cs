using System.Text;
using Discord;
using Discord.WebSocket;

namespace BadKittenBot.SlashCommands;

public class Wunschzettel : ISlashCommand
{
    private DiscordSocketClient _client;

    public string Name
    {
        get => "wuschzettel";
    }

    public async void Execute(SocketSlashCommand command)
    {
        
    }

    public SlashCommandProperties BuildCommand(DiscordSocketClient client)
    {
        _client = client;
        Discord.SlashCommandBuilder bulider = new Discord.SlashCommandBuilder()
        {
            Name = this.Name,
            Description = "Gibt eine Liste mit allen Usern des Servers",
        };
        return bulider.Build();
    }
}