using Api.Dto.Requests;
using Api.Dto.Responses;
using Application.Abstractions;
using Domain.Exceptions;
using Domain.Repositories;

namespace Application.UseCases.BeneficiaryUseCases;

public class UpdateBeneficiaryCase(IBeneficiaryRepository repository)
    : IUseCase<Guid, UpdateBeneficiaryRequest, UpdateBeneficiaryResponse>
{
    public async Task<UpdateBeneficiaryResponse> Handle(Guid id, UpdateBeneficiaryRequest request)
    {
        var beneficiary = await repository.GetByIdAsync(id);
        if (beneficiary == null) throw new EntityNotFound("The Beneficiary with id = " + id + " not found");
        var beneficiaryUpdated = await repository.UpdateAsync(id, beneficiary);

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