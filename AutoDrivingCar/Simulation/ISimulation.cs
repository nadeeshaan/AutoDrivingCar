using AutoDrivingCar.Cars;
using AutoDrivingCar.Simulation.State;

namespace AutoDrivingCar.Simulation;

public interface ISimulation
{
    public void SetState(ISimulationState simulationState);

    public ISimulationState GetState();

    public void SetField(int x, int y);

    public IEnumerable<ICar> GetCars();

    public bool ContainsCar(string name);

    public Dictionary<string, List<ICar>> GetCollisions();

    public void AddCar(string name, Coordinate coordinate, string facing, string commands);

    public void Simulate();

    public string Prompt();

    public string Process(string userInput);

    public bool WaitForUserInput();
}
