using Application.DTOs.Requests;
using Application.DTOs.Responses;

namespace Application.Abstractions.Interfaces.PlanUseCases;

public interface ICreatePlanCase: IUseCase<PlanRequest, PlanResponse>;