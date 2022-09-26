namespace GymOffice.Business.Common.Exceptions;
public class CannotAddCustomerToTrainingException : Exception
{
	public CannotAddCustomerToTrainingException(string trainingName)
		: base($"Cannot add the customer to training \"{trainingName}\" because the training list is full")
	{

	}
}
