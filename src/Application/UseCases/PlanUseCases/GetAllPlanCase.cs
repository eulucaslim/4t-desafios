using System.Collections;
using Application.Abstractions;
using Application.Abstractions.Interfaces.PlanUseCases;
using Domain.Repositories;

namespace Application.UseCases.PlanUseCases;

public sealed class GetAllPlansCase(IPlanRepository repository) : IGetAllPlansCase
{
    public async Task<ICollection> Handle()
    {
        return await repository.GetAllAsync();
    }
}