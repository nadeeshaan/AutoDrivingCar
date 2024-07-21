using AutoDrivingCar.Cars;
using AutoDrivingCar.Simulation;
using AutoDrivingCar.Simulation.State;
using Moq;
using Xunit;

namespace AutoDrivingCarTests;

public class StatesTests
{
    [Fact]
    public void TestCaptureAndRunSimulationState()
    {
        var captureAndRunSimulationState = new AddCarOrRunSimulationState();
        var simulation = new Simulation();

        var processedOutput = captureAndRunSimulationState.Process(simulation, "1");
        Assert.Equal("", processedOutput);
        Assert.IsType<AddCarState>(simulation.GetState());

        var processedOutput2 = captureAndRunSimulationState.Process(simulation, "2");
        Assert.Equal("", processedOutput2);
        Assert.IsType<RunSimulationState>(simulation.GetState());
    }
}
