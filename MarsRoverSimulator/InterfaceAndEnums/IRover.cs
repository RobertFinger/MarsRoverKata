using System.Collections.Generic;
using MarsRoverSimulator.Rover;

namespace MarsRoverSimulator.InterfaceAndEnums
{
	public interface IRover
	{
		public int SerialNumber { get; set; }
		public Queue<Controls> Movement { get; set; }
		public Position CurrentPosition { get; set; }
		public void ApplyMovementCommands(string commands);
		public MoveConditions MoveRover(IMap map);
	}
}