using Application.Abstractions;
using Application.Abstractions.Interfaces.PlanUseCases;
using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Validators;

namespace Application.UseCases.PlanUseCases;

public sealed class CreatePlanCase(IPlanRepository repository) : ICreatePlanCase
{
    public async Task<PlanResponse> Handle(PlanRequest request)
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

        return new PlanResponse(
            planCreated.Id,
            planCreated.Name,
            planCreated.AnsRegistrationCode
        );
    }
}