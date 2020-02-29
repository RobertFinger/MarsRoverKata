using MarsRoverSimulator.Rover;
using MarsRoverSimulator.UserInterface;

namespace MarsRoverSimulator
{
	public class MarsRoverSimulator
    {
	    public void Start()
	    {
			// I am not a fan of comments in code except in cases where the code is too complicated to understand (difficult math for example) or because the reason for doing something isn't obvious.
			// This is because comments lie after the first revision. - (So said the clean coder book, and I agree)  
			// But you asked me to add comments so I will follow that direction.
			UI.WelcomeUser();
			
			// I could pass the map to the UI and set the map size there, but that blurs the lines between UI and Code. So I would rather get the input and validate in the ui class.
			var m = UI.GetMapSize();
			var map = new Map(m.Item1, m.Item2);
			
		    // I know, it's a little silly to have a 'builder' for 2 objects of the same type - but for most real projects we would want a builder so you get one :)  
			var rb = new RoverBuilder();
			var r1Loc = UI.SetRoverLocation(1);
			var r2Loc = UI.SetRoverLocation(1);

			//We don't allow rovers to occupy the same space.  Mars is big, rovers are expensive.  The builder will test to see if the starting locations are in the same spot.
			var rover1 = rb.AddRover(r1Loc, map);
			var rover2 = rb.AddRover(r2Loc, map);
			
			
			map.SetRoverPosition(rover1);
			map.SetRoverPosition(rover2);
	    }
    }
}
