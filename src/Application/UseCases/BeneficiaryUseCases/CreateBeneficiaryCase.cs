using Application.Abstractions;
using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Validators;

namespace Application.UseCases.BeneficiaryUseCases;

public sealed class CreateBeneficiaryCase(IBeneficiaryRepository repository, IPlanRepository planRepository)
    : IUseCase<BeneficiaryRequest, BeneficiaryResponse>
{
    public async Task<BeneficiaryResponse> Handle(BeneficiaryRequest request)
    {
        CpfValidator.IsValid(request.Cpf);

        var beneficiary = await repository.GetByCpfAsync(request.Cpf);
        if (beneficiary != null)
            throw new EntityAlreadyExists(
                "Beneficiario com esse CPF " + beneficiary.Cpf + " já existe!"
            );

        var plan = await planRepository.GetByIdAsync(request.PlanId);

        var entity = new Beneficiary(
            request.FullName,
            request.Cpf,
            request.BirthDate,
            request.Status,
            plan!.Id
        );

        var beneficiaryCreated = await repository.CreateAsync(entity);

        return new BeneficiaryResponse(
            beneficiaryCreated.Id,
            beneficiaryCreated.FullName,
            beneficiaryCreated.Cpf,
            beneficiaryCreated.BirthDate,
            beneficiaryCreated.RegistrationDate,
            beneficiaryCreated.Status.ToString(),
            beneficiaryCreated.PlanId
        );
    }
}