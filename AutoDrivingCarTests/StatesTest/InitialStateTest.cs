using AutoDrivingCar.Simulation;
using AutoDrivingCar.State;
using Xunit;

namespace AutoDrivingCarTests.StatesTest;

public class InitialStateTest : IDisposable
{
    private Simulator _simulator;
    private InitialState _simulationState;
    private Context _context;

    public InitialStateTest()
    {
        _simulator = new Simulator();
        _simulator.SetField(10, 10);
        _context = new Context(_simulator);
        _simulationState = new InitialState(_context);
    }

    [Fact]
    public void TestInitialStatePrompt()
    {
        var prompt = _simulationState.Prompt();

        Assert.Equal("""
                     Welcome to Auto Driving Car Simulation!

                     Please enter the width and height of the simulation field in x y format:
                     """, prompt);
    }

    [Fact]
    public void TestInitialStateProcessWithValidNumericInput()
    {
        var processedOutput = _simulationState.Process("10 8");

        Assert.Equal("You have created a field of 10 x 8.", processedOutput);
        Assert.IsType<AddCarOrRunSimulationState>(_context.State);
    }

    [Fact]
    public void TestInitialStateProcessWithNonNumericInput()
    {
        var processedOutput = _simulationState.Process("test1 test2");

        Assert.Equal("Invalid input test1 test2 found", processedOutput);
        Assert.IsType<InitialState>(_context.State);
    }
    
    public void Dispose()
    {
        _simulator = null;
        _simulationState = null;
        _context = null;
    }
}
