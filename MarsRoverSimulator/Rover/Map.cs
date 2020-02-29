using MarsRoverSimulator.InterfaceAndEnums;
using System.Collections.Generic;
using System.Linq;
using MarsRoverSimulator.UserInterface;

namespace MarsRoverSimulator.Rover
{
	public class Map : IMap
    {
	    private readonly List<IRover> _objectsOnMap = new List<IRover>();
		private UI Ui { get;}
		private readonly int _xLimit;
	    private readonly int _yLimit;
	    public Map(int x, int y)
	    {
		    var io = new UserInputOutputManager();
		    Ui = new UI(io);

			_xLimit = x;
		    _yLimit = y;
	    }

	    public void SetRoverPosition(IRover rover)
	    {
		    foreach (var vehicle in _objectsOnMap)
		    {
			    if (rover.SerialNumber != vehicle.SerialNumber) continue;

			    vehicle.CurrentPosition = rover.CurrentPosition;
			    return;
		    }

			_objectsOnMap.Add(rover);
	    }

	    public bool IsLocationSafe(Position pos, int serial)
	    {
		    if (pos == null)
			    return false;

		    if (pos.X < 0 || pos.X > _xLimit || pos.Y < 0 || pos.Y > _yLimit)
		    {
			    if (Ui.DriveOffCliff())
			    {
				    Ui.SetRoverLocation(serial);
				}

		    }

		    if (_objectsOnMap.Any(rover => rover.CurrentPosition.X == pos.X && rover.CurrentPosition.Y == pos.Y))
		    {
			    if (Ui.CrashIntoRover())
			    {
				    Ui.SetRoverLocation(serial);
			    }
		    }


		    return true;
	    }
    }
}
