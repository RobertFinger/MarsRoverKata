using MarsRoverSimulator.InterfaceAndEnums;
using System;
using System.Collections.Generic;

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
		public Queue<Position> Movement { get; set; }
		private bool CanMoveToPosition(Position pos)
		{
			throw new NotImplementedException();
		}

		public bool SetMoveCommands(string commands)
		{
			throw new NotImplementedException();
		}




		public Position MoveRover()
		{
			throw new NotImplementedException();
		}
	}
}
