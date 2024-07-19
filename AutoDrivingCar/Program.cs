using AutoDrivingCar.Simulation.State;

namespace AutoDrivingCar;

class Program
{
    static void Main(string[] args)
    {
        var simulation = new Simulation.Simulation();
        var userInput = "";
        do
        {
            Console.WriteLine(simulation.Prompt());
            if (simulation.WaitForUserInput())
            {
                userInput = simulation.WaitForUserInput() ? Console.ReadLine() ?? "" : "";
            }

            var processedOutput = simulation.Process(userInput);

            if (!string.IsNullOrWhiteSpace(processedOutput))
            {
                Console.WriteLine(processedOutput);
            }
        } while (simulation.GetState() is not TerminalState);
    }
}
