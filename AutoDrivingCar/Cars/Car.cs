using AutoDrivingCar.Simulation;

namespace AutoDrivingCar.Cars;

public class Car(
    string name,
    Coordinate coordinate,
    string facing,
    string commands,
    Dimensions dimensions) : ICar
{
    private static readonly List<string> DirectionList = ["N", "E", "S", "W"];
    private int _directionIndex = DirectionList.IndexOf(facing);
    private int _step;
    private bool _moving = true;

    public bool Moving() => _moving;
    public string Commands() => commands;
    public string Name() => name;
    public Coordinate Coordinate() => coordinate;
    public string Facing() => DirectionList[_directionIndex];
    public int Step() => _step;

    public void Move()
    {
        if (_step >= commands.Length)
        {
            _moving = false;
            return;
        }

        var command = commands[_step].ToString();
        switch (command)
        {
            case "F":
                Forward();
                break;
            case "L":
            case "R":
                Turn(command);
                break;
            default:
                throw new ArgumentException($"Invalid command ${command} found");
        }

        _step++;
    }

    private void Forward()
    {
        var facingDirection = DirectionList[_directionIndex];
        var displaced = facingDirection switch
        {
            "N" => new Coordinate(coordinate.X, coordinate.Y + 1),
            "E" => new Coordinate(coordinate.X + 1, coordinate.Y),
            "S" => new Coordinate(coordinate.X, coordinate.Y - 1),
            "W" => new Coordinate(coordinate.X - 1, coordinate.Y),
            _ => throw new ArgumentException($"Invalid direction ${facingDirection} found")
        };

        if (WithinField(displaced))
        {
            coordinate = displaced;
        }
    }

    private bool WithinField(Coordinate coord) =>
        coord.X >= 0 && coord.X < dimensions.Width && coord.Y >= 0 && coord.Y < dimensions.Height;

    private void Turn(string direction)
    {
        var directionsEndIndex = DirectionList.Count - 1;
        switch (direction)
        {
            case "L":
                _directionIndex -= _directionIndex == 0 ? -directionsEndIndex : 1;
                break;
            case "R":
                _directionIndex += _directionIndex == directionsEndIndex ? -directionsEndIndex : 1;
                break;
            default:
                throw new ArgumentException($"Invalid input ${direction} found");
        }
    }
}
