using MarsRoverSimulator.InterfaceAndEnums;
using MarsRoverSimulator.UserInterface;

namespace MarsRoverSimulator.Rover
{
	public class RoverBuilder : IRoverBuilder
    {
		public int MaxSerialNumber { get; set; }
	    public IRover AddRover(Position location, IMap map)
	    {
		    
		    if (!map.IsLocationSafe(location))
		    {
			    if (!UI.CrashIntoRover())
				    location = UI.SetRoverLocation(MaxSerialNumber);
		    }


			return new MarsVehicle(location) {SerialNumber = MaxSerialNumber++, CurrentPosition = location};
	    }
    }
}
