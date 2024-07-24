using System.Text;

namespace AutoDrivingCar.State;

public class RunSimulationState(Context context) : SimulationState(context)
{
    public override bool WaitForUserInput() => false;

    public override string Prompt()
    {
        return $"""
                Your current list of cars are:
                {Context.Simulator.GetCars().ListCars()}
                """;
    }

    public override string Process(string userInput)
    {
        Context.Simulator.Simulate();
        Context.State = new RestartOrExitState(Context);

        var uncollidedCars = Context.Simulator.GetCars()
            .Where(car => Context.Simulator.GetCollisions()[car.Name].Count == 0)
            .ToList().PositionInfo();
        var collisionDetails = Context.Simulator.GetCollisions()
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
