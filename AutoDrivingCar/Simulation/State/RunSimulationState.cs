using System.Text;

namespace AutoDrivingCar.Simulation.State;

public class RunSimulationState : ISimulationState
{
    public bool WaitForUserInput() => false;

    public string Prompt(ISimulation simulation) =>
        $"""
         Your current list of cars are:
         {simulation.GetCars().ListCars()}
         """;

    public string Process(ISimulation simulation, string userInput)
    {
        simulation.Simulate();
        simulation.SetState(new RestartOrExitState());

        var uncollidedCars = simulation.GetCars()
            .Where(car => simulation.GetCollisions()[car.Name()].Count == 0)
            .ToList().PositionInfo();
        var collisionDetails = simulation.GetCollisions()
            .Where(pair => pair.Value.Count > 0).ToDictionary()
            .CollisionInfo();

        var outputBuilder = new StringBuilder("""

                                              After simulation, the result is:
                                              """);
        if (!string.IsNullOrEmpty(uncollidedCars))
        {
            outputBuilder.Append($"""

                                  {uncollidedCars}
                                  """);
        }

        if (!string.IsNullOrEmpty(collisionDetails))
        {
            outputBuilder.Append($"""

                                  {collisionDetails}
                                  """);
        }

        return outputBuilder.ToString();
    }
}
