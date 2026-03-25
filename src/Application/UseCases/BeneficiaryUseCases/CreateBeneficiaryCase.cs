using Application.Abstractions;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;

namespace Application.UseCases.BeneficiaryUseCases;

public record CreateBeneficiaryRequest(
    string FullName,
    string Cpf,
    DateOnly BirthDate,
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

public sealed class CreateBeneficiaryCase(IBeneficiaryRepository repository) : IUseCase<CreateBeneficiaryRequest, CreateBeneficiaryResponse>
{
    public Task<CreateBeneficiaryResponse> Handle(CreateBeneficiaryRequest request)
    {
        var entity = new Beneficiary(
            request.FullName,
            request.Cpf,
            request.BirthDate,
            request.PlanId
        );
    }
}

