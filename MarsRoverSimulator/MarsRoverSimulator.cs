using MarsRoverSimulator.Rover;
using MarsRoverSimulator.UserInterface;

namespace MarsRoverSimulator
{
	public class MarsRoverSimulator
    {
	    public void Start()
	    {
			// an enterprise project would use an IOC container here but that's a bit much for this project.
			var io = new UserInputOutputManager();
			var ui = new UI(io);

			ui.WelcomeUser();
			
			// I could pass the map to the UI and set the map size there, but that blurs the lines between UI and business logic.
			// So I would rather get the input and validate in the ui class.
			var m = ui.GetMapSize();
			var map = new Map(m.Item1, m.Item2);
		    
			// I know, it's a little silly to have a 'builder' for 2 objects of the same type - but for most real projects we would want a builder so you get one :)  
			var rb = new RoverBuilder(ui);
			const int r1Serial = 1;
			const int r2Serial = 2;

			
			var r1Loc = ui.SetRoverLocation(r1Serial);
			var rover1 = rb.AddRover(r1Loc, map, r1Serial);
			var commands1 = ui.SetMovementPlan(rover1.SerialNumber);
			rover1.ApplyMovementCommands(commands1);
			rover1.MoveRover(map);
			io.SendTextToUser($"Rover 1 output {rover1.CurrentPosition.X} {rover1.CurrentPosition.Y} {rover1.CurrentPosition.Facing.ToString()}");

			// the first rover may have muted these warnings, so lets unmute them. In production code we may prefer each rover to have it's on mute warnings settings, but that's a design call.
			// Here I decided to let the map handle all environmental things like rovers colliding or driving off the edge. 
			map.MuteCliffWarning(false);
			map.MuteCollisionWarning(false);

			var r2Loc = ui.SetRoverLocation(r2Serial);			
			var rover2 = rb.AddRover(r2Loc, map, r2Serial);
			var commands2 = ui.SetMovementPlan(rover2.SerialNumber);
			rover2.ApplyMovementCommands(commands2);
			rover2.MoveRover(map);
			io.SendTextToUser($"Rover 2 output {rover2.CurrentPosition.X} {rover2.CurrentPosition.Y} {rover2.CurrentPosition.Facing.ToString()}");

	    }
    }
}
