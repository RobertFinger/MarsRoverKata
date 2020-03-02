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

		public IRover AddRover(Position location, IMap map, int serial)
		{
			var loc = map.IsLocationSafe(location, serial);

			if ( loc != MoveConditions.Safe)
			{
				location = loc switch
				{
					MoveConditions.CrashWithRover => _ui.SetRoverLocation(serial),
					MoveConditions.DriveOffLedge => _ui.SetRoverLocation(serial),
					_ => location
				};
			}

			var rv = new MarsVehicle(location) {SerialNumber = serial, CurrentPosition = location};
			map.SetRoverPosition(rv);

			return rv;
		}
	}
}