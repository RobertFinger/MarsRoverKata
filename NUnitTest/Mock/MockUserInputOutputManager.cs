using MarsRoverSimulator.InterfaceAndEnums;

namespace NUnitTest.Mock
{
	public class MockUserInputOutputManager : IInputOutputManager
	{
		public int TestNumber { get; set; }
		
		public string GetUserResponse(string input)
		{
			switch (input)
			{
				case "Enter Graph Upper Right Coordinate (X Y)":
					return SetMapCoords();
			}

			return string.Empty;
		}

		private string SetMapCoords()
		{
			switch (TestNumber)
			{
				case 1:
					return "0 0";
				case 2:
					return "5,5";
				case 3:
					return " 0 0    ";
				case 4:
					return "apple banana";
				case 5:
					return "1,a";
				default:
					return "5 5";


			}
		}


		public void SendTextToUser(string input)
		{
			return;
		}
	}
}