using Application.Abstractions;
using Domain.Exceptions;
using Domain.Repositories;

namespace Application.UseCases.PlanUseCases;

public record UpdatePlanRequest(
    string Name,
    string AnsRegistrationCode
);

public record UpdatePlanResponse(
    Guid Id,
    string Name,
    string AnsRegistrationCode
);

public class UpdatePlanCase(IPlanRepository repository) : IUseCase<Guid, UpdatePlanRequest, UpdatePlanResponse>
{
    public async Task<UpdatePlanResponse> Handle(Guid id, UpdatePlanRequest request)
    {
        var plan = await repository.GetByIdAsync(id);
        if (plan == null) throw new EntityNotFound("The plan with id = " + id + " not found");
        var planUpdated = await repository.UpdateAsync(id, plan);

        return new UpdatePlanResponse(
            planUpdated.Id,
            planUpdated.Name,
            planUpdated.AnsRegistrationCode
        );
    }
}