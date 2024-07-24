namespace AutoDrivingCar.Simulation;

public readonly struct Dimensions(int width, int height)
{
    public int Width => width;
    public int Height => height;
}
