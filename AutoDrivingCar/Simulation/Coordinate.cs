namespace AutoDrivingCar.Simulation;

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

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}
