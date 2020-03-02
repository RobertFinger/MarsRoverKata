using System.Collections.Generic;
using System.Linq;
using MarsRoverSimulator.InterfaceAndEnums;
using MarsRoverSimulator.UserInterface;

namespace MarsRoverSimulator.Rover
{
	public class Map : IMap
	{
		private readonly List<IRover> _objectsOnMap = new List<IRover>();
		private readonly int _xLimit;
		private readonly int _yLimit;

		public Map(int x, int y)
		{
			var io = new UserInputOutputManager();
			Ui = new UI(io);

			_xLimit = x;
			_yLimit = y;
		}

		private UI Ui { get; }

		public void SetRoverPosition(IRover rover)
		{		
			// rather than reset the same rover's position we can just replace it.
			// to do that we remove it if we find it here, then add it again.  In a more robust system we would
			// prefer to update instead of replace since removing things from a list is expensive.
			// for this exercise though, we're fine.


			foreach (var vehicle in _objectsOnMap.Where(vehicle => rover.SerialNumber == vehicle.SerialNumber))
			{
				_objectsOnMap.Remove(vehicle);
				break;
			}
				

			_objectsOnMap.Add(rover);
		}

		public MoveConditions IsLocationSafe(Position pos, int serial)
		{
			//Since this is a simulator, they will want us to warn them if their planned route is not safe.
			//the two conditions we can test for is "did we drive off the cliff" and "did we crash into another rover"
			// note: the reason we use a queue for movement in MarsVehicle.cs is so we can check for collision or cliff edges every step of the way.
			// that way you can't park a rover in the path of another rover without getting a feedback.
			// also, if we want to, we can easily make both rovers run at the same time and test for collisions.  
			// That's not part of the spec, but a beneficial side effect of how it's built.

			if (pos == null)
				return MoveConditions.DriveOffLedge;

			if (pos.X < 0 || pos.X > _xLimit || pos.Y < 0 || pos.Y > _yLimit)
			{
				
				if (Ui.DriveOffCliff())
				{
					return MoveConditions.DriveOffLedge;
				}
			}

			if (_objectsOnMap.Any(rover => rover.CurrentPosition.X == pos.X && rover.CurrentPosition.Y == pos.Y && rover.SerialNumber != serial))
			{
				if (Ui.CrashIntoRover())
				{
					return MoveConditions.CrashWithRover;
				}
			}


			return MoveConditions.Safe;
		}

	}
}