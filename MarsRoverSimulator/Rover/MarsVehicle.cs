using System;
using System.Collections.Generic;
using System.Globalization;
using MarsRoverSimulator.InterfaceAndEnums;

namespace MarsRoverSimulator.Rover
{
	public class MarsVehicle : IRover
	{
		public MarsVehicle(Position pos)
		{
			CurrentPosition = pos;
		}

		public Position CurrentPosition { get; set; }

		public int SerialNumber { get; set; }
		public Queue<Controls> Movement { get; set; } = new Queue<Controls>();

		public void ApplyMovementCommands(string commands)
		{
			//this is where we convert the movement string into a movement queue.
			//the queue allows us to test each position of the movement for collision or driving off the edge and if we wanted
			//to build the functionality, we could undo the last command and add others.
			//we could do all of the logic off of string manipulation, but that's pretty fragile.

			Movement = new Queue<Controls>();
			var com = commands.ToLower(CultureInfo.InvariantCulture).ToCharArray();

			foreach (var c in com)
				switch (c)
				{
					case 'l':
						Movement.Enqueue(Controls.Left);
						break;
					case 'r':
						Movement.Enqueue(Controls.Right);
						break;
					case 'm':
						Movement.Enqueue(Controls.Forward);
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(commands));
				}
		}


		public MoveConditions MoveRover(IMap map)
		{
			foreach (var move in Movement)
				switch (move)
				{
					case Controls.Right:
						if ((int) CurrentPosition.Facing < 270)
							CurrentPosition.Facing += 90;
						else
							CurrentPosition.Facing = 0;
						break;

					case Controls.Left:
						if ((int) CurrentPosition.Facing > 0)
							CurrentPosition.Facing -= 90;
						else
							CurrentPosition.Facing = (Dir) 270;
						break;

					case Controls.Forward:
						switch (CurrentPosition.Facing)
						{
							case Dir.North:
								CurrentPosition.Y++;
								break;
							case Dir.South:
								CurrentPosition.Y--;
								break;
							case Dir.East:
								CurrentPosition.X++;
								break;
							case Dir.West:
								CurrentPosition.X--;
								break;
						}

						// check to see if that last move was OK.  If not, reset the process.
						var condition = map.IsLocationSafe(CurrentPosition, SerialNumber);
						if (condition == MoveConditions.CrashWithRover || condition == MoveConditions.DriveOffLedge)
							return condition;

						break;

					default:
						throw new ArgumentOutOfRangeException();
				}

			return map.IsLocationSafe(CurrentPosition, SerialNumber);
		}
	}
}