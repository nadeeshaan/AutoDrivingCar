using AutoDrivingCar.Simulation;
using AutoDrivingCar.Simulation.State;
using Xunit;

namespace AutoDrivingCarTests.StatesTest;

public class AddCarOrRunSimulationTest : IDisposable
{
    private ISimulation _simulation;
    private ISimulationState? _simulationState;

    public AddCarOrRunSimulationTest()
    {
        _simulation = new Simulation();
        _simulationState = new AddCarOrRunSimulationState();
    }

    [Fact]
    public void TestStatePrompt()
    {
        var prompt = _simulationState!.Prompt(_simulation);
        Assert.Equal("""

                     Please choose from the following options:
                     [1] Add a car to field
                     [2] Run simulation
                     """, prompt);
    }

    [Fact]
    public void TestInvalidInput()
    {
        var processOutput = _simulationState!.Process(_simulation, "1345");
        Assert.IsType<AddCarOrRunSimulationState>(_simulation.GetState());
        Assert.Equal("Invalid input 1345 found", processOutput);
    }

    [Fact]
    public void TestAddCarStateOnInput()
    {
        var processOutput = _simulationState!.Process(_simulation, "1");
        Assert.IsType<AddCarState>(_simulation.GetState());
        Assert.Empty(processOutput);
    }

    [Fact]
    public void TestRunSimulationStateOnInput()
    {
        var processOutput = _simulationState!.Process(_simulation, "2");
        Assert.IsType<RunSimulationState>(_simulation.GetState());
        Assert.Empty(processOutput);
    }

    public void Dispose()
    {
        _simulation = null;
        _simulationState = null;
    }
}
