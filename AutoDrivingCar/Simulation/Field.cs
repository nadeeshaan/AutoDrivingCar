using AutoDrivingCar.Cars;

namespace AutoDrivingCar.Simulation;

public class Field(Dimensions dimensions)
{
    private readonly Dictionary<string, Car> _cars = new();
    private Dictionary<string, List<Car>> _collisions = new();

    public Dimensions GetDimensions() => dimensions;

    public Dictionary<string, Car> Cars() => _cars;
    public Dictionary<string, List<Car>> Collisions() => _collisions;

    public void Simulate()
    {
        while (true)
        {
            var movingCars = _cars
                .Where(pair => (!_collisions.ContainsKey(pair.Key) || _collisions[pair.Key].Count == 0) &&
                               _cars[pair.Key].Moving)
                .Select(pair => pair.Value).ToList();

            if (movingCars.Count == 0)
            {
                break;
            }

            foreach (var movingCar in movingCars)
            {
                movingCar.Move();
            }

            _collisions = _cars.Values
                .GroupBy(car => car.Coordinate)
                .SelectMany(group => group.Select(car => new
                {
                    Car = car.Name,
                    Others = group
                        .Where(otherCar => !otherCar.Name.Equals(car.Name))
                        .Select(c => c)
                        .ToList()
                }))
                .ToDictionary(x => x.Car, x => x.Others);
        }
    }
}
