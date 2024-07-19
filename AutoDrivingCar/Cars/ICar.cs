using AutoDrivingCar.Simulation;

namespace AutoDrivingCar.Cars;

public interface ICar
{
    string Name();
    Coordinate Coordinate();
    string Facing();
    void Move();
    bool Moving();
    string Commands();
    int Step();
}
