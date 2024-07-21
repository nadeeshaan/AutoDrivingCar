using AutoDrivingCar.Cars;
using AutoDrivingCar.Simulation;
using Xunit;

namespace AutoDrivingCarTests;

public class CarTest
{
    [Fact]
    public void TestTurnLeftFromFacingNorth()
    {
        var car = new Car("A", new Coordinate(1, 1), "N", "L", new Dimensions(10, 10));
        car.Move();

        Assert.Equal("W", car.Facing());
    }

    [Fact]
    public void TestTurnLeftFromFacingWest()
    {
        var car = new Car("A", new Coordinate(1, 1), "W", "L", new Dimensions(10, 10));
        car.Move();

        Assert.Equal("S", car.Facing());
    }

    [Fact]
    public void TestTurnRightFromFacingWest()
    {
        var car = new Car("A", new Coordinate(1, 1), "W", "R", new Dimensions(10, 10));
        car.Move();

        Assert.Equal("N", car.Facing());
    }

    [Fact]
    public void TestTurnRightFromFacingNorth()
    {
        var car = new Car("A", new Coordinate(1, 1), "N", "R", new Dimensions(10, 10));
        car.Move();

        Assert.Equal("E", car.Facing());
    }

    [Fact]
    public void TestMoveForwardWithOneCommand()
    {
        var car = new Car("A", new Coordinate(1, 1), "N", "F", new Dimensions(10, 10));
        car.Move();

        Assert.Equal("N", car.Facing());
        Assert.Equal(1, car.Coordinate().X);
        Assert.Equal(2, car.Coordinate().Y);
    }

    [Fact]
    public void TestInvalidCommand()
    {
        var car = new Car("A", new Coordinate(1, 1), "N", "X", new Dimensions(10, 10));

        var exception = Assert.Throws<ArgumentException>(() => car.Move());
        Assert.Equal("Invalid command X found", exception.Message);
    }

    [Fact]
    public void TestValidStepValueAfterMoving()
    {
        var car = new Car("A", new Coordinate(1, 1), "N", "FF", new Dimensions(10, 10));

        car.Move();
        car.Move();
        car.Move();

        Assert.False(car.Moving());
    }
}
