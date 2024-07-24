namespace AutoDrivingCar.State;

public class TerminalState(Context context) : SimulationState(context)
{
    public override string Prompt() => "";

    public override string Process(string userInput) => "";
}
