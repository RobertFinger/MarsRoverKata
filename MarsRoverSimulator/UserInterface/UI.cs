using MarsRoverSimulator.InterfaceAndEnums;
using MarsRoverSimulator.Rover;
using System;

namespace MarsRoverSimulator.UserInterface
{
	public class UI 
    {
		public IInputOutputManager IO;

		public UI(IInputOutputManager io)
		{
			IO = io;
		}
		
		public void WelcomeUser()
	    {
		    IO.SendTextToUser("Hello and welcome to the NASA mars rover simulator.");
	    }

	    public Tuple<int,int> GetMapSize()
	    {
		    var response = IO.GetUserResponse("Enter Graph Upper Right Coordinate (X Y)");
			
			// since we aren't following the most common way to input x y, we can predict they may use a comma.  Let's not make them retype it, we know what they meant.
			response = response?.Replace(',', ' '); 
			
			var pos = response?.Split(' ');
			var s1 = int.TryParse(pos[0].Trim(), out var x);
			var s2 = int.TryParse(pos[1].Trim(), out var y);

			if (!s1 || !s2)
			{
				IO.SendTextToUser($"Invalid parameters.  Please use this format (X Y Dir)");
				GetMapSize();
			}

			return new Tuple<int, int>(x,y);
		}

	    public Position SetRoverLocation(int roverNumber)
	    {
		    var response = IO.GetUserResponse($"Enter the starting position for rover #{roverNumber} (X Y Dir)");
		
		    // since we aren't following the most common way to input x y dir, we can predict they may use a comma.  Let's not make them retype it, we know what they meant.
		    
		    response = response?.Replace(',', ' ');
		    var pos = response?.Split(' ');
			var rv = new Position();

			var s1 = int.TryParse(pos[0].Trim(), out var x);
			var s2 = int.TryParse(pos[1].Trim(), out var y);
			var s3 = GetDirFromChar(pos[2].Trim());


			if (!s1 || !s2 || s3 == Dir.Fail)
			{
				IO.SendTextToUser($"Invalid parameters.  Please use this format (X Y Dir)");
				SetRoverLocation(roverNumber);
			}

			rv.X = x;
			rv.Y = y;
			rv.Facing = s3;

			return rv;

	    }

	    private Dir GetDirFromChar(string dir)
	    {
		    var d = dir.ToLower().ToCharArray();
		    
			// There are some common mistakes they may make, like typing North instead of N.
			// We can handle that, but then we have to start testing a lot of conditions.
			// In this case, I prefer to correct the user. I would discuss this with the client and let them make the call.


		    if (d.Length > 1)
			    return Dir.Fail;

		    switch (dir[0])
		    {
				case 'n':
					return Dir.North;				
				case 's':
					return Dir.North;				
				case 'e':
					return Dir.North;				
				case 'w':
					return Dir.North;
				default:
					return Dir.Fail;

		    }
		    
	    }


	    public bool DriveOffCliff()
	    {
		    var response = IO.GetUserResponse("There are no guard rails on Mars and this path will cause the rover to drive off a cliff.  Would you like to change this path?  (Y/N)");
			
			// In production code there are lots of variations of yes we should test here - Yno, would return true. That's ok, it's a kata :)
			return response?.Contains("y", StringComparison.InvariantCultureIgnoreCase) ?? false;


	    }

	    public bool CrashIntoRover()
	    {
		    var response = IO.GetUserResponse("This path will result in a rover crash and it takes forever for AAA to come validate an insurance claim on Mars. Would you like to change this path? (Y/N)");
			// In production code there are lots of variations of yes we should test here - Yno, would return true. That's ok, it's a kata :)
		    return response?.Contains("y", StringComparison.InvariantCultureIgnoreCase) ?? false;
		}


    }
}
