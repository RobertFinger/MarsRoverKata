using System.Collections.Generic;
using MarsRoverSimulator.Rover;

namespace MarsRoverSimulator.InterfaceAndEnums
{
	public interface IRover
	{
		public int SerialNumber { get; set; }
		public Queue<Controls> Movement { get; set; }
		public bool ApplyMovementCommands(string commands);
		public Position CurrentPosition { get; set; }
		public bool MoveRover(IMap map);

	}
}