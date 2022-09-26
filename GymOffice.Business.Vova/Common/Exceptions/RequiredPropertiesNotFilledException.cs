namespace GymOffice.Business.Common.Exceptions;
public class RequiredPropertiesNotFilledException : Exception
{
	public RequiredPropertiesNotFilledException()
		: base("Not all required entity properties are filled")
	{

	}

}
