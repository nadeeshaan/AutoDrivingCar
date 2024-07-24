using AutoDrivingCar.Cars;

namespace AutoDrivingCar.State;

public interface ISimulationState
{
    public string Prompt();

    public string Process(string userInput);

    public bool WaitForUserInput();

    protected static string InvalidInput(string input) => $"Invalid input {input} found";
}

internal static class CarsExtension
{
    public static string ListCars(this IEnumerable<Car> cars) =>
        string.Join(Environment.NewLine, cars.Select(car =>
            $"- {car.Name}, ({car.Coordinate.X}, {car.Coordinate.Y}) {car.Facing}, {car.Commands}"));

    public static string PositionInfo(this IEnumerable<Car> cars) =>
        string.Join(Environment.NewLine, cars.Select(car =>
            $"- {car.Name}, ({car.Coordinate.X}, {car.Coordinate.Y}) {car.Facing}"));

    public static string CollisionInfo(this Dictionary<string, List<Car>> collisions)
    {
        return string.Join(Environment.NewLine, collisions.Select(car =>
            $"- {car.Key}, collides with {string.Join(",", car.Value.Select(otherCar => otherCar.Name))} at {car.Value[0].CoordinateLiteral()} at step {car.Value[0].CurrentStep}"));
    }

    public static string CoordinateLiteral(this Car car) => $"({car.Coordinate.X}, {car.Coordinate.Y})";
}
