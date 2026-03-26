using Application.Abstractions;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Validators;

namespace Application.UseCases.BeneficiaryUseCases;

public record CreateBeneficiaryRequest(
    string FullName,
    string Cpf,
    DateOnly BirthDate,
    Status Status,
    Guid PlanId
    );

public record CreateBeneficiaryResponse(
    Guid Id,
    string FullName,
    string Cpf,
    DateOnly BirthDate,
    DateTime RegistrationDate,
    Status Status,
    Guid PlanId
    );

public sealed class CreateBeneficiaryCase(IBeneficiaryRepository repository, IPlanRepository planRepository) : IUseCase<CreateBeneficiaryRequest, CreateBeneficiaryResponse>
{
    public async Task<CreateBeneficiaryResponse> Handle(CreateBeneficiaryRequest request)
    {
        CpfValidator.IsValid(request.Cpf);
        
        var beneficiary = await repository.GetByCpfAsync(request.Cpf);
        if (beneficiary != null)
        {
            throw new EntityAlreadyExists(
                "Beneficiario com esse CPF " + beneficiary.Cpf +" já existe!"
                );
        }
        
        var plan = await planRepository.GetByIdAsync(request.PlanId);
        
        var entity = new Beneficiary(
            request.FullName,
            request.Cpf,
            request.BirthDate,
            request.Status,
            plan!.Id
        );
        
        var beneficiaryCreated  = await repository.CreateAsync(entity);

        return new CreateBeneficiaryResponse(
            beneficiaryCreated.Id,
            beneficiaryCreated.FullName,
            beneficiaryCreated.Cpf,
            beneficiaryCreated.BirthDate,
            beneficiaryCreated.RegistrationDate,
            beneficiaryCreated.Status,
            beneficiaryCreated.PlanId
        );

    }
}

