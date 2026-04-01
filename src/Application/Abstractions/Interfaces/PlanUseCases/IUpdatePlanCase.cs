using Application.DTOs.Requests;
using Application.DTOs.Responses;

namespace Application.Abstractions.Interfaces.PlanUseCases;

public interface IUpdatePlanCase
    : IUseCaseWithId<Guid, PlanRequest, PlanResponse>
{
}