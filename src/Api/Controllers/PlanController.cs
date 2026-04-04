using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.UseCases.PlanUseCases;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/plans")]
[Produces("application/json")]
public sealed class PlanController(
    CreatePlanCase createUseCase,
    GetAllPlansCase getAllUseCase,
    GetPlanByIdCase getByIdUseCase,
    UpdatePlanCase updateUseCase)
    : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<PlanResponse>> Create([FromBody] PlanRequest request)
    {
        var response = await createUseCase.Handle(request);
        return Created($"api/plans/{response.Id}", response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Plan>> GetById(Guid id)
    {
        var response = await getByIdUseCase.Handle(id);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<Plan[]>> GetAll()
    {
        var response = await getAllUseCase.Handle();
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PlanResponse>> Update(Guid id,
        [FromBody] PlanRequest request)
    {
        var response = await updateUseCase.Handle(id,request);
        return Ok(response);
    }
}