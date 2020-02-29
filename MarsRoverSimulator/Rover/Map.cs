using MarsRoverSimulator.InterfaceAndEnums;
using MarsRoverSimulator.UserInterface;
using System.Collections.Generic;
using System.Linq;

namespace MarsRoverSimulator.Rover
{
	public class Map : IMap
    {
	    private readonly List<IRover> _objectsOnMap = new List<IRover>();

	    private readonly int _xLimit;
	    private readonly int _yLimit;
	    public Map(int x, int y)
	    {
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

	    public bool IsLocationSafe(Position pos)
	    {
		    if (pos == null)
			    return false;

		    if (pos.X < 0 || pos.X > _xLimit || pos.Y < 0 || pos.Y > _yLimit)
		    {
				UI.DriveOffCliff();
			    return false;
		    }

		    if (_objectsOnMap.Any(rover => rover.CurrentPosition.X == pos.X && rover.CurrentPosition.Y == pos.Y))
		    {
			    UI.CrashIntoRover();
			    return false;
		    }

		    return true;

	    }
    }
}
