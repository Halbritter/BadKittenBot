using System.Reflection;
using Discord.WebSocket;

namespace BadKittenBotBackgroundWorker.Tasks;

public class TimerTaskHandler
{
    private readonly DiscordSocketClient _client;
    private readonly List<ITimerTask>    _tasks;

    public TimerTaskHandler(DiscordSocketClient client)

    {
        _client = client;
        _tasks  = new List<ITimerTask>();
        IEnumerable<Type> _types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "BadKittenBotBackgroundWorker.Tasks")
            .Where(c => c.IsClass && c.IsAssignableTo(typeof(ITimerTask)));
        foreach (Type nestedType in _types)
        {
            ITimerTask? instance = Activator.CreateInstance(nestedType, client) as ITimerTask;
            _tasks.Add(instance);
            Console.WriteLine($"Loaded {instance.TimerType} TimerTask");
        }
    }

    public void Exceute(TimerType type)
    {
        foreach (ITimerTask task in _tasks)
        {
            if (task.TimerType == type)
                task.Execute();
        }
    }
}