using Api.Dto.Responses;
using Application.UseCases.BeneficiaryUseCases;
using Microsoft.AspNetCore.Mvc;
using GetAllBeneficiariesCase = Application.UseCases.BeneficiaryUseCases.GetAllBeneficiariesCase;

namespace Api.Controllers;

[ApiController]
[Route("api/beneficiarios")]
[Produces("application/json")]
public sealed class BeneficiaryController(
    CreateBeneficiaryCase createUseCase,
    GetAllBeneficiariesCase getAllUseCase,
    GetBeneficiaryByIdCase getByIdUseCase,
    UpdateBeneficiaryCase updateUseCase)
    : ControllerBase
{
    
}