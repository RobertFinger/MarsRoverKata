using MarsRoverSimulator.Rover;
using MarsRoverSimulator.UserInterface;

namespace MarsRoverSimulator
{
	public class MarsRoverSimulator
    {
	    public void Start()
	    {
			// an enterprise project would use an IOC container here but that's a bit much for this project.
			var io = new UserInputOutputManager();
			var ui = new UI(io);

			ui.WelcomeUser();
			
			// I could pass the map to the UI and set the map size there, but that blurs the lines between UI and business logic.
			// So I would rather get the input and validate in the ui class.
			var m = ui.GetMapSize();
			var map = new Map(m.Item1, m.Item2);

			const int r1Serial = 1;
			const int r2Serial = 2;

		    // I know, it's a little silly to have a 'builder' for 2 objects of the same type - but for most real projects we would want a builder so you get one :)  
		    var rb = new RoverBuilder(ui);
			var r1Loc = ui.SetRoverLocation(r1Serial);
			var r2Loc = ui.SetRoverLocation(r2Serial);

			//We don't allow rovers to occupy the same space.  Mars is big, rovers are expensive.  The builder will test to see if the starting locations are in the same spot.
			rb.AddRover(r1Loc, map, r1Serial);
			rb.AddRover(r2Loc, map, r2Serial);

	    }
    }
}
