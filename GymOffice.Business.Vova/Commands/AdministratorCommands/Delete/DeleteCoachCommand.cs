namespace GymOffice.Business.Commands.AdministratorCommands.Delete;
public class DeleteCoachCommand : IDeleteCoachCommand
{
    private readonly ICoachRepository _coachRepository;

    public DeleteCoachCommand(ICoachRepository coachRepository)
    {
        _coachRepository = coachRepository;
    }

    public async Task<Coach> ExecuteAsync(Guid coachId)
    {
        Coach? coach = await _coachRepository.GetByIdAsync(coachId);

        if (coach == null)
        {
            throw new NotFoundException(nameof(Coach), coachId);
        }
        if (coach.GroupTrainings != null && coach.GroupTrainings.Any())
        {
            throw new CannotDeleteCoachException(nameof(GroupTraining));
        }
        if (coach.PersonalTrainings != null && coach.PersonalTrainings.Any())
        {
            throw new CannotDeleteCoachException(nameof(PersonalTraining));
        }

        return await _coachRepository.DeleteCoachAsync(coachId);
    }
}
