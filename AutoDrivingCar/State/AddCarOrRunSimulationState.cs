namespace AutoDrivingCar.State;

public class AddCarOrRunSimulationState(Context context) : SimulationState(context)
{
    private const int AddCarInputOption = 1;
    private const int RunSimulationInputOption = 2;

    public override string Prompt()
    {
        return """

               Please choose from the following options:
               [1] Add a car to field
               [2] Run simulation
               """;
    }

    public override string Process(string userInput)
    {
        if (int.TryParse(userInput, out var option) && option is AddCarInputOption or RunSimulationInputOption)
        {
            Context.State = option is AddCarInputOption ? new AddCarState(Context) : new RunSimulationState(Context);
            return string.Empty;
        }

        Context.State = new AddCarOrRunSimulationState(Context);
        return ISimulationState.InvalidInput(userInput);
    }
}
