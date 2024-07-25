using AutoDrivingCar.Cars;

namespace AutoDrivingCar.Simulation;

public class Field(Dimensions dimensions)
{
    private Dictionary<string, List<Car>> _collisions = new();
    private readonly HashSet<Car> _cars = [];

    public Dimensions GetDimensions() => dimensions;
    public void AddCar(Car car) => _cars.Add(car);
    public HashSet<Car> Cars() => _cars;
    public Dictionary<string, List<Car>> Collisions() => _collisions;

    public void Simulate()
    {
        while (true)
        {
            // A car is movable, if it can accept further commands and does not have collisions
            var movableCars = _cars
                .Where(car => car.CanAcceptCommands &&
                              (!_collisions.ContainsKey(car.Name) || _collisions[car.Name].Count == 0))
                .ToList();

            if (movableCars.Count == 0)
            {
                break;
            }

            foreach (var movingCar in movableCars)
            {
                movingCar.Move();
            }

            // Capture and store the collisions
            _collisions = _cars
                .GroupBy(car => car.Coordinate)
                .SelectMany(group => group.Select(car => new
                {
                    Car = car.Name,
                    Others = group
                        .Where(otherCar => !otherCar.Name.Equals(car.Name))
                        .Select(otherCar => otherCar)
                        .ToList()
                }))
                .ToDictionary(entry => entry.Car, entry => entry.Others);
        }
    }
}
