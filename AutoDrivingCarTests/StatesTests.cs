using AutoDrivingCar.Cars;
using AutoDrivingCar.Simulation;
using AutoDrivingCar.Simulation.State;
using Moq;
using Xunit;

namespace AutoDrivingCarTests;

public class StatesTests
{
    [Fact]
    public void TestInitialState()
    {
        var initialState = new InitialState();
        var simulation = new Simulation();
        var processedOutput = initialState.Process(simulation, "10 10");

        Assert.Equal("You have created a field of 10 x 10.", processedOutput);
        Assert.IsType<CaptureAndRunSimulationState>(simulation.GetState());
    }

    [Fact]
    public void TestCaptureAndRunSimulationState()
    {
        var captureAndRunSimulationState = new CaptureAndRunSimulationState();
        var simulation = new Simulation();

        var processedOutput = captureAndRunSimulationState.Process(simulation, "1");
        Assert.Equal("", processedOutput);
        Assert.IsType<AddCarState>(simulation.GetState());

        var processedOutput2 = captureAndRunSimulationState.Process(simulation, "2");
        Assert.Equal("", processedOutput2);
        Assert.IsType<RunSimulationState>(simulation.GetState());
    }

    [Fact]
    public void TestAddCarState()
    {
        var addCarState = new AddCarState();
        var simulationMock = new Mock<ISimulation>(MockBehavior.Default) { CallBase = true };

        simulationMock.Setup(s =>
            s.AddCar(It.IsAny<string>(), It.IsAny<Coordinate>(), It.IsAny<string>(), It.IsAny<string>()));
        var simulation = simulationMock.Object;

        var promptName = addCarState.Prompt(simulation);
        addCarState.Process(simulation, "A");
        Assert.Equal("Please enter the name of the car:", promptName);
        simulationMock.Verify(s => s.SetState(It.IsAny<AddCarState>()));

        var promptCoordinates = addCarState.Prompt(simulation);
        addCarState.Process(simulation, "1 2 N");
        Assert.Equal("Please enter initial position of car A in x y Direction format:", promptCoordinates);
        simulationMock.Verify(s => s.SetState(It.IsAny<AddCarState>()));

        var promptCommands = addCarState.Prompt(simulation);
        addCarState.Process(simulation, "FFRRFF");
        Assert.Equal("Please enter the commands for car A:", promptCommands);
        simulationMock.Verify(s => s.SetState(It.IsAny<AddCarState>()));

        addCarState.Process(simulation, "1");
        simulationMock.Verify(s => s.SetState(It.IsAny<AddCarState>()));

        addCarState.Process(simulation, "2");
        simulationMock.Verify(s => s.SetState(It.IsAny<RunSimulationState>()));
    }

    [Fact]
    public void TestRunSimulationState()
    {
        var runSimulationState = new RunSimulationState();
        var simulationMock = new Mock<ISimulation>(MockBehavior.Default) { CallBase = true };

        simulationMock.Setup(s => s.Simulate());
        simulationMock.Setup(s => s.GetCollisions()).Returns(() => new Dictionary<string, List<ICar>>());
        simulationMock.Setup(s => s.GetCars()).Returns(() => []);
        var simulation = simulationMock.Object;

        runSimulationState.Process(simulation, "");
        simulationMock.Verify(s => s.Simulate(), Times.Once);
        simulationMock.Verify(s => s.SetState(It.IsAny<RestartOrExitState>()), Times.Once);
    }
}
