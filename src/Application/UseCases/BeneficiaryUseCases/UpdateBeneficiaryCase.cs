using Application.Abstractions;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Repositories;

namespace Application.UseCases.BeneficiaryUseCases;

public record UpdateBeneficiaryRequest(
    string FullName,
    string Cpf,
    Status Status,
    DateOnly BirthDate,
    Guid PlanId
);

public record UpdateBeneficiaryResponse(
    Guid Id,
    string FullName,
    string Cpf,
    DateOnly BirthDate,
    DateTime RegistrationDate,
    Status Status,
    Guid PlanId
);
public class UpdateBeneficiaryCase(IBeneficiaryRepository repository) : IUseCase<Guid, UpdateBeneficiaryRequest, UpdateBeneficiaryResponse> 
{
    public async Task<UpdateBeneficiaryResponse> Handle(Guid id, UpdateBeneficiaryRequest request)
    {
        var beneficiary = await repository.GetByIdAsync(id);
        if (beneficiary == null)
        {
            throw new EntityNotFound("The Beneficiary with id = " + id + " not found");
        }
        var beneficiaryUpdated  = await repository.UpdateAsync(id, beneficiary);

        return new UpdateBeneficiaryResponse(
            beneficiaryUpdated.Id,
            beneficiaryUpdated.FullName,
            beneficiaryUpdated.Cpf,
            beneficiaryUpdated.BirthDate,
            beneficiaryUpdated.RegistrationDate,
            beneficiaryUpdated.Status,
            beneficiaryUpdated.PlanId
        );
    }
}