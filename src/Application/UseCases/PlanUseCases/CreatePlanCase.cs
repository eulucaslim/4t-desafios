using Application.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Validators;

namespace Application.UseCases.PlanUseCases;

public record CreatePlanRequest(
    string Name,
    string AnsRegistrationCode
);

public record CreatePlanResponse(
    Guid Id,
    string Name,
    string AnsRegistrationCode
);

public sealed class CreatePlanCase(IPlanRepository repository) : IUseCase<CreatePlanRequest, CreatePlanResponse>
{
    public async Task<CreatePlanResponse> Handle(CreatePlanRequest request)
    {
        // Validation first
        AnsRegistrationCodeValidator.IsValid(request.AnsRegistrationCode);

        var plan = await repository.GetByAnsCode(request.AnsRegistrationCode);
        if (plan != null)
            throw new EntityAlreadyExists(
                "Plano com esse Código de Registro Ans " + plan.AnsRegistrationCode + " já existe!"
            );
        var entity = new Plan(
            request.Name,
            request.AnsRegistrationCode
        );

        var planCreated = await repository.CreateAsync(entity);

        return new CreatePlanResponse(
            planCreated.Id,
            planCreated.Name,
            planCreated.AnsRegistrationCode
        );
    }
}