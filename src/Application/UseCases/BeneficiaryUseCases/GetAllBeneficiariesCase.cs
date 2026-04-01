using System.Collections;
using Application.Abstractions;
using Application.Abstractions.Interfaces.BeneficiaryUseCases;
using Domain.Repositories;

namespace Application.UseCases.BeneficiaryUseCases;

public sealed class GetAllBeneficiariesCase(IBeneficiaryRepository repository) : IGetAllBeneficiariesCase
{
    public async Task<ICollection> Handle()
    {
        return await repository.GetAllAsync();
    }
}