using AutoDrivingCar.Cars;

namespace AutoDrivingCar.Simulation;

public class Simulator
{
    private Field _field = null!;

    public void SetField(int x, int y) => _field = new Field(new Dimensions(x, y));

    public IEnumerable<Car> GetCars() => _field.Cars().Values;

    public bool ContainsCar(string name) => _field.Cars().ContainsKey(name);

    public Dictionary<string, List<Car>> GetCollisions() => _field.Collisions();

    public void AddCar(string name, Coordinate coordinate, string facing, string commands)
    {
        var direction = (Direction)Enum.Parse(typeof(Direction), facing);
        _field.Cars().Add(name, new Car(name, coordinate, direction, commands, _field.GetDimensions()));
    }

    public void Simulate() => _field.Simulate();
}
