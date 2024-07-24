using AutoDrivingCar.Simulation;
using AutoDrivingCar.State;

namespace AutoDrivingCar;

public abstract class Runner
{
    public static void Run()
    {
        var context = new Context(new Simulator());

        var userInput = "";
        do
        {
            Console.WriteLine(context.Prompt());
            if (context.WaitForUserInput())
            {
                userInput = context.WaitForUserInput() ? Console.ReadLine() ?? "" : "";
            }

            var processedOutput = context.Process(userInput);

            if (!string.IsNullOrWhiteSpace(processedOutput))
            {
                Console.WriteLine(processedOutput);
            }
        } while (context.State is not TerminalState);
    }
}
