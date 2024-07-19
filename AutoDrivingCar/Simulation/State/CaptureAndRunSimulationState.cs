namespace AutoDrivingCar.Simulation.State;

public class CaptureAndRunSimulationState : ISimulationState
{
    public string Prompt(ISimulation simulation) =>
        """

        Please choose from the following options:
        [1] Add a car to field
        [2] Run simulation
        """;

    public string Process(ISimulation simulation, string userInput)
    {
        if (int.TryParse(userInput, out var option) && option is 1 or 2)
        {
            simulation.SetState(option is 1 ? new AddCarState() : new RunSimulationState());
            return "";
        }

        simulation.SetState(new CaptureAndRunSimulationState());
        return $"Invalid input {userInput} found";
    }
}
