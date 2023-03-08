using BadKittenBot.Modals;
using Discord;
using Discord.WebSocket;

namespace BadKittenBot.SlashCommands;

public class StrikeCommand : ISlashCommand
{
    public static string              reasonFieldID = "D1E273E8-C9F5-4235-BA24-931FD2EE4ABC";
    private       DiscordSocketClient _client;

    public string Name
    {
        get => "strike";
    }

    public async void Execute(SocketSlashCommand command)
    {
        IReadOnlyCollection<SocketGuildUser> readOnlyCollection = _client.GetGuild((ulong)command.GuildId!).Users;
        ModalBuilder                         modalBuilder       = new ModalBuilder();
        modalBuilder.Title    = "Gestraft sei er";
        modalBuilder.CustomId = StrikeModal.ModalID;
        modalBuilder.AddTextInput("Vergehen/Begründung", reasonFieldID, TextInputStyle.Paragraph, required: true, minLength: 5);
        Modal build = modalBuilder.Build();
        await command.RespondWithModalAsync(build);
    }

    public SlashCommandProperties BuildCommand(DiscordSocketClient client)
    {
        _client = client;
        return new SlashCommandBuilder()
        {
            Name        = Name,
            Description = "Haue einem Mitglied auf die Finger",
            Options = new List<SlashCommandOptionBuilder>()
            {
                new SlashCommandOptionBuilder()
                {
                    Name        = "user",
                    Description = "Benutzer der gestraft werden soll",
                    Type        = ApplicationCommandOptionType.User,
                    IsRequired  = true
                }
            }
        }.Build();
    }
}