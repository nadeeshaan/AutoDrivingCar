using AutoDrivingCar.Simulation;
using AutoDrivingCar.Simulation.State;
using Xunit;

namespace AutoDrivingCarTests.StatesTest;

public class InitialStateTest
{
    [Fact]
    public void TestInitialStatePrompt()
    {
        var initialState = new InitialState();
        var simulation = new Simulation();
        var prompt = initialState.Prompt(simulation);

        Assert.Equal("""
                     Welcome to Auto Driving Car Simulation!

                     Please enter the width and height of the simulation field in x y format:
                     """, prompt);
    }

    [Fact]
    public void TestInitialStateProcessWithValidNumericInput()
    {
        var initialState = new InitialState();
        var simulation = new Simulation();
        var processedOutput = initialState.Process(simulation, "10 8");

        Assert.Equal("You have created a field of 10 x 8.", processedOutput);
        Assert.IsType<AddCarOrRunSimulationState>(simulation.GetState());
    }

    [Fact]
    public void TestInitialStateProcessWithNonNumericInput()
    {
        var initialState = new InitialState();
        var simulation = new Simulation();
        var processedOutput = initialState.Process(simulation, "test1 test2");

        Assert.Equal("Invalid input test1 test2 found", processedOutput);
        Assert.IsType<InitialState>(simulation.GetState());
    }
}
