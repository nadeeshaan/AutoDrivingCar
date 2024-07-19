using AutoDrivingCar.Simulation;
using Xunit;

namespace AutoDrivingCarTests;

public class FieldTests
{
    [Fact]
    public void TestNoCollisions()
    {
        var field = new Field(new Dimensions(10, 10));
        field.AddCar("A", 1, 2, "N", "FFRFFFFRRL");
        field.AddCar("B", 7, 8, "W", "FFLFFFFFFF");
        field.Simulate();

        var collisions = field.Collisions();

        Assert.Equal(2, collisions.Count);
        Assert.Equal(5, collisions["A"][0].Coordinate().X);
        Assert.Equal(4, collisions["A"][0].Coordinate().Y);
        Assert.Equal(5, collisions["B"][0].Coordinate().X);
        Assert.Equal(4, collisions["B"][0].Coordinate().Y);
    }
    
    [Fact]
    public void TestCollisions()
    {
        var field = new Field(new Dimensions(10, 10));
        field.AddCar("A", 1, 2, "N", "FFRFFFFRRL");
        field.AddCar("B", 7, 8, "W", "FFLFFFFFFF");
        field.Simulate();

        var collisions = field.Collisions();

        Assert.Equal(2, collisions.Count);
        Assert.Equal(5, collisions["A"][0].Coordinate().X);
        Assert.Equal(4, collisions["A"][0].Coordinate().Y);
        Assert.Equal(5, collisions["B"][0].Coordinate().X);
        Assert.Equal(4, collisions["B"][0].Coordinate().Y);
    }
}
