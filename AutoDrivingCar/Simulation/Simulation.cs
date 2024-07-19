using AutoDrivingCar.Cars;
using AutoDrivingCar.Simulation.State;

namespace AutoDrivingCar.Simulation;

public class Simulation : ISimulation
{
    private ISimulationState _simulationState;

    private Field _field;

    public Simulation()
    {
        _simulationState = new InitialState();
    }

    public void SetState(ISimulationState simulationState) => _simulationState = simulationState;

    public ISimulationState GetState() => _simulationState;

    public void SetField(int x, int y) => _field = new Field(new Dimensions(x, y));

    public IEnumerable<ICar> GetCars() => _field.Cars().Values;

    public Dictionary<string, List<ICar>> GetCollisions() => _field.Collisions();

    public void AddCar(string name, Coordinate coordinate, string facing, string commands) =>
        _field.Cars().Add(name, new Car(name, coordinate, facing, commands, _field.GetDimensions()));

    public void Simulate() => _field.Simulate();

    public string Prompt() => _simulationState.Prompt(this);

    public string Process(string userInput) => _simulationState.Process(this, userInput);

    public bool WaitForUserInput() => _simulationState.WaitForUserInput();
}
