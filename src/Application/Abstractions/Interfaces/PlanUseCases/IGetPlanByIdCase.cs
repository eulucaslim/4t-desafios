using Domain.Entities;

namespace Application.Abstractions.Interfaces.PlanUseCases;

public interface IGetPlanByIdCase : IUseCase<Guid, Plan>
{
}