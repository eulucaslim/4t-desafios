using Application.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;

namespace Application.UseCases.PlanUseCases;

public sealed class GetPlanByIdCase(IPlanRepository repository) : IUseCase<Guid, Plan>
{
    public async Task<Plan> Handle(Guid id)
    {
        var plan = await repository.GetByIdAsync(id);
        return plan ?? throw new EntityNotFound("The plan with id = " + id + " not found");
    }
}