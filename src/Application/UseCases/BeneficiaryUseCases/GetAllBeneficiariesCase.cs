using System.Collections;
using Application.Abstractions;
using Domain.Repositories;

namespace Application.UseCases.BeneficiaryUseCases;

public sealed class GetAllBeneficiariesCase(IBeneficiaryRepository repository) : IUseCaseCollection
{
    public async Task<ICollection> Handle()
    {
        return await repository.GetAllAsync();
    }
}