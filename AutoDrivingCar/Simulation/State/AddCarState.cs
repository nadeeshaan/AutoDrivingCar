namespace AutoDrivingCar.Simulation.State;

public class AddCarState : ISimulationState
{
    private CarOptionsSetting _carOptionsSetting = CarOptionsSetting.Name;
    private string _name;
    private Coordinate _coordinate;
    private string _facing;
    private string _commands;

    public string Prompt(ISimulation simulation)
    {
        return _carOptionsSetting switch
        {
            CarOptionsSetting.Name => "Please enter the name of the car:",
            CarOptionsSetting.Coordinates =>
                $"Please enter initial position of car {_name} in x y Direction format:",
            CarOptionsSetting.Commands =>
                $"Please enter the commands for car {_name}:",
            CarOptionsSetting.Complete =>
                $"""
                 Your current list of cars are:
                 {simulation.GetCars().ListCars()}

                 Please choose from the following options:
                 [1] Add a car to field
                 [2] Run simulation
                 """,
            _ => "Invalid state found"
        };
    }

    public string Process(ISimulation simulation, string userInput)
    {
        if (string.IsNullOrWhiteSpace(userInput))
        {
            simulation.SetState(new AddCarState());
            return $"Invalid input {userInput} found";
        }

        switch (_carOptionsSetting)
        {
            case CarOptionsSetting.Name:
                SetName(simulation, userInput);
                break;
            case CarOptionsSetting.Coordinates:
                SetCoordinates(simulation, userInput);
                break;
            case CarOptionsSetting.Commands:
                SetCommand(simulation, userInput);
                break;
            case CarOptionsSetting.Complete:
                return HandleCompletion(simulation, userInput);
            default:
                simulation.SetState(new CaptureAndRunSimulationState());
                break;
        }

        return "";
    }

    private void SetCommand(ISimulation simulation, string userInput)
    {
        _commands = userInput;
        simulation.AddCar(_name, _coordinate, _facing, _commands);
        simulation.SetState(this);
        _carOptionsSetting = CarOptionsSetting.Complete;
    }

    private void SetCoordinates(ISimulation simulation, string userInput)
    {
        var dimensions = userInput.Split((char[])null, StringSplitOptions.RemoveEmptyEntries)
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Take(3)
            .ToArray();

        if (dimensions.Length == 3 &&
            int.TryParse(dimensions[0], out var xCord) &&
            int.TryParse(dimensions[1], out var yCord) &&
            !string.IsNullOrWhiteSpace(dimensions[2]))
        {
            _coordinate = new Coordinate(xCord, yCord);
            _facing = dimensions[2];
            _carOptionsSetting = CarOptionsSetting.Commands;
            simulation.SetState(this);
            return;
        }

        Console.WriteLine($"Invalid input {userInput} found");
    }

    private void SetName(ISimulation simulation, string userInput)
    {
        _name = userInput;
        _carOptionsSetting = CarOptionsSetting.Coordinates;
        simulation.SetState(this);
    }

    private string HandleCompletion(ISimulation simulation, string userInput)
    {
        if (int.TryParse(userInput, out var option) && option is 1 or 2)
        {
            simulation.SetState(option is 1 ? new AddCarState() : new RunSimulationState());
            return "";
        }

        simulation.SetState(new CaptureAndRunSimulationState());
        return $"Invalid input {userInput} found";
    }
}
