using AutoDrivingCar.Cars;
using AutoDrivingCar.Simulation;
using Xunit;

namespace AutoDrivingCarTests;

public class CarTest
{
    private static readonly Dimensions Dimensions = new(10, 10);
    private static readonly Coordinate Coordinate = new(1, 1);


    [Fact]
    public void TestTurnLeftFromFacingNorth()
    {
        var car = new Car("A", Coordinate, Direction.N, "L", Dimensions);
        car.Move();

        Assert.Equal(Direction.W, car.Facing);
    }

    [Fact]
    public void TestTurnLeftFromFacingWest()
    {
        var car = new Car("A", Coordinate, Direction.W, "L", Dimensions);
        car.Move();

        Assert.Equal(Direction.S, car.Facing);
    }

    [Fact]
    public void TestTurnRightFromFacingWest()
    {
        var car = new Car("A", Coordinate, Direction.W, "R", Dimensions);
        car.Move();

        Assert.Equal(Direction.N, car.Facing);
    }

    [Fact]
    public void TestTurnRightFromFacingNorth()
    {
        var car = new Car("A", Coordinate, Direction.N, "R", Dimensions);
        car.Move();

        Assert.Equal(Direction.E, car.Facing);
    }

    [Theory]
    [InlineData(0, 1, Direction.W, 0, 1, Direction.W)]
    [InlineData(0, 1, Direction.E, 1, 1, Direction.E)]
    [InlineData(1, 0, Direction.S, 1, 0, Direction.S)]
    [InlineData(1, 1, Direction.S, 1, 0, Direction.S)]
    [InlineData(9, 1, Direction.E, 9, 1, Direction.E)]
    [InlineData(9, 1, Direction.W, 8, 1, Direction.W)]
    [InlineData(9, 9, Direction.N, 9, 9, Direction.N)]
    [InlineData(9, 8, Direction.N, 9, 9, Direction.N)]
    public void TestMoveForward_PlacedAtBorders(int startX, int startY, Direction facing, int endX, int endY,
        Direction endDirection)
    {
        var car = new Car("A", new Coordinate(startX, startY), facing, "F", Dimensions);
        car.Move();

        Assert.Equal(endDirection, car.Facing);
        Assert.Equal(endX, car.Coordinate.X);
        Assert.Equal(endY, car.Coordinate.Y);
    }

    [Fact]
    public void TestInvalidCommand()
    {
        var car = new Car("A", Coordinate, Direction.N, "X", Dimensions);

        var exception = Assert.Throws<ArgumentException>(() => car.Move());
        Assert.Equal("Invalid command X found", exception.Message);
    }

    [Fact]
    public void TestValidStepValueAfterMoving()
    {
        var car = new Car("A", Coordinate, Direction.N, "FF", Dimensions);

        car.Move();
        car.Move();
        car.Move();

        Assert.False(car.Moving);
    }
}
