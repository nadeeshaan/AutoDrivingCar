using AutoDrivingCar.Simulation;

namespace AutoDrivingCar.State;

public class AddCarState(Context context) : SimulationState(context)
{
    private CarOptionsSetting _carOptionsSetting = CarOptionsSetting.Name;
    private string _name = null!;
    private Coordinate _coordinate;
    private string _facing = null!;
    private string _commands = null!;

    public override string Prompt()
    {
        return _carOptionsSetting switch
        {
            CarOptionsSetting.Name => "Please enter the name of the car:",
            CarOptionsSetting.Coordinates =>
                $"Please enter initial position of car {_name} in x y Direction format:",
            CarOptionsSetting.Commands =>
                $"Please enter the commands for car {_name}:",
            _ =>
                $"""
                 Your current list of cars are:
                 {Context.Simulator.GetCars().ListCars()}

                 Please choose from the following options:
                 [1] Add a car to field
                 [2] Run simulation
                 """
        };
    }

    public override string Process(string userInput)
    {
        if (string.IsNullOrWhiteSpace(userInput))
        {
            Context.State = new AddCarState(Context);
            return ISimulationState.InvalidInput(userInput);
        }

        switch (_carOptionsSetting)
        {
            case CarOptionsSetting.Name:
                return SetName(userInput);
            case CarOptionsSetting.Coordinates:
                return SetCoordinates(Context, userInput);
            case CarOptionsSetting.Commands:
                return SetCommand(userInput);
            case CarOptionsSetting.Complete:
            default:
                return HandleCompletion(userInput);
        }
    }

    private string SetCommand(string userInput)
    {
        _commands = userInput;
        Context.Simulator.AddCar(_name, _coordinate, _facing, _commands);
        Context.State = this;
        _carOptionsSetting = CarOptionsSetting.Complete;

        return string.Empty;
    }

    private string SetCoordinates(Context context, string userInput)
    {
        var dimensions = userInput.Split((char[])null, StringSplitOptions.RemoveEmptyEntries)
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Take(3)
            .ToArray();

        if (dimensions.Length != 3 ||
            !int.TryParse(dimensions[0], out var xCord) ||
            !int.TryParse(dimensions[1], out var yCord))
        {
            return ISimulationState.InvalidInput(userInput);
        }

        _coordinate = new Coordinate(xCord, yCord);
        _facing = dimensions[2];
        _carOptionsSetting = CarOptionsSetting.Commands;
        context.State = this;

        return string.Empty;
    }

    private string SetName(string userInput)
    {
        if (string.IsNullOrWhiteSpace(userInput))
        {
            return ISimulationState.InvalidInput("empty value");
        }

        if (Context.Simulator.ContainsCar(userInput))
        {
            return $"You have already added a car with the name {userInput}";
        }

        _name = userInput;
        _carOptionsSetting = CarOptionsSetting.Coordinates;
        Context.State = this;

        return string.Empty;
    }

    private string HandleCompletion(string userInput)
    {
        if (int.TryParse(userInput, out var option) && option is 1 or 2)
        {
            Context.State = option is 1 ? new AddCarState(Context) : new RunSimulationState(Context);
            return string.Empty;
        }

        Context.State = new AddCarOrRunSimulationState(Context);
        return ISimulationState.InvalidInput(userInput);
    }
}
