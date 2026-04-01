using Application.Abstractions;
using Application.Abstractions.Interfaces.BeneficiaryUseCases;
using Domain.Exceptions;
using Domain.Repositories;

namespace Application.UseCases.BeneficiaryUseCases;

public sealed class DeleteBeneficiaryCase(IBeneficiaryRepository repository) : IDeleteBeneficiaryCase
{
    public async Task Handle(Guid id)
    {
        var beneficiary = await repository.GetByIdAsync(id);
        if (beneficiary == null) throw new EntityNotFound("The Beneficiary with id = " + id + " not found");
        await repository.DeleteAsync(id);
    }
}