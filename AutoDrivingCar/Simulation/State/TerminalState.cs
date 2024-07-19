namespace AutoDrivingCar.Simulation.State;

public class TerminalState : ISimulationState
{
    public string Prompt(ISimulation simulation) => "";

    public string Process(ISimulation simulation, string userInput) => "";
}
