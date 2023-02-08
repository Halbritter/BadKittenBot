﻿using System.Text;
using BadKittenBot.ButtonClicks;
using Discord;
using Discord.WebSocket;

namespace BadKittenBot.SlashCommands;

public class GetUserMissingRoleCommand : ISlashCommand
{
    private DiscordSocketClient _client;

    public string Name
    {
        get => "get_member_missing";
    }

    public async void Execute(SocketSlashCommand command)
    {
        if (!((IGuildUser)command.User).GuildPermissions.Administrator)
        {
            throw new Exception("Du darfst diesen Befehl nicht ausführen");
        }


        var msg = command.DeferAsync(ephemeral: true);

        IRole?      rolle       = command.GetValueFromOption("rolle") as IRole;
        bool        includeBots = (bool)(command.GetValueFromOption("include_bots") ?? false);
        SocketGuild guild       = _client.GetGuild((ulong)command.GuildId!);

        IAsyncEnumerable<IReadOnlyCollection<IGuildUser>> asyncEnumerable = guild.GetUsersAsync();

        List<IGuildUser> missingUser = new List<IGuildUser>();

        await foreach (var userCollection in asyncEnumerable)
        {
            StringBuilder b = new StringBuilder();
            foreach (var user in userCollection)
            {
                if (!includeBots)
                {
                    if (user.IsBot) continue;
                }

                if (!user.RoleIds.Contains(rolle.Id))
                {
                    b.Append("Fehlt: ");
                    b.Append(user.DisplayName);
                    b.Append(" gejoint vor ");
                    b.Append(Math.Round((DateTime.Now - user.JoinedAt).Value.TotalHours));
                    b.Append(" Stunden");
                    b.AppendLine();
                    missingUser.Add(user);
                }
            }

            ComponentBuilder bu = new ComponentBuilder();

            List<ActionRowBuilder> abrList = new List<ActionRowBuilder>();
            for (int j = 0; j < 5; j++)
            {
                ActionRowBuilder abr = new ActionRowBuilder();
                for (int k = 0; k < 5; k++)
                {
                    int        index     = j * 5 + (k + 1) - 1;
                    IGuildUser guildUser = missingUser[index];
                    abr.WithButton("Kick: " + guildUser.DisplayName, customId: ButtonKick.ID + ":" + guildUser);
                }

                abrList.Add(abr);
            }

            bu.WithRows(abrList);
            command.FollowupAsync("Ich konnte folgende Benutzer nicht finden\n(es werden nur die ersten 25 angezeigt)", components: bu.Build(), ephemeral: true);
        }
    }

    public SlashCommandProperties BuildCommand(DiscordSocketClient client)
    {
        _client = client;
        Discord.SlashCommandBuilder bulider = new Discord.SlashCommandBuilder()
        {
            Name        = this.Name,
            Description = "Gibt eine Liste mit Benutzern die nicht in der Gruppe sind",
            Options = new List<SlashCommandOptionBuilder>()
            {
                new SlashCommandOptionBuilder()
                {
                    Name        = "rolle",
                    Description = "Die zu prüfende Rolle",
                    Type        = ApplicationCommandOptionType.Role,
                    IsRequired  = true
                },
                new SlashCommandOptionBuilder()
                {
                    Name        = "include_bots",
                    Description = "Sollen Bots mit in die Auflistung einbezogen werden? (Standard ist nein)",
                    Type        = ApplicationCommandOptionType.Boolean,
                    IsRequired  = false,
                }
            }
        };
        return bulider.Build();
    }
}