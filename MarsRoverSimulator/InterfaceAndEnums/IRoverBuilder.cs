using MarsRoverSimulator.Rover;

namespace MarsRoverSimulator.InterfaceAndEnums
{
    public interface IRoverBuilder
    {
		public int MaxSerialNumber { get; set; }
		public IRover AddRover(Position location, IMap map);
    }
}
