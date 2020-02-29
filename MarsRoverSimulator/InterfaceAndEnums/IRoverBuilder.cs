using MarsRoverSimulator.Rover;

namespace MarsRoverSimulator.InterfaceAndEnums
{
    public interface IRoverBuilder
    {
	    public void AddRover(Position location, IMap map, int roverNum);
    }
}
