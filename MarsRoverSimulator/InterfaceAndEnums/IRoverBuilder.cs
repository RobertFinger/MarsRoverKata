using MarsRoverSimulator.Rover;

namespace MarsRoverSimulator.InterfaceAndEnums
{
    public interface IRoverBuilder
    {
	    public IRover AddRover(Position location, IMap map, int roverNum);
    }
}
