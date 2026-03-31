using System.Collections;
using Application.Abstractions;
using Domain.Repositories;

namespace Application.UseCases.PlanUseCases;

public sealed class GetAllPlansCase(IPlanRepository repository) : IUseCaseCollection
{
    public async Task<ICollection> Handle()
    {
        return await repository.GetAllAsync();
    }
}