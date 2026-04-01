using Domain.Entities;

namespace Application.Abstractions.Interfaces.BeneficiaryUseCases;

public interface IGetBeneficiaryByIdCase : IUseCase<Guid, Beneficiary>
{
}