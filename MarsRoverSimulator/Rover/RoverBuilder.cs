using MarsRoverSimulator.InterfaceAndEnums;
using MarsRoverSimulator.UserInterface;

namespace MarsRoverSimulator.Rover
{
	public class RoverBuilder : IRoverBuilder
	{
		private readonly UI _ui;
	    public RoverBuilder(UI ui)
	    {
		    _ui = ui;
	    }
		public void AddRover(Position location, IMap map, int serial)
	    {
		    
		    if (!map.IsLocationSafe(location, serial))
		    {
			    var resetDangerousLocation = _ui.CrashIntoRover();
			    
			    if (resetDangerousLocation)
				    location = _ui.SetRoverLocation(serial);

		    }
			var rv = new MarsVehicle(location) {SerialNumber = serial, CurrentPosition = location};
			map.SetRoverPosition(rv);
	    }
    }
}
