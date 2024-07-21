using AutoDrivingCar.Cars;

namespace AutoDrivingCar.Simulation.State;

public interface ISimulationState
{
    public string Prompt(ISimulation simulation);

    public string Process(ISimulation simulation, string userInput);

    public bool WaitForUserInput() => true;

    protected static string InvalidInput(string input) => $"Invalid input {input} found";
}

internal static class CarsExtension
{
    public static string ListCars(this IEnumerable<ICar> cars) =>
        string.Join(Environment.NewLine, cars.Select(car =>
            $"- {car.Name()}, ({car.Coordinate().X}, {car.Coordinate().Y}) {car.Facing()}, {car.Commands()}"));

    public static string PositionInfo(this IEnumerable<ICar> cars) =>
        string.Join(Environment.NewLine, cars.Select(car =>
            $"- {car.Name()}, ({car.Coordinate().X}, {car.Coordinate().Y}) {car.Facing()}"));

    public static string CollisionInfo(this Dictionary<string, List<ICar>> collisions)
    {
        return string.Join(Environment.NewLine, collisions.Select(car =>
            $"- {car.Key}, collides with {string.Join(",", car.Value.Select(otherCar => otherCar.Name()))} at {car.Value[0].CoordinateLiteral()} at step {car.Value[0].Step()}"));
    }

    public static string CoordinateLiteral(this ICar car) => $"({car.Coordinate().X}, {car.Coordinate().Y})";
}

internal enum CarOptionsSetting
{
    Name,
    Coordinates,
    Commands,
    Complete
}
