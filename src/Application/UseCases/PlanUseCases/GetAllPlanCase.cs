using System.Collections;
using Application.Abstractions;
using Domain.Repositories;

namespace Application.UseCases.PlanUseCases;

public sealed class GetAllBeneficiariesCase(IPlanRepository repository) : IUseCase
{
    public async Task<ICollection> Handle()
    {
        return await repository.GetAllAsync();
    }
}