using System;
using MarsRoverSimulator.InterfaceAndEnums;
using System.Collections.Generic;
using System.Globalization;

namespace MarsRoverSimulator.Rover
{
	public class MarsVehicle : IRover
	{
		public Position CurrentPosition { get; set; }

		public MarsVehicle(Position pos)
		{
			CurrentPosition = pos;
		}

		public int SerialNumber { get; set; }
		public Queue<Controls> Movement { get; set; } = new Queue<Controls>();

		public bool ApplyMovementCommands(string commands)
		{

			var com = commands.ToLower(CultureInfo.InvariantCulture).ToCharArray();

			foreach (var c in com)
			{
				switch (c)
				{
					case 'l':
						this.Movement.Enqueue(Controls.Left);
						break;
					case 'r':
						this.Movement.Enqueue(Controls.Right);
						break;
					case 'm':
						this.Movement.Enqueue(Controls.Forward);
						break;
					default:
						return false;

				}
				
			}

			return true;

		}


		public bool MoveRover(IMap map)
		{

			foreach (var move in this.Movement)
			{
				switch (move)
				{
					case Controls.Right:
						if((int)this.CurrentPosition.Facing < 270)
							this.CurrentPosition.Facing+=90;
						else
							this.CurrentPosition.Facing = 0;

						Console.WriteLine($" facing: {this.CurrentPosition.Facing}");
						break;
					case Controls.Left:
						if ((int)this.CurrentPosition.Facing > 0)
							this.CurrentPosition.Facing -= 90;
						else
							this.CurrentPosition.Facing = (Dir) 270;
						
						Console.WriteLine($" facing: {this.CurrentPosition.Facing}");
						break;
					case Controls.Forward:
						switch (this.CurrentPosition.Facing)
						{
							case Dir.North:
								this.CurrentPosition.Y++;
								Console.WriteLine($" y: {this.CurrentPosition.Y}");
								break;
							case Dir.South:
								this.CurrentPosition.Y--;
								Console.WriteLine($" y: {this.CurrentPosition.Y}");
								break;
							case Dir.East:
								this.CurrentPosition.X++;
								Console.WriteLine($" x: {this.CurrentPosition.Y}");
								break;
							case Dir.West:
								this.CurrentPosition.X--;
								Console.WriteLine($" x: {this.CurrentPosition.Y}");
								break;
						}

						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}

			return true;
		}
	}
}
