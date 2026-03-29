using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.UseCases.BeneficiaryUseCases;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using GetAllBeneficiariesCase = Application.UseCases.BeneficiaryUseCases.GetAllBeneficiariesCase;

namespace Api.Controllers;

[ApiController]
[Route("api/beneficiaries")]
[Produces("application/json")]
public sealed class BeneficiaryController(
    CreateBeneficiaryCase createUseCase,
    GetAllBeneficiariesCase getAllUseCase,
    GetBeneficiaryByIdCase getByIdUseCase,
    UpdateBeneficiaryCase updateUseCase)
    : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<BeneficiaryResponse>> Create([FromBody] BeneficiaryRequest request)
    {
        var response = await createUseCase.Handle(request);
        return Created($"api/beneficiaries/{response.Id}", response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Beneficiary>> GetById(Guid id)
    {
        var response = await getByIdUseCase.Handle(id);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<Beneficiary[]>> GetAll()
    {
        var response = await getAllUseCase.Handle();
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Beneficiary>> Update(Guid id,
        [FromBody] BeneficiaryRequest request)
    {
        var response = await updateUseCase.Handle(id,request);
        return Ok(response);
    }
    
    
}