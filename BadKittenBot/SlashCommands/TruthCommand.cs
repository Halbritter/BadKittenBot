using Discord;
using Discord.WebSocket;

namespace BadKittenBot.SlashCommands;

public class TruthCommand : ISlashCommand
{
    public static readonly string TruthID = "c9ec6dcd-be91-4cf3-951d-f6f02e6240c4";

    private List<string> truths = new List<string>()
    {
        "Bist du ein Opfer?",
        "Wie lange ist dein Rüssel?"
    };

    public string Name
    {
        get => "wahrheit";
    }

    public async void Execute(SocketSlashCommand command)
    {
        var builder = new ComponentBuilder().WithButton("Wahrheit", TruthID).WithButton("Pflicht", DareCommand.DareID);

        command.RespondAsync(
            truths[Random.Shared.Next(0, truths.Count - 1)],
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