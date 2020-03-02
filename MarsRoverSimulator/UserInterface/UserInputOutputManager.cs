using System;
using MarsRoverSimulator.InterfaceAndEnums;

namespace MarsRoverSimulator.UserInterface
{
	public class UserInputOutputManager : IInputOutputManager
	{

		// I didn't have a lot of time to do unit testing, but if I did I would mock the io manager to give responses where necessary.
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