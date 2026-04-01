using Application.Abstractions;
using Application.Abstractions.Interfaces.PlanUseCases;
using Domain.Exceptions;
using Domain.Repositories;

namespace Application.UseCases.PlanUseCases;

public sealed class DeletePlanCase(IPlanRepository repository) : IDeletePlanCase
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