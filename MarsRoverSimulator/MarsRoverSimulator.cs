using MarsRoverSimulator.InterfaceAndEnums;
using MarsRoverSimulator.Rover;
using MarsRoverSimulator.UserInterface;

namespace MarsRoverSimulator
{
	public class MarsRoverSimulator
	{
		private UI _ui;
		private UserInputOutputManager _io;

		public MarsRoverSimulator()
		{
			_io = new UserInputOutputManager();
			_ui = new UI(_io);			
		}

		public void Start()
		{
			// an enterprise project would use an IOC container here but that's a bit much for this project.
			
			_ui.WelcomeUser();
			var m = _ui.GetMapSize();
			var map = new Map(m.Item1, m.Item2);

			var success = MoveConditions.NoResults;

			// if the rover hits something or drives off a ledge, redo it.
			do
			{
				success = CommandRover(1, map);
			} while (success != MoveConditions.Safe);


			// the first rover may have muted these warnings, so lets unmute them. In production code we may prefer each rover to have it's on mute warnings settings, but that's a design call.
			// I decided to let the map handle all environmental things like rovers colliding or driving off the edge. 
			map.MuteCliffWarning(false);
			map.MuteCollisionWarning(false);

			do
			{
				success = CommandRover(2, map);
			} while (success != MoveConditions.Safe);


		}


		private MoveConditions CommandRover(int serial, Map map)
		{
			// I know, it's a little silly to have a 'builder' for 2 objects of the same type - but for most real projects we would want a builder so you get one :)  
			var rb = new RoverBuilder(_ui);
			
			var Loc = _ui.SetRoverLocation(serial);
			
			var rover = rb.AddRover(Loc, map, serial);
			var commands = _ui.SetMovementPlan(rover.SerialNumber);
			rover.ApplyMovementCommands(commands);
			var move = rover.MoveRover(map);

			if (move != MoveConditions.Safe) return move;

			_io.SendTextToUser($"Rover 1 output {rover.CurrentPosition.X} {rover.CurrentPosition.Y} {rover.CurrentPosition.Facing.ToString()}");
			return MoveConditions.Safe;

		}
	}
}