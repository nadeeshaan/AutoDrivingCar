namespace AutoDrivingCar.State;

public class InitialState(Context context) : SimulationState(context)
{
    public override string Prompt()
    {
        return """
               Welcome to Auto Driving Car Simulation!

               Please enter the width and height of the simulation field in x y format:
               """;
    }

    public override string Process(string userInput)
    {
        var dimensions = userInput.Split((char[])null, StringSplitOptions.RemoveEmptyEntries)
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Take(2)
            .ToArray();

        if (int.TryParse(dimensions[0], out var xCord) && int.TryParse(dimensions[1], out var yCord))
        {
            Context.Simulator.SetField(xCord, yCord);
            Context.State = new AddCarOrRunSimulationState(Context);

            return $"You have created a field of {xCord} x {yCord}.";
        }

        Context.State = new InitialState(Context);
        return ISimulationState.InvalidInput(userInput);
    }
}
