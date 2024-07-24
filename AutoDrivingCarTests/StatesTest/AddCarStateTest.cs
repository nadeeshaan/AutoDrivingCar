using AutoDrivingCar.Simulation;
using AutoDrivingCar.State;
using Xunit;

namespace AutoDrivingCarTests.StatesTest;

public class AddCarStateTest : IDisposable
{
    private Simulator _simulator;
    private AddCarState _addCarState;
    private Context _context;

    public AddCarStateTest()
    {
        _simulator = new Simulator();
        _simulator.SetField(10, 10);
        _context = new Context(_simulator);
        _addCarState = new AddCarState(_context);
    }

    [Fact]
    public void TestProcessingEmptyUserInput()
    {
        var processedOutput = _addCarState.Process("");
        Assert.Equal("Invalid input  found", processedOutput);
        Assert.IsType<AddCarState>(_context.State);
    }

    [Fact]
    public void TestAddCarState()
    {
        var processOutput = _addCarState.Process("");
        Assert.Equal("Invalid input  found", processOutput);
        Assert.IsType<AddCarState>(_context.State);

        var promptName = _addCarState.Prompt();
        _addCarState.Process("A");
        Assert.Equal("Please enter the name of the car:", promptName);
        Assert.IsType<AddCarState>(_context.State);

        var promptCoordinates = _addCarState.Prompt();
        _addCarState.Process("1 2 N");
        Assert.Equal("Please enter initial position of car A in x y Direction format:", promptCoordinates);
        Assert.IsType<AddCarState>(_context.State);

        var promptCommands = _addCarState.Prompt();
        _addCarState.Process("FFRRFF");
        Assert.Equal("Please enter the commands for car A:", promptCommands);
        Assert.IsType<AddCarState>(_context.State);

        var completeStatePrompt = _addCarState.Prompt();
        _addCarState.Process("1");
        var expectedPrompt =
            """
            Your current list of cars are:
            - A, (1, 2) N, FFRRFF

            Please choose from the following options:
            [1] Add a car to field
            [2] Run simulation
            """;
        Assert.Equal(expectedPrompt, completeStatePrompt);
        Assert.IsType<AddCarState>(_context.State);

        _addCarState.Process("2");
        Assert.IsType<RunSimulationState>(_context.State);
    }

    public void Dispose()
    {
        _simulator = null;
        _addCarState = null;
        _context = null;
    }
}
