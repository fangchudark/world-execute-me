public enum SimulationStyle
{
    Console,
    CSCode,
    JavaCode
}

public interface ISimulation
{
    bool IsRunning { get; }
    void Run();
    void Stop();
}