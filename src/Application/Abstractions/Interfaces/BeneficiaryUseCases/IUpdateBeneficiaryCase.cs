using Application.DTOs.Requests;
using Application.DTOs.Responses;

namespace Application.Abstractions.Interfaces.BeneficiaryUseCases;

public interface IUpdateBeneficiaryCase
    : IUseCaseWithId<Guid, BeneficiaryRequest, BeneficiaryResponse>
{
}