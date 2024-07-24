using AutoDrivingCar.Simulation;
using AutoDrivingCar.State;
using Xunit;

namespace AutoDrivingCarTests.StatesTest;

public class AddCarOrRunSimulationTest : IDisposable
{
    private Simulator _simulator;
    private AddCarOrRunSimulationState? _simulationState;
    private Context _context;

    public AddCarOrRunSimulationTest()
    {
        _simulator = new Simulator();
        _simulator.SetField(10, 10);
        _context = new Context(_simulator);
        _simulationState = new AddCarOrRunSimulationState(_context);
    }

    [Fact]
    public void TestStatePrompt()
    {
        var prompt = _simulationState!.Prompt();
        Assert.Equal("""

                     Please choose from the following options:
                     [1] Add a car to field
                     [2] Run simulation
                     """, prompt);
    }

    [Fact]
    public void TestInvalidInput()
    {
        var processOutput = _simulationState!.Process("1345");
        Assert.IsType<AddCarOrRunSimulationState>(_context.State);
        Assert.Equal("Invalid input 1345 found", processOutput);
    }

    [Fact]
    public void TestAddCarStateOnInput()
    {
        var processOutput = _simulationState!.Process("1");
        Assert.IsType<AddCarState>(_context.State);
        Assert.Empty(processOutput);
    }

    [Fact]
    public void TestRunSimulationStateOnInput()
    {
        var processOutput = _simulationState!.Process("2");
        Assert.IsType<RunSimulationState>(_context.State);
        Assert.Empty(processOutput);
    }

    public void Dispose()
    {
        _simulator = null;
        _simulationState = null;
        _context = null;
    }
}
