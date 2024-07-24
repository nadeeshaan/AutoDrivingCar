using AutoDrivingCar.Simulation;
using AutoDrivingCar.State;
using Xunit;

namespace AutoDrivingCarTests.StatesTest;

public class TestRunSimulationState : IDisposable
{
    private Simulator _simulator;
    private RunSimulationState _simulationState;
    private Context _context;

    public TestRunSimulationState()
    {
        _simulator = new Simulator();
        _simulator.SetField(10, 10);
        _context = new Context(_simulator);

        _simulator.AddCar("A", new Coordinate(1, 2), "N", "FFRFFFFRRL");
        _simulator.AddCar("B", new Coordinate(7, 8), "W", "FFLFFFFFFF");
        _simulationState = new RunSimulationState(_context);
    }

    [Fact]
    public void TestRunSimulationStatePrompt()
    {
        var prompt = _simulationState.Prompt();
        Assert.Equal("""
                     Your current list of cars are:
                     - A, (1, 2) N, FFRFFFFRRL
                     - B, (7, 8) W, FFLFFFFFFF
                     """, prompt);
    }

    [Fact]
    public void TestRunSimulationStateProcess()
    {
        var processOutput = _simulationState.Process("");
        Assert.Equal("""
                     
                     After simulation, the result is:
                     - A, collides with B at (5, 4) at step 7
                     - B, collides with A at (5, 4) at step 7
                     """, processOutput);
        Assert.IsType<RestartOrExitState>(_context.State);
    }

    public void Dispose()
    {
        _simulator = null;
        _simulationState = null;
        _context = null;
    }
}
