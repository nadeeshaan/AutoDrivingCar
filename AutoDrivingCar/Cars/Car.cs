using AutoDrivingCar.Simulation;

namespace AutoDrivingCar.Cars;

public class Car
{
    private int _directionIndex;

    public Car(string name, Coordinate coordinate, Direction facing, string commands, Dimensions dimensions)
    {
        Name = name;
        Commands = commands;
        Coordinate = coordinate;
        Dimensions = dimensions;
        _directionIndex = (int)facing;
    }

    public bool Moving { get; set; } = true;
    public string Commands { get; }
    public string Name { get; private set; }
    public Coordinate Coordinate { get; private set; }

    public Dimensions Dimensions { get; }

    public Direction Facing => (Direction)Enum.GetValues(typeof(Direction)).GetValue(_directionIndex);

    public int CurrentStep { get; private set; }

    public void Move()
    {
        if (CurrentStep >= Commands.Length)
        {
            Moving = false;
            return;
        }

        if (Enum.TryParse(Commands[CurrentStep].ToString(), true, out Command command))
        {
            switch (command)
            {
                case Command.F:
                    Forward();
                    break;
                case Command.L:
                    Left();
                    break;
                case Command.R:
                    Right();
                    break;
            }

            CurrentStep++;
            return;
        }

        throw new ArgumentException($"Invalid command {Commands[CurrentStep]} found");
    }

    private void Forward()
    {
        var displaced = Facing switch
        {
            Direction.N => new Coordinate(Coordinate.X, Coordinate.Y + 1),
            Direction.E => new Coordinate(Coordinate.X + 1, Coordinate.Y),
            Direction.S => new Coordinate(Coordinate.X, Coordinate.Y - 1),
            _ => new Coordinate(Coordinate.X - 1, Coordinate.Y),
        };

        if (WithinField(displaced))
        {
            Coordinate = displaced;
        }
    }

    private bool WithinField(Coordinate coord) =>
        coord.X >= 0 && coord.X < Dimensions.Width && coord.Y >= 0 && coord.Y < Dimensions.Height;

    private void Left() => _directionIndex -= _directionIndex == 0 ? 1 - Enum.GetValues(typeof(Direction)).Length : 1;

    private void Right()
    {
        var directionsEndIndex = Enum.GetValues(typeof(Direction)).Length - 1;
        _directionIndex += _directionIndex == directionsEndIndex ? -directionsEndIndex : 1;
    }
}
