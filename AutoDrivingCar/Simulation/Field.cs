using AutoDrivingCar.Cars;

namespace AutoDrivingCar.Simulation;

public class Field(Dimensions dimensions)
{
    private readonly Dictionary<string, ICar> _cars = new();
    private Dictionary<string, List<ICar>> _collisions = new();

    public Dimensions GetDimensions() => dimensions;

    public void AddCar(string name, int xCord, int yCord, string facing, string command) =>
        _cars.Add(name, new Car(name, new Coordinate(xCord, yCord), facing, command, dimensions));

    public Dictionary<string, ICar> Cars() => _cars;
    public Dictionary<string, List<ICar>> Collisions() => _collisions;

    public void Simulate()
    {
        while (true)
        {
            var movingCars = _cars
                .Where(pair => (!_collisions.ContainsKey(pair.Key) || _collisions[pair.Key].Count == 0) &&
                               _cars[pair.Key].Moving())
                .Select(pair => pair.Value).ToList();

            if (movingCars.Count == 0)
            {
                break;
            }

            Parallel.ForEach(movingCars, car => car.Move());

            _collisions = _cars.Values
                .GroupBy(car => car.Coordinate())
                .SelectMany(group => group.Select(car => new
                {
                    Car = car.Name(),
                    Others = group
                        .Where(otherCar => !otherCar.Name().Equals(car.Name()))
                        .Select(c => c)
                        .ToList()
                }))
                .ToDictionary(x => x.Car, x => x.Others);
        }
    }
}

public readonly struct Dimensions(int width, int height)
{
    public int Width => width;
    public int Height => height;
}

public readonly struct Coordinate(int x, int y)
{
    public int X => x;
    public int Y => y;

    public override bool Equals(object? obj)
    {
        if (obj is not Coordinate coordinate)
        {
            return false;
        }

        return x == coordinate.X && y == coordinate.Y;
    }
}
