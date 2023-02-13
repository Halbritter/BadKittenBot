namespace BadKittenBotBackgroundWorker.Tasks;

public interface ITimerTask
{
    public TimerType TimerType { get; }
    void Execute();
}