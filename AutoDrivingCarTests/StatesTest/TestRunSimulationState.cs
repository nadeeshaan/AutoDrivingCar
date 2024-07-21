using AutoDrivingCar.Cars;
using AutoDrivingCar.Simulation;
using AutoDrivingCar.Simulation.State;
using Moq;
using Xunit;

namespace AutoDrivingCarTests.StatesTest;

public class TestRunSimulationState : IDisposable
{
    private ISimulation _simulation;
    private ISimulationState _simulationState;
    private Mock<ISimulation> _simulationMock;

    public TestRunSimulationState()
    {
        var carA = new Car("A", new Coordinate(1, 2), "N", "FFRFFFFRRL", new Dimensions(10, 10));
        var carB = new Car("B", new Coordinate(7, 8), "N", "FFLFFFFFFF", new Dimensions(10, 10));
        _simulationMock = new Mock<ISimulation>(MockBehavior.Default) { CallBase = true };
        _simulationMock.Setup(s => s.Simulate());
        _simulationMock.Setup(s => s.GetCars()).Returns(() => new[] { carA, carB });
        _simulationMock
            .Setup(s => s.GetCollisions())
            .Returns(() => new Dictionary<string, List<ICar>> { { "A", [carB] }, { "B", [carA] } });

        _simulation = _simulationMock.Object;
        _simulationState = new RunSimulationState();
    }

    [Fact]
    public void TestRunSimulationStatePrompt()
    {
        var prompt = _simulationState.Prompt(_simulation);
        Assert.Equal("""
                     Your current list of cars are:
                     - A, (1, 2) N, FFRFFFFRRL
                     - B, (7, 8) N, FFLFFFFFFF
                     """, prompt);
    }

    [Fact]
    public void TestRunSimulationStateProcess()
    {
        var processOutput = _simulationState.Process(_simulation, "");
        Assert.Equal("""

                     After simulation, the result is:
                     - A, collides with B at (7, 8) at step 0
                     - B, collides with A at (1, 2) at step 0
                     """, processOutput);
        _simulationMock.Verify(s => s.Simulate(), Times.Once);
        _simulationMock.Verify(s => s.SetState(It.IsAny<RestartOrExitState>()), Times.Once);
    }

    public void Dispose()
    {
        _simulation = null;
        _simulationState = null;
        _simulationMock = null;
    }
}
