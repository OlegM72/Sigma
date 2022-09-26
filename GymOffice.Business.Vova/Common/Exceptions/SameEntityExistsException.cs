namespace GymOffice.Business.Common.Exceptions;
public class SameEntityExistsException : Exception
{
	public SameEntityExistsException(string name, object key)
		: base($"Entity \"{name}\" with the same property ({key}) already exists")
    {

	}
}
