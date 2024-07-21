namespace AutoDrivingCar.Simulation.State;

public class InitialState : ISimulationState
{
    public string Prompt(ISimulation simulation) =>
        """
        Welcome to Auto Driving Car Simulation!

        Please enter the width and height of the simulation field in x y format:
        """;

    public string Process(ISimulation simulation, string userInput)
    {
        var dimensions = userInput.Split((char[])null, StringSplitOptions.RemoveEmptyEntries)
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Take(2)
            .ToArray();

        if (int.TryParse(dimensions[0], out var xCord) && int.TryParse(dimensions[1], out var yCord))
        {
            simulation.SetField(xCord, yCord);
            simulation.SetState(new AddCarOrRunSimulationState());

            return $"You have created a field of {xCord} x {yCord}.";
        }

        simulation.SetState(new InitialState());
        return ISimulationState.InvalidInput(userInput);
    }
}
