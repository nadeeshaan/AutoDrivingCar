using AutoDrivingCar.Cars;
using AutoDrivingCar.Simulation;
using AutoDrivingCar.Simulation.State;
using Moq;
using Xunit;

namespace AutoDrivingCarTests.StatesTest;

public class AddCarStateTest
{
    [Fact]
    public void TestAddCarState()
    {
        var addCarState = new AddCarState();
        var simulationMock = new Mock<ISimulation>(MockBehavior.Default) { CallBase = true };

        simulationMock.Setup(s =>
            s.AddCar(It.IsAny<string>(), It.IsAny<Coordinate>(), It.IsAny<string>(), It.IsAny<string>()));
        simulationMock.Setup(s => s.GetCars()).Returns(() => new[]
            { new Car("A", new Coordinate(1, 2), "N", "FFRRFF", new Dimensions(10, 10)) });
        var simulation = simulationMock.Object;

        var processOutput = addCarState.Process(simulation, "");
        Assert.Equal("Invalid input  found", processOutput);
        simulationMock.Verify(s => s.SetState(It.IsAny<AddCarState>()));

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

        var completeStatePrompt = addCarState.Prompt(simulation);
        addCarState.Process(simulation, "1");
        var expectedPrompt =
            """
            Your current list of cars are:
            - A, (1, 2) N, FFRRFF

            Please choose from the following options:
            [1] Add a car to field
            [2] Run simulation
            """;
        Assert.Equal(expectedPrompt, completeStatePrompt);
        simulationMock.Verify(s => s.SetState(It.IsAny<AddCarState>()));

        addCarState.Process(simulation, "2");
        simulationMock.Verify(s => s.SetState(It.IsAny<RunSimulationState>()));
    }
}
