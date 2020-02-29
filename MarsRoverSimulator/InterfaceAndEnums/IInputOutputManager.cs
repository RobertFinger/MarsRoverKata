namespace MarsRoverSimulator.InterfaceAndEnums
{
    public interface IInputOutputManager
    {
	    public string GetUserResponse(string input);
	    public void SendTextToUser(string input);
    }
}
