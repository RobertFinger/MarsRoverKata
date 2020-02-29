using MarsRoverSimulator.Rover;

namespace MarsRoverSimulator.InterfaceAndEnums
{
	public interface IMap
	{
		public bool IsLocationSafe(Position pos);
		public void SetRoverPosition(IRover rover);
	}
}