using AutoDrivingCar;
using Xunit;

namespace AutoDrivingCarTests;

public class RunnerTest
{
    [Fact]
    public void TestRunner_OneCar()
    {
        var output = new StringWriter();
        Console.SetOut(output);

        Console.SetIn(new StringReader($"10 10{Environment.NewLine}" +
                                       $"1{Environment.NewLine}" +
                                       $"A{Environment.NewLine}" +
                                       $"1 2 N{Environment.NewLine}" +
                                       $"FFRFFFFRRL{Environment.NewLine}" +
                                       $"2{Environment.NewLine}" +
                                       $"2{Environment.NewLine}"));

        Runner.Run();

        Assert.Contains("""
                        Your current list of cars are:
                        - A, (1, 2) N, FFRFFFFRRL

                        After simulation, the result is:
                        - A, (5, 4) S
                        """, output.ToString());
    }

    [Fact]
    public void TestRunner_TwoCars_Colliding()
    {
        var output = new StringWriter();
        Console.SetOut(output);

        Console.SetIn(new StringReader($"10 10{Environment.NewLine}" +
                                       $"1{Environment.NewLine}" +
                                       $"A{Environment.NewLine}" +
                                       $"1 2 N{Environment.NewLine}" +
                                       $"FFRFFFFRRL{Environment.NewLine}" +
                                       $"1{Environment.NewLine}" +
                                       $"B{Environment.NewLine}" +
                                       $"7 8 W{Environment.NewLine}" +
                                       $"FFLFFFFFFF{Environment.NewLine}" +
                                       $"2{Environment.NewLine}" +
                                       $"2{Environment.NewLine}"));

        Runner.Run();

        Assert.Contains("""
                        Your current list of cars are:
                        - A, (1, 2) N, FFRFFFFRRL
                        - B, (7, 8) W, FFLFFFFFFF

                        After simulation, the result is:
                        - A, collides with B at (5, 4) at step 7
                        - B, collides with A at (5, 4) at step 7
                        """, output.ToString());
    }
}
