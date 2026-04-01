using Application.DTOs.Requests;
using Application.DTOs.Responses;

namespace Application.Abstractions.Interfaces.BeneficiaryUseCases;

public interface ICreateBeneficiaryCase: IUseCase<BeneficiaryRequest, BeneficiaryResponse>;