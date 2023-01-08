using Discord;
using Discord.WebSocket;

namespace BadKittenBot.SlashCommands;

public class DareCommand : ISlashCommand
{
    private List<string> dares = new List<string>()
    {
        "Küss deinen linken/rechten Nachbarn auf die Wange oder den Mund.",
        "Zeig uns deine Google Suchhistorie.",
        "Imitiere einen brunftigen Hirsch.",
        "Tausche dein Oberteil mit dem Spieler links/rechts von dir.",
        "Leg dich in die Mitte, schließ die Augen und lass dich eine Minute lang von allen kitzeln.",
        "Trinke einen Shot (wenn du Erwachsen bist).",
        "Mach 10 Liegestütze.",
        "Mach ein Selfie von der Gruppe und poste es auf Instagram/Facebook/Snapchat.",
        "Mach der Person links/rechts von dir ein ernstgemeintes Kompliment.",
        "Teste wessen Haare am besten riechen.",
        "Imitiere eine Person aus der Runde. Die anderen müssen erraten, wer es ist.",
        "Trink das Getränk von dem Spieler links/rechts von dir auf ex.",
        "Tanze 60 Sekunden sexy zu einem Lied.",
        "Beleidige die Person links/rechts von dir.",
        "Schick einer Person per Whatsapp ein Kompliment.",
        "Zeig allen ein peinliches Foto von dir.",
        "Zeig allen deine letzte Whatsapp.",
        "Zeig allen das letzte Foto was du geschossen hast.",
        "Umarme die Person gegenüber so herzlich wie möglich.",
        "Flirte eine Runde mit der Person links/rechts von dir.",
        "Erzähl der Person gegenüber, was du besonders attraktiv an ihr findest.",
        "Schrei 20 schlimme Schimpfwörter.",
        "Nimm einen Eiswürfel und steck ihn dir in die Hose.",
        "Schick jemandem der nicht mitspielt ein Bild von deinen nackten Füßen.",
        "Schütte dir einen Drink ein, ohne die Hände zu benutzen.",
        "Ruf die fünft letzte Nummer die du gewählt hast an, frag die Person wie das folgende Lied heißt und sing es ihr vor, bis sie den Titel nennt.",
        "Du bist für eine Runde der Buttler. Achte darauf, dass alle Gäste immer genug zu trinken haben und behandle sie mit ausreichend Respekt.",
        "Du hast dich schlecht verhalten. Stell dich eine Runde in die Ecke und schäm dich.",
        "Lass einen Spieler an deiner Achsel riechen.",
        "Tausche dein T-Shirt mit der Person rechts neben dir.",
        "Gib deinem linken Nachbarn dein Handy und lass ihn in deine Foto-Galerie schauen.",
        "Lege einen Moonwalk wie Michael Jackson hin.",
        "Laufe wie bei Germany's Next Topmodel über den Catwalk.",
        "Jaule 30 Sekunden lang wie ein Wolf - ohne zu lachen!",
        "Für Alle: Nehmt jeder ein Glas und füllt es mit der Menge Flüssigkeit ab, die eurer Meinung nach einem Samenerguss entspricht.",
        "Zieh dir ein Kleidungsstück aus.",
        "Schneide der Person rechts von dir einige Schamhaare ab.",
        "Nenne 10 Synonyme für das weibliche Geschlecht.",
        "Mach den Eskimokuss (Nase an Nase reiben) mit der Person rechts von dir. ",
        "Tausche den Platz mit jemanden.",
        "Zieh dir die Hose aus, häng dir ein Handtuch um den Bauch und mach den berühmten Schwimm-/Unterhose Austausch.",
        "Lass dich von einem oder mehreren Mitspielern fesseln. Sie bestimmen, was mit dir passieren wird.",
        "Iss eine Frucht auf eine erotische Art und Weise.",
        "Nimm  einen Schluck aus deinem Glas, lass ihn in den Bauchnabel der Person rechts von dir tropfen und Leck ihn wieder auf.",
        "Führe einen Striptease auf. Gehe dabei so wiet, wie du möchtest.",
        "Geschicklichkeit: Halte deine Hände auf dem Rücken und versuche mit deinen Zähnen das Oberteil der Person rechts von dir zu öffnen bzw. ausziehen.",
        "Nimm einen Besen o.ä und zeige den anderen, was ein \"Stangentanz\" ist.",
        "Spielt eine \"Miss Wet-T-Shirt\"-Wahl nach.",
        "Gib der Person rechts von dir für mindestens 10 Sekunden einen Kuss auf den Mund.",
        "Zieh deine Schuhe aus und Liebkose mit deinen Füssen die Füsse deiner dir gegenübersitzenden Person.",
        "Versuche mit einer Hand den BH deiner linken Sitznachbarin zu lösen.",
        "Stell dich vor deine/n Partner/in und bring ihm/ihr ein Ständchen.",
        "Trinke ein Glas Sekt oder ähnliches in einem Zug aus.",
        "Leg dich auf den Tisch und Ruf:\"Nimm, Mich!\"",
        "Gib der Person Links von dir einen Knutschfleck auf ein Körperteil deiner Wahl.",
        "Ertaste den Brustumfang deiner linken Nachbarn und errate wei viele cm das sind. ",
        "\"Tätowiere\" mit einem Kuli eine Figur auf die Brust der Person links von dir.",
        "Tausche ein Kleidungsstück mit der Person Rechts von dir.",
        "Beschreib drei Sexuelle Praktiken, die dich reizen.",
        "Küss die zweite Person rechts von dir vom Hals an nach unten (auf die Haut selbstverständlich). Wie weit traust du dich zu gehen (oder wie weit darfst du gehen)?",
        "Nimm einen Eiswürfel und mach etwas unanständiges damit.",
        "Streichle die Person links von dir über den Körper.",
        "Führe einen Paarungstanz vor, wie du ihn dir vorstellst.",
        "Streichle mit deiner Hand bei der Person links von dir vom Fuss an nach oben bis sie halt sagt.",
        "Befühle den Hintern der Person rechts von dir und errate die Unterhosengröße.",
        "Für Alle: Nehmt einen schluck eurer Drinks und zeigt mit einem Strahl, wie weit ihr oder euer Partner Ejakuliert.",
        "Für Alle: Erratet die Unterwäsche der anderen.",
        "Spritz bei der Person links von dir Schlagsahne auf die entblösste Brust und lecke sie weider ab.",
        "Befühle die Brüste deiner rechten Nachbarin und errate  ihre BH-Größe.",
        "Nimm den Zeigefinger der Person rechtsvon dir in den Mund und Simuliere ein Orales Zungenspiel (Blasen oder Lecken).",
        "Male deine Lippen mit Lippenstift an und küsse die Stirn der Person rechts von dir.",
        "Für Alle: Zeichne dein GLied oder das deines Partners in entspanntem und erregtem zustand auf ein Blatt Pappier.",
        "Leg dich auf deinen Bauch, enblösse deinen hintern und lass einen Mitspieler aus 60 cm höhe Kerzenwachs auf ihn träufeln.",
        "Zupfe deinem rechten Nachbarn ein Brusthaar aus.",
        "Nuckel erotisch am Ohrläppchen der Person links von dir.",
        "Enthaare einen Teil der Wade der Männlichen Person links von der mit einem Klebestreifen.",
        "Krieche wie eine rollige Katze durchs Wohnzimmer.",
        "Spiel eine heiße (Musik-) Nummer ab und Tanze Intim mit deinem Partner.",
        "Küsse und lecke zärtlich den Hals deines Partners.",
        "Setze das Spiel untertänig Fort, indem du jede Antwort auf eine Frage deiner Mitspieler mit \"Meister\" enden lässt.",
        "Führe einen Bauchtanz auf.",
        "Nimm eine Süßigkeit in den Mund und gib sie an dei Person rechts von dir weiter. Diese gibt sie ebenfalls weiter, bis sie weider zu dir zurück kommt.",
        "Führe deine Lieblingsposition beim Sex vor.",
        "Zeige den Gesichtsausdruck deines Partners wenn er kommt.",
        "Die dir Gegenübersitzende Person versteckt unter ihrer Kleidung eine Münze. Du musst sie mit verbundenen Augen suchen.",
        "Täusche durch stöhnen einen Orgasmus vor.",
        "Geschicklichkeit: Stelle ein Leeres Glas bei der Person links von dir in die Hose oder zwischen die Brüste und wirf aus etwa 3 m entfernung eine Erdnuss o.ä. hinein.",
        "Streichle in einer erotischen Art durch die Haare der Person Links von dir.",
        "Imitiere einen Exhibitionisten.",
        "Schreib auf einen Zettel: \" Schatz, ich will dich!\" und gib ihn der dir  Gegenübersitzenden Person.",
        "Nenn 10 Synonyme für das Männliche Glied.",
        "Geh nach draußen und schrei: \"Schatz, ich bin betrunken, aber ich liebe dich!\"",
        "Küsse deinen Partner an einer Stelle, die du dir aussuchen darfst.",
        "Lass dir die Augen verbinden und dich von deinem Partner mit einer Speise seiner Wahl füttern. Er darf sie dir nur mit dem Mund reichen.",
        "Tanze für deinen Partner einen heißen Stripteas. ",
        "Verwöhne deinen Partner mit einer kurzen Massage an einer Stelle seiner Wahl.",
        "Mache deinen Partner in einer für ihn typischen Situation nach. ",
        "Küsse deinen Partner an einer Stelle, die er sich aussuchen darf.",
        "Tanze deinem Partner vor, was du dir für eure gemeinsame Zukunft wünschst.",
        "Knutscht! Mindestens eine Minute - ihr dürft gerne auch länger.",
        "Überlege dir einen Pornotitel für euer Sexleben.",
        "Macht den ultimativen Vertrauenstest, indem du dich nach hinten fallen und von deinem Partner auffangen lässt.",
        "Tauscht eure Kleidung.",
        "Flüstere deinem Schatz schmutzige Kosenamen in Ohr, ohne zu lachen.",
        "Wirf deinem Partner deinen verführerischsten Blick zu.",
        "Schlage für jedes Jahr/jeden Monat, das/den ihr zusammen seid, einen Purzelbaum.",
        "Tigere wie eine sexy Raubkatze durchs Zimmer.",
        "Du bist eine französische Prostituierte-tu was dir dazu einfällt.",
        "Macht Armdrücken. Wer gewinnt, darf sich vom anderen etwas Wünschen.",
        "Mach vor, wie du deinen Partner in einer Bar anbaggern würdest, wenn du ihn noch nicht kennen würdest.",
        "Wo wärst du heute, wenn du deinen Partner nicht kennengelernt hättest? Male diese Vorstellung in den dunkelsten Farben aus.",
        "Lasse dir von deinem Partner Zöpfchen flechten.",
        "Rufe aus dem Fenster, wie sehr du deinen Partner liebst.",
        "Setze dich für die nächsten fünf Runden auf dem Schoß deines Partners.",
        "Küsse deinen Partner auf die Stelle seines Körpers, die du an ihm am erotischsten findest.",
        "Mache deinem Partner eine Liebeserklärung.",
        "Kraule deinem Partner den Rücken - mit deinen Füßen.",
        "Mache deinen Schwiegervater oder deine Schwiegermutter nach. Erkennt dein Partner, wer du bist?",
        "Verpasse deinem Partner einen Knutschfleck.",
        "Lasse dir von deinem Partner mit Edding an einer gut sichtbaren Stelle ein Herztattoo verpassen, in dem sein Name steht.",
        "Singe ein schmalziges Lied darüber, warum dein Partner die Liebe deines Lebens ist. ",
        "Verwuschle das Haar deines Partners so, dass es aussieht wie nach einer wilden Nacht.",
        "Mache für jedes Jahr/jeden Monat, das/den ihr zusammen seid, einen Liegestütz.",
        "Knutscht so übertrieben und feucht, wie ihr könnt.",
        "Verrate deinem Partner irgendetwas,, das er noch nicht über dich weiß.",
        "Rieche am Haar deines Partners-wonach riecht es? PS: Einfach nur \"Schampoo\" ist keine Antwort!",
        "Lecke deinem Partner über die Wange.",
        "Lasse dich von deinem Partner 30 Sekunden kitzeln.",
        "Spiele eine Szene aus einem Liebesfilm nack. Erkennt dein Partner, welche es ist?",
        "Lasse dich von deinem Partner schminken.",
        "Schenke deinem Partner deine Unterhose-und zwar die, die du gerade trägst!",
        "Tritt gegen deinen Partner zum Ringkampf an. Wer liegt als Erster auf dem Rücken?",
        "Liege für die nächsten fünf Minuten deinem Partner zu Füßen-ihr dürft in der Zwischenzeit weiterspielen.",
        "Erzähle einen schmutzigen Witz.",
        "Vollende folgenden Satz: \"Bevor ich meinen Partner kennengelernt habe,...\"",
        "Poste ein Foto von euch. Kommentiere es nur mit einem Kindernamen und einem sechs Monate in der Zukunft liegenden Datum.",
        "Zeichne ein Bild von euch beiden beim Sex.",
        "Iss etwas, ohne die Hände zu benutzen-der Bauch deines Partners ist der Teller.",
        "Dein Partner war unartig? Schlage ihm auf den Po!",
        "Dichte für deinen Partner ein Gedicht, in dem die Worte \"Diebe\", \"Herz\" und \"Kartoffelsalat\" vorkommen. Du hast eine Minute!",
        "Gib den Romeo und schreie liebeskrank nach deiner Julia-so laut du kansnt.",
        "Mache deinem Schatz eine wütende Szene. Thema? Frei wählbar.",
        "Finde zu jedem Buchstaben im Namen deines Partners eine positive Eigenschaften. Ein Beispiel? Ben-Bärenstark, einfühlsam, nackt...",
        "Gurgle \" I Will Always Love You\". Erkennt dein Partner, welches Lied das ist?",
        "Spiele eure erste Begegnung nach-du übernimmst einfach beide Rollen.",
        "Mache deinem Partner ein originelles Kompliment.",
        "Schreib mit deinem Finger einen Kosenamen auf den Rücken deines Partners. Erkennt er, was du geschrieben hast?",
        "Lasse dir von deinem Partner die Fingernägel lackieren.",
        "Kannst du dich verbiegen wie eine Brezel? Versuche, deinen Zeh in den Mund zu nehmen.",
        "Lasse dir die Augen verbinden und ertaste und erkunde den Körper deines Partners blind.",
        "Gibt es eien Stelle am Körper deines Partners, die du noch nie berührt hast? Berühre sie-wenn er das möchte.",
        "Nimm deinen Partner huckepack und trage ihn einmal durch den Raum.",
        "Knie vor deinem Partner nieder und schmettere eine Liebesballade.",
        "Zeige deinem Partner das letzte Bild, das du über dein Smartphone an eine andere Person als ihn verschickt hast."
    };

    public string Name
    {
        get => "pflicht";
    }

    public async void Execute(SocketSlashCommand command)
    {
        SendDare(command);
    }

    public void SendDare(SocketInteraction command)
    {
        var builder = new ComponentBuilder()
            .WithButton("Wahrheit", ButtonClicks.ButtonTruth.ID).WithButton("Pflicht", ButtonClicks.ButtonDare.ID);
        command.RespondAsync("Auf geht es " + command.User.Mention + "\n" +
                             dares[Random.Shared.Next(0, dares.Count)],
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