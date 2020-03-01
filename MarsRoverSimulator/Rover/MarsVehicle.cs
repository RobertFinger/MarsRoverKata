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
			//the queue allows us to test each position of the movement for collision or driving off the edge.
			//we could do all of the logic off of string manipulation, but that's pretty fragile.

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


		public bool MoveRover(IMap map)
		{
			Console.WriteLine($"Facing:{CurrentPosition.Facing}");

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
								map.IsLocationSafe(CurrentPosition, SerialNumber);
								break;
							case Dir.South:
								CurrentPosition.Y--;
								map.IsLocationSafe(CurrentPosition, SerialNumber);
								break;
							case Dir.East:
								CurrentPosition.X++;
								map.IsLocationSafe(CurrentPosition, SerialNumber);
								break;
							case Dir.West:
								CurrentPosition.X--;
								map.IsLocationSafe(CurrentPosition, SerialNumber);
								break;
						}

						break;

					default:
						throw new ArgumentOutOfRangeException();
				}

			return true;
		}
	}
}