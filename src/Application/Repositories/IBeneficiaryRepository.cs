using Domain.Entities;

namespace Application.Repositories;

public interface IBeneficiaryRepository
{
    Beneficiary Create(Beneficiary beneficiary);
    Beneficiary GetById(Guid beneficiaryId);
    void Delete(Guid beneficiaryId);
    List<Beneficiary> GetAll();
}