namespace AutoDrivingCar.State;

public class RestartOrExitState(Context context) : SimulationState(context)
{
    private const int StartOverOption = 1;
    private const int ExitOption = 2;

    public override string Prompt()
    {
        return """
               Please choose from the following options:
               [1] Start over
               [2] Exit
               """;
    }

    public override string Process(string userInput)
    {
        if (int.TryParse(userInput, out var option) && option is StartOverOption or ExitOption)
        {
            Context.State = option is StartOverOption ? new InitialState(Context) : new TerminalState(Context);
            return option is ExitOption ? "Thank you for running the simulation. Goodbye!" : "";
        }

        Context.State = new RestartOrExitState(Context);
        return ISimulationState.InvalidInput(userInput);
    }
}
