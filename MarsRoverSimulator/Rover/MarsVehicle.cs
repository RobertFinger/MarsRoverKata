using MarsRoverSimulator.InterfaceAndEnums;
using System;
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
		public Queue<Controls> Movement { get; set; }
		private bool CanMoveToPosition(Position pos)
		{
			throw new NotImplementedException();
		}

		public bool SetMoveCommands(string commands)
		{

			var com = commands.ToLower(CultureInfo.InvariantCulture).ToCharArray();

			foreach (var c in com)
			{
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
						return false;

				}
				
			}

			return true;

		}


		public Position MoveRover()
		{
			throw new NotImplementedException();
		}
	}
}
