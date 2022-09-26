namespace GymOffice.Business.Common.Exceptions;
public class CannotDeleteCoachException : Exception
{
	public CannotDeleteCoachException(string reason)
		: base($"The coach cannot be deleted because he/she has \"{reason}\"")
	{
		
	}
}
