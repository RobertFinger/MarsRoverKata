using System.Collections.Generic;
using MarsRoverSimulator.Rover;

namespace MarsRoverSimulator.InterfaceAndEnums
{
	public interface IRover
	{
		public int SerialNumber { get; set; }
		public Queue<Controls> Movement { get; set; }
		public bool SetMoveCommands(string commands);
		public Position CurrentPosition { get; set; }
		public Position MoveRover();

	}
}