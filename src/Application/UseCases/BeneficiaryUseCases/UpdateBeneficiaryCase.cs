using Application.Abstractions;
using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Mappers;
using Domain.Exceptions;
using Domain.Repositories;

namespace Application.UseCases.BeneficiaryUseCases;

public class UpdateBeneficiaryCase(IBeneficiaryRepository repository, BeneficiaryMapper mapper)
    : IUseCaseWithId<Guid, BeneficiaryRequest, BeneficiaryResponse>
{
    public async Task<BeneficiaryResponse> Handle(Guid id, BeneficiaryRequest request)
    {
        var beneficiary = await repository.GetByIdAsync(id);
        if (beneficiary == null) throw new EntityNotFound("The Beneficiary with id = " + id + " not found");
        mapper.UpdateEntity(beneficiary, request);
        var beneficiaryUpdated = await repository.UpdateAsync(id, beneficiary);

        return new BeneficiaryResponse(
            beneficiaryUpdated.Id,
            beneficiaryUpdated.FullName,
            beneficiaryUpdated.Cpf,
            beneficiaryUpdated.BirthDate,
            beneficiaryUpdated.RegistrationDate,
            beneficiaryUpdated.Status.ToString(),
            beneficiaryUpdated.PlanId
        );
    }
}