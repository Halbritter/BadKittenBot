using Discord;
using Discord.WebSocket;

namespace BadKittenBot.SlashCommands;

public class DareCommand : ISlashCommand
{
    public static readonly string DareID = "81432d5c-304c-4f2c-8805-ba622ef57473";

    private List<string> dares = new List<string>()
    {
        "Haue deinen Pullermann auf den Tisch",
        "Mach mit deinen Boobis Milch"
    };

    public string Name
    {
        get => "pflicht";
    }

    public async void Execute(SocketSlashCommand command)
    {
        var builder = new ComponentBuilder()
            .WithButton("Wahrheit", TruthCommand.TruthID).WithButton("Pflicht", DareID);
        command.RespondAsync(
            dares[Random.Shared.Next(0, dares.Count - 1)],
            components: builder.Build()
        );
    }

    public SlashCommandProperties BuildCommand(DiscordSocketClient client)
    {
        return new SlashCommandBuilder()
        {
            Description = "Startet eine neue Runde Wahrheit oder Pflicht",
        }.Build();
    }
}