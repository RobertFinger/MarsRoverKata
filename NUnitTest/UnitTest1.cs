using System;
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
		public void CanSetMapSize()
		{
			var io = new MockUserInputOutputManager();
			var ui = new UI(io);
			
			// to see the string input, look in the MockUserInputManager.cs file.


			io.TestNumber = 1;
			Assert.AreEqual(ui.GetMapSize(), new Tuple<int, int>(0, 0));

			io.TestNumber = 2; 
			Assert.AreEqual(ui.GetMapSize(), new Tuple<int, int>(5, 5));

		}
	}
}