using MarsRoverSimulator.InterfaceAndEnums;
using MarsRoverSimulator.Rover;
using NUnit.Framework;

namespace NUnitTest
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
			// I didn't do a lot of unit testing, but normally I would.  Most of this was coded while I was on an airplane flying from big sky to detroit and I got home before my unit test section was complete.  
			// Don't take this to mean I do not value unit tests, I believe in unit testing very much. I did the minimum spec in this department to make the other parts of the spec more robust. It was a trade off.
		}

		[Test]
		public void MoveRover1()
		{
			var map = new Map(5, 5);
			var p = new Position {X = 1, Y = 2, Facing = Dir.North};
			var rover = new MarsVehicle(p);
			const string movement = "LMLMLMLMM";

			rover.ApplyMovementCommands(movement);
			rover.MoveRover(map);

			var actual = rover.CurrentPosition;
			var expected = new Position {X = 1, Y = 3, Facing = Dir.North};

			// we can serialize the objects to json and compare them that way, but these are tiny objects so it's not necessary.

			Assert.AreEqual(expected.X, actual.X);
			Assert.AreEqual(expected.Y, actual.Y);
			Assert.AreEqual(expected.Facing, actual.Facing);
		}


		[Test]
		public void MoveRover2()
		{
			var map = new Map(5, 5);
			var p2 = new Position {X = 3, Y = 3, Facing = Dir.East};
			var rover2 = new MarsVehicle(p2);
			const string movement2 = "MMRMMRMRRM";

			rover2.ApplyMovementCommands(movement2);
			rover2.MoveRover(map);

			var actual2 = rover2.CurrentPosition;
			var expected2 = new Position {X = 5, Y = 1, Facing = Dir.East};

			Assert.AreEqual(expected2.X, actual2.X);
			Assert.AreEqual(expected2.Y, actual2.Y);
			Assert.AreEqual(expected2.Facing, actual2.Facing);
		}

		[Test]
		public void RotateRight()
		{
			var map = new Map(5, 5);
			var p2 = new Position { X = 3, Y = 3, Facing = Dir.East };
			var rover2 = new MarsVehicle(p2);
			const string movement2 = "R";

			rover2.ApplyMovementCommands(movement2);
			rover2.MoveRover(map);

			var actual2 = rover2.CurrentPosition;
			var expected2 = new Position { X = 3, Y = 3, Facing = Dir.South };

			Assert.AreEqual(expected2.X, actual2.X);
			Assert.AreEqual(expected2.Y, actual2.Y);
			Assert.AreEqual(expected2.Facing, actual2.Facing);
		}


		[Test]
		public void RotateLeft()
		{
			var map = new Map(5, 5);
			var p2 = new Position { X = 3, Y = 3, Facing = Dir.East };
			var rover2 = new MarsVehicle(p2);
			const string movement2 = "l";

			rover2.ApplyMovementCommands(movement2);
			rover2.MoveRover(map);

			var actual2 = rover2.CurrentPosition;
			var expected2 = new Position { X = 3, Y = 3, Facing = Dir.North };

			Assert.AreEqual(expected2.X, actual2.X);
			Assert.AreEqual(expected2.Y, actual2.Y);
			Assert.AreEqual(expected2.Facing, actual2.Facing);
		}


		[Test]
		public void MoveForward()
		{
			var map = new Map(10, 10);
			var p2 = new Position { X = 3, Y = 3, Facing = Dir.East };
			var rover2 = new MarsVehicle(p2);
			const string movement2 = "mmm";

			rover2.ApplyMovementCommands(movement2);
			rover2.MoveRover(map);

			var actual2 = rover2.CurrentPosition;
			var expected2 = new Position { X = 6, Y = 3, Facing = Dir.East };

			Assert.AreEqual(expected2.X, actual2.X);
			Assert.AreEqual(expected2.Y, actual2.Y);
			Assert.AreEqual(expected2.Facing, actual2.Facing);
		}

		[Test]
		public void MoveMakeAnL()
		{
			var map = new Map(10, 10);
			var p2 = new Position { X = 3, Y = 3, Facing = Dir.East };
			var rover2 = new MarsVehicle(p2);
			const string movement2 = "mmmLmmm";

			rover2.ApplyMovementCommands(movement2);
			rover2.MoveRover(map);

			var actual2 = rover2.CurrentPosition;
			var expected2 = new Position { X = 6, Y = 6, Facing = Dir.North };

			Assert.AreEqual(expected2.X, actual2.X);
			Assert.AreEqual(expected2.Y, actual2.Y);
			Assert.AreEqual(expected2.Facing, actual2.Facing);
		}


	}
}