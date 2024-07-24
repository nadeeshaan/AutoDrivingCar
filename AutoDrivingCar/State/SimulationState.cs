namespace AutoDrivingCar.State;

public abstract class SimulationState(Context context) : ISimulationState
{
    protected readonly Context Context = context;
    public abstract string Prompt();
    public abstract string Process(string userInput);
    public virtual bool WaitForUserInput() => true;
}
