namespace AutoDrivingCar.State;

using Simulation;

public class Context
{
    public Context(Simulator simulator)
    {
        State = new InitialState(this);
        Simulator = simulator;
    }

    public ISimulationState State { get; set; }

    public Simulator Simulator { get; }

    public string Prompt() => State.Prompt();

    public string Process(string userInput) => State.Process(userInput);

    public bool WaitForUserInput() => State.WaitForUserInput();
}
