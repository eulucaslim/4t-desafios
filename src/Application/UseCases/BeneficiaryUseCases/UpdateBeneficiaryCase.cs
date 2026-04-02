using Application.Abstractions;
using Application.Abstractions.Interfaces.BeneficiaryUseCases;
using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Mappers;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Validators;

namespace Application.UseCases.BeneficiaryUseCases;

public class UpdateBeneficiaryCase(
    IBeneficiaryRepository repository, 
    IPlanRepository planRepository, 
    BeneficiaryMapper mapper)
    : IUpdateBeneficiaryCase
{
    public async Task<BeneficiaryResponse> Handle(Guid id, BeneficiaryRequest request)
    {
        var beneficiary = await repository.GetByIdAsync(id);
        if (beneficiary == null) throw new EntityNotFound("The Beneficiary with id = " + id + " not found");
        
        // Validations
        CpfValidator.IsValid(request.Cpf);
        
        var plan = await planRepository.GetByIdAsync(beneficiary.PlanId);
        if (plan == null)
        {
            throw new EntityNotFound("The plan with id = " + beneficiary.PlanId + " not found");
        }
        
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