using Discord;
using Discord.WebSocket;

namespace BadKittenBot.SlashCommands;

public class TruthCommand : ISlashCommand
{
    private List<string> truths = new List<string>()
    {
        "Wie viele Jungs / Mädchen hast du bisher geküsst?",
        "Bist du im Moment verliebt?",
        "Hast du schon mal mit einer Frau / einem Mann rumgemacht?",
        "Was war dein letzter Traum an den du dich erinnerst?",
        "Hast du schon mal etwas geklaut? Wenn ja, was?",
        "Was war dein schönstes Date?",
        "Was war dein schlimmstes Date?",
        "Wen in der Runde findest du am interessantesten?",
        "Mit wem in der Runde würdest du am liebsten einen Urlaub verbringen?",
        "Hast du schon mal eine Straftat begangen?",
        "Mit wem hattest du deinen ersten Kuss?",
        "Was würdest du machen, wenn du für einen Tag ein Mann oder eine Frau wärst?",
        "Welches Tier wärst du am liebsten?",
        "Hast du schon mal illegale Drogen genommen?",
        "Wen in der Runde findest du am attraktivsten?",
        "Wonach hast du als letztes gegoogelt?",
        "Wer in der Runde ist am besten angezogen und warum?",
        "Welches ist dein aktuelles Lieblingslied?",
        "Von wem hast du deinen letzten Korb bekommen?",
        "Hast du schon mal auf dem Klo telefoniert?",
        "Was war das gemeinste, was du je getan hast?",
        "Was war das kriminellste, was du je getan hast?",
        "Hast du schon mal sexy Bilder von der verschickt?",
        "Würdest du mit jemandem knutschen, den du gerade erst kennengelernt hast?",
        "Was findest du an dir am sexiesten?",
        "Wofür hast du dich in letzter Zeit geschämt?",
        "Worauf guckst du zuerst, wenn du jemanden kennenlernst?",
        "Wie kann man dich am schnellsten von sich begeistern?",
        "Was würdest du machen, wenn du für einen Tag unsichtbar wärst?",
        "Wer in der Runde hat die schönsten Lippen?",
        "Wer in der Runde hat den schönsten Hintern?",
        "Wie sieht dein Traumdate aus?",
        "Mit wem in der Runde würdest du gerne für einen Tag das Leben tauschen?",
        "Welche Körperstellen rasierst du regelmäßig?",
        "Was war deine letzte Lüge?",
        "Welches Abenteuer würdest du gerne noch erleben?",
        "Hast du schon mal jemanden betrogen?",
        "Was würdest du an deinem Körper verändern wenn du könntest?",
        "Was würdest du machen, wenn du nur noch eine Stunde zu leben hättest?",
        "Was turnt dich bei Frauen/Männern besonders an?",
        "Was war der größte Fehler deines Lebens?",
        "Wie viele Menschen hast du schon geküsst?",
        "Welcher ist dein Lieblings-Song?",
        "Wann hattest du das letzte Mal einen Hang Over?",
        "In wen bist du verliebt?",
        "Mit welcher Person aus der Gruppe würdest du gerne mal Sex haben? ",
        "Wie oft guckst du Pornos? ",
        "Wie lange dauert es bei der Masturbation, bis du kommst? ",
        "Hand aufs Herz, bist du gut im Bett? ",
        "Wirst du besoffen manchmal aufdringlich? ",
        "Erzähl der Gruppe von einem unanständigen Traum, an den du dich erinnern kannst ",
        "Welcher Song lässt in dir die Stripperin erwachen? ",
        "Was ist ein absoluter Abturn für dich im Bett? ",
        "Hast du schon einmal einen Schwangerschaftstest gemacht bzw ein Mädchen, nachdem du mit ihr geschlafen hast? ",
        "Wurdest du schon einmal so richtig hart gekorbt? ",
        "Wie stehst du zu Fuß-Fetischen? ",
        "Könntest du dir vorstellen in einer offenen Beziehung zu sein? ",
        "Mit wie vielen Leuten hast du im letzten Jahr geschlafen? Zähl sie auf und bewerte den Sex von 1-10 ",
        "Wenn du für den Rest deines Lebens nur noch einen bestimmten Alkohol trinken dürftest, welcher wäre es? ",
        "Was wolltest du schon immer über das andere Geschlecht wissen? ",
        "Welche Sex Toys besitzt du? ",
        "Stehst du auf Anal Sex? ",
        "Hattest du schon mal eine Geschlechtskrankheit, die dir Beschwerden bereitet hat? ",
        "Hast du deinen ONS schon mal beklaut? ",
        "Wie wichtig ist die ein ausgiebiges Vorspiel? ",
        "Auf was könntest du eher verzichten: Alkohol oder Sex? ",
        "Fandest du deinen Sex-Partner schon mal so richtig eklig? ",
        "Von welchem Arbeitskollegen würdest du dich gerne mal hart durchnehmen lassen? ",
        "Frau: Was ist besser, gefingert oder geleckt werden? ",
        "Welches körperliche Merkmal findest du beim anderen Geschlecht nicht schön? ",
    };

    public string Name
    {
        get => "wahrheit";
    }

    public async void Execute(SocketSlashCommand command)
    {
        SendTruth(command);
    }

    public void SendTruth(SocketInteraction command)
    {
        var builder = new ComponentBuilder().WithButton("Wahrheit", ButtonClicks.ButtonTruth.ID)
            .WithButton("Pflicht", ButtonClicks.ButtonDare.ID);

        command.RespondAsync(command.User.Mention + " muss Plaudern:\n" +
                             truths[Random.Shared.Next(0, truths.Count)],
            components: builder.Build()
        );
    }

    public SlashCommandProperties BuildCommand(DiscordSocketClient client)
    {
        return new SlashCommandBuilder()
        {
            Name = this.Name,
            Description = "Startet eine neue Runde Wahrheit oder Pflicht",
        }.Build();
    }
}