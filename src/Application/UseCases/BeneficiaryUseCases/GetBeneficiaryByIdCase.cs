using Application.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;

namespace Application.UseCases.BeneficiaryUseCases;

public sealed class GetBeneficiaryByIdCase(IBeneficiaryRepository repository) : IUseCase<Guid, Beneficiary>
{
    public async Task<Beneficiary> Handle(Guid id)
    {
        var beneficiary = await repository.GetByIdAsync(id);
        return beneficiary ?? throw new EntityNotFound("The Beneficiary with id = " + id + " not found");
    }
}