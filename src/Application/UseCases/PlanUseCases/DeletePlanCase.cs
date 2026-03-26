using Application.Abstractions;
using Domain.Exceptions;
using Domain.Repositories;

namespace Application.UseCases.PlanUseCases;

public sealed class DeletePlanCase(IPlanRepository repository): IUseCase<Guid>
{
    public async Task Handle(Guid id)
    {
        var plan = await repository.GetByIdAsync(id);
        if (plan == null)
        { 
            throw new EntityNotFound("The plan with id = " + id + " not found");
        }
        await repository.DeleteAsync(id);
    }
}