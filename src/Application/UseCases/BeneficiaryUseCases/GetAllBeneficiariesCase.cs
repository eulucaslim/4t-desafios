using System.Collections;
using Application.Abstractions;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCases.BeneficiaryUseCases;

public sealed class GetAllBeneficiariesCase(IBeneficiaryRepository repository) : IUseCase
{
    public async Task<ICollection> Handle()
    {
        return await repository.GetAllAsync();
    }
}