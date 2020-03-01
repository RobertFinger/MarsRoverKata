using MarsRoverSimulator.InterfaceAndEnums;
using MarsRoverSimulator.Rover;
using System;
using System.Text.RegularExpressions;

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

	    public string SetMovementPlan(int serial)
	    {
			var response = IO.GetUserResponse($"Enter the movement plan for rover #{serial} (L: left  R:right M:move -  for example LMLMLMLMM )");

			//the only valid response contains L M R, so lets reject everything but that.
			Regex strPattern = new Regex("^[lrmLRM]");

			if (!strPattern.IsMatch(response))
			{
				IO.SendTextToUser($"Invalid parameters.  Please use this format (L: left  R:right M:move -  for example LMLMLMLMM )");
				SetMovementPlan(serial);
			}

			return response;
	    }

	    public Position SetRoverLocation(int serial)
	    {
		    var response = IO.GetUserResponse($"Enter the starting position for rover #{serial} (X Y Dir)");
		
		    // since we aren't following the most common way to input x y dir, we can predict they may use a comma.  Let's not make them retype it, we know what they meant.
		    response = response?.Replace(',', ' ');
		    var pos = response?.Split(' ');
		    var rv = new Position();
		    try
		    {

				var s1 = int.TryParse(pos[0], out var x);
				var s2 = int.TryParse(pos[1], out var y);
				var s3 = GetDirFromChar(pos[2]);

				rv.X = x;
				rv.Y = y;
				rv.Facing = s3;

		    }
		    catch (Exception e)
		    {
				// log the error here, but reprompt the user.  We would also have a counter here, to make sure it's not a never ending loop.  After 3 tries, throw and let the user know it's not working.
			    IO.SendTextToUser($"Invalid parameters.  Please use this format (X Y Dir)");
			    SetRoverLocation(serial);
			}
		    return rv;
		}

	    private Dir GetDirFromChar(string dir)
	    {
		    var d = dir.ToLower().ToCharArray();
		    
			// There are some common mistakes they may make, like typing North instead of N.
			// We can handle that, but then we have to start testing a lot of conditions.
			// In this case, I prefer to correct the user. I would discuss this with the client and let them make the call.


		    if (d.Length > 1 || d.Length < 1)
			    return Dir.Fail;

		    var direction = dir[0];

		    return direction switch
		    {

			    'n' => Dir.North,
			    's' => Dir.South,
			    'e' => Dir.East,
			    'w' => Dir.West,
			    _ => Dir.Fail
		    };
	    }


	    public bool DriveOffCliff()
	    {
		    var response = IO.GetUserResponse("There are no guard rails on Mars and this path will cause the rover to drive off a cliff.  Would you like to change this path?  (Y/N)");
			
			// In production code there are lots of variations of yes we should test here - Yno, would return true. That's ok, it's a kata :)
			return response?.Contains("y", StringComparison.InvariantCultureIgnoreCase) ?? false;


	    }

	    public bool PromptToMuteDriveOffCliff()
	    {
		    var response = IO.GetUserResponse("Would you like to mute the warning about driving off a cliff? (Y/N)");
			// In production code there are lots of variations of yes we should test here - Yno, would return true. That's ok, it's a kata :)
			return response?.Contains("y", StringComparison.InvariantCultureIgnoreCase) ?? false;
	    }


		public bool CrashIntoRover()
	    {
		    var response = IO.GetUserResponse("There are no guard rails on Mars and this path will cause the rover to drive off a cliff.  Would you like to change this path?  (Y/N)");
			
			// In production code there are lots of variations of yes we should test here - Yno, would return true. That's ok, it's a kata :)
		    return response?.Contains("y", StringComparison.InvariantCultureIgnoreCase) ?? false;
		}

		public bool PromptToMuteCrashIntoRover()
		{
			var response = IO.GetUserResponse("Would you like to mute the warning about colliding with another rover? (Y/N)");
			// In production code there are lots of variations of yes we should test here - Yno, would return true. That's ok, it's a kata :)
			return response?.Contains("y", StringComparison.InvariantCultureIgnoreCase) ?? false;
		}


	}
}
