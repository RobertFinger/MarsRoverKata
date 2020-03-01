using System;
using MarsRoverSimulator.InterfaceAndEnums;

namespace MarsRoverSimulator.UserInterface
{
	public class UserInputOutputManager : IInputOutputManager
	{
		public string GetUserResponse(string input)
		{
			Console.WriteLine(input);
			var rv = Console.ReadLine();
			return rv;
		}

		public void SendTextToUser(string input)
		{
			Console.WriteLine(input);
		}
	}
}