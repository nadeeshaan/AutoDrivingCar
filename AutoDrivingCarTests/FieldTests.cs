using AutoDrivingCar.Cars;
using AutoDrivingCar.Simulation;
using Xunit;

namespace AutoDrivingCarTests;

public class FieldTests : IDisposable
{
    private Field _field;
    private Dimensions _dimensions;

    public FieldTests()
    {
        _dimensions = new Dimensions(10, 10);
        _field = new Field(_dimensions);
    }

    [Fact]
    public void TestNoCollisions()
    {
        _field.AddCar(new Car("A", new Coordinate(1, 2), Direction.N, "FFRFFFLFLL", _dimensions));
        _field.AddCar(new Car("B", new Coordinate(7, 8), Direction.W, "FFLFFFFFFF", _dimensions));
        _field.Simulate();

        Assert.Empty(_field.Collisions()["A"]);
        Assert.Empty(_field.Collisions()["B"]);
    }

    [Fact]
    public void TestCollisions()
    {
        _field.AddCar(new Car("A", new Coordinate(1, 2), Direction.N, "FFRFFFFRRL", _dimensions));
        _field.AddCar(new Car("B", new Coordinate(7, 8), Direction.W, "FFLFFFFFFF", _dimensions));
        _field.Simulate();

        var collisions = _field.Collisions();

        Assert.Equal(2, collisions.Count);
        Assert.Equal(5, collisions["A"][0].Coordinate.X);
        Assert.Equal(4, collisions["A"][0].Coordinate.Y);
        Assert.Equal(5, collisions["B"][0].Coordinate.X);
        Assert.Equal(4, collisions["B"][0].Coordinate.Y);
    }

    public void Dispose()
    {
        _field = null;
    }
}
