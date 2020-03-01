using System;
using System.Security.Cryptography.X509Certificates;
using MarsRoverSimulator.InterfaceAndEnums;
using MarsRoverSimulator.Rover;
using MarsRoverSimulator.UserInterface;
using NUnit.Framework;
using NUnitTest.Mock;

namespace NUnitTest
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void MoveRover1()
		{
			var map = new Map(5,5);
			var p = new Position(){X = 1, Y=2, Facing = Dir.North};
			var rover = new MarsVehicle(p);
			var movement = "LMLMLMLMM";

			rover.ApplyMovementCommands(movement);
			rover.MoveRover(map);

			var actual = rover.CurrentPosition;
			var expected = new Position() { X = 1, Y = 3, Facing = Dir.North };

			// we can serialize the objects to json and compare them that way, but these are tiny objects and we'd like to know exactly where it failed.

			Assert.AreEqual(expected.X, actual.X);
			Assert.AreEqual(expected.Y, actual.Y);
			Assert.AreEqual(expected.Facing, actual.Facing);
		}


		[Test]
		public void MoveRover2()
		{
			var map = new Map(5, 5);
			var p2 = new Position() { X = 3, Y = 3, Facing = Dir.East };
			var rover2 = new MarsVehicle(p2);
			var movement2 = "MMRMMRMRRM";

			rover2.ApplyMovementCommands(movement2);
			rover2.MoveRover(map);

			var actual2 = rover2.CurrentPosition;
			var expected2 = new Position() { X = 5, Y = 1, Facing = Dir.East };

			// we can serialize the objects to json and compare them that way, but these are tiny objects and we'd like to know exactly where it failed.

			Assert.AreEqual(expected2.X, actual2.X);
			Assert.AreEqual(expected2.Y, actual2.Y);
			Assert.AreEqual(expected2.Facing, actual2.Facing);



		}


	}
}