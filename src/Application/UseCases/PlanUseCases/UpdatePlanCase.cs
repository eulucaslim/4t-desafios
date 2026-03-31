using Application.Abstractions;
using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Mappers;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Validators;

namespace Application.UseCases.PlanUseCases;

public class UpdatePlanCase(IPlanRepository repository, PlanMapper mapper) : IUseCaseWithId<Guid, PlanRequest, PlanResponse>
{
    public async Task<PlanResponse> Handle(Guid id, PlanRequest request)
    {
        AnsRegistrationCodeValidator.IsValid(request.AnsRegistrationCode);
        
        var plan = await repository.GetByIdAsync(id);
        if (plan == null) throw new EntityNotFound("The plan with id = " + id + " not found");
        mapper.UpdateEntity(plan, request);
        var planUpdated = await repository.UpdateAsync(id, plan);

        return new PlanResponse(
            planUpdated.Id,
            planUpdated.Name,
            planUpdated.AnsRegistrationCode
        );
    }
}