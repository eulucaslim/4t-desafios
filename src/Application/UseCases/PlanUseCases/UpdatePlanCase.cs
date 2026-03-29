using Application.Abstractions;
using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Domain.Exceptions;
using Domain.Repositories;

namespace Application.UseCases.PlanUseCases;

public class UpdatePlanCase(IPlanRepository repository) : IUseCase<Guid, PlanRequest, PlanResponse>
{
    public async Task<PlanResponse> Handle(Guid id, PlanRequest request)
    {
        var plan = await repository.GetByIdAsync(id);
        if (plan == null) throw new EntityNotFound("The plan with id = " + id + " not found");
        var planUpdated = await repository.UpdateAsync(id, plan);

        return new PlanResponse(
            planUpdated.Id,
            planUpdated.Name,
            planUpdated.AnsRegistrationCode
        );
    }
}