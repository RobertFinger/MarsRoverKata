using MarsRoverSimulator.InterfaceAndEnums;
using MarsRoverSimulator.Rover;
using System;

namespace MarsRoverSimulator.UserInterface
{
	public static class UI
    {

	    public static void WelcomeUser()
	    {
			Console.WriteLine("Hello and welcome to the NASA mars rover simulator.");
	    }

	    public static Tuple<int,int> GetMapSize()
	    {
			Console.WriteLine("Enter Graph Upper Right Coordinate (X Y)");
			var response = Console.ReadLine();

			// since we aren't following the most common way to input x y, we can predict they may use a comma.  Let's not make them retype it, we know what they meant.
			response = response?.Replace(',', ' '); 
			
			var pos = response?.Split(' ');
			var s1 = int.TryParse(pos[0].Trim(), out var x);
			var s2 = int.TryParse(pos[1].Trim(), out var y);

			if (!s1 || !s2)
			{
				Console.WriteLine($"Invalid parameters.  Please use this format (X Y Dir)");
				GetMapSize();
			}

			return new Tuple<int, int>(x,y);
		}

	    public static Position SetRoverLocation(int roverNumber)
	    {
		    Console.WriteLine($"Enter the starting position for rover #{roverNumber} (X Y Dir)");
		    var response = Console.ReadLine();

		    // since we aren't following the most common way to input x y dir, we can predict they may use a comma.  Let's not make them retype it, we know what they meant.
		    
		    response = response?.Replace(',', ' ');
		    var pos = response?.Split(' ');
			var rv = new Position();

			var s1 = int.TryParse(pos[0].Trim(), out var x);
			var s2 = int.TryParse(pos[1].Trim(), out var y);
			var s3 = GetDirFromChar(pos[2].Trim());


			if (!s1 || !s2 || s3 == Dir.Fail)
			{
				Console.WriteLine($"Invalid parameters.  Please use this format (X Y Dir)");
				SetRoverLocation(roverNumber);
			}

			rv.X = x;
			rv.Y = y;
			rv.Facing = s3;

			return rv;

	    }

	    private static Dir GetDirFromChar(string dir)
	    {
		    var d = dir.ToLower().ToCharArray();
		    
			// There are some common mistakes they may make, like typing North instead of N.
			// We can handle that, but then we have to start testing a lot of conditions.
			// In this case, I prefer to correct the user. I would discuss this with the client and let them make the call.


		    if (d.Length > 0)
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


	    public static bool DriveOffCliff()
	    {
			Console.WriteLine("There are no guard rails on Mars. Are you sure you want to drive off the cliff?  (Yes/No)");
			var response = Console.ReadLine();
			return  response?.Contains("Yes", StringComparison.InvariantCultureIgnoreCase) ?? false;


	    }

	    public static bool CrashIntoRover()
	    {
		    Console.WriteLine("This path will result in a rover crash and it takes forever for AAA to come validate an insurance claim on Mars. Are you sure you want to use this path? (Yes/No)");
		    var response = Console.ReadLine();
		    return response?.Contains("Yes", StringComparison.InvariantCultureIgnoreCase) ?? false;
		}


    }
}
