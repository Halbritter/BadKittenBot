// See https://aka.ms/new-console-template for more information

using System.Runtime.Serialization;
using BadKittenBotBackgroundWorker.Tasks;
using Discord.WebSocket;

namespace BadKittenBotBackgroundWorker;

internal class Program
{
    public static async Task Main(string[] args)
    {
        Log("Start Backround Task!");


        DiscordSocketClient discordSocketClient = await BadKittenBot.Program.CreateClient();
        Thread.Sleep(10000);

        TimerTaskHandler th = new TimerTaskHandler(discordSocketClient);

        switch (args[0])
        {
            case "-h":
                Log("Start Hourly Task");
                th.Exceute(TimerType.Hourly);
                Log("Finished Hourly Task");
                break;
            case "-d":
                Log("Start Daily Task");
                th.Exceute(TimerType.Hourly);
                Log("Finished Daily Task");
                break;
            case "-w":
                Log("Start Weekly Task");
                th.Exceute(TimerType.Hourly);
                Log("Finished Weekly Task");
                break;
            case "-m":
                Log("Start Manual Task");
                th.Exceute(TimerType.Hourly);
                Log("Finished Manual Task");
                break;
        }
    }

    public static void Log(string str)
    {
        IFormatProvider now = new DateTimeFormat("dd.MM.yyyy mm:hh:ss").FormatProvider;
        Console.WriteLine(DateTime.Now.ToString(now) + " " + str);
    }
}