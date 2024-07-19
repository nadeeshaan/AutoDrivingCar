namespace AutoDrivingCar.Simulation.State;

public class RestartOrExitState : ISimulationState
{
    public string Prompt(ISimulation simulation) =>
        """
        Please choose from the following options:
        [1] Start over
        [2] Exit
        """;

    public string Process(ISimulation simulation, string userInput)
    {
        if (int.TryParse(userInput, out var option) && option is 1 or 2)
        {
            simulation.SetState(option is 1 ? new InitialState() : new TerminalState());
            return option is 2 ? "Thank you for running the simulation. Goodbye!" : "";
        }

        simulation.SetState(new RestartOrExitState());
        return $"Invalid input {userInput} found";
    }
}
