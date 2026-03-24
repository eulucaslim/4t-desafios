using Domain.Entities;

namespace Application.Repositories;

public interface IBeneficiaryRepository
{
    Beneficiary Create(Beneficiary beneficiary);
    void Delete(Guid id);
    Beneficiary? Update(Guid id, Beneficiary beneficiary);
    Beneficiary? Get(Guid id);
    IEnumerable<Beneficiary> GetAll();
    
}