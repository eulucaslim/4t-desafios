using Domain.Entities;

namespace Domain.Repositories;

public interface IBeneficiaryRepository
{
    Task<Beneficiary> CreateAsync(Beneficiary beneficiary);
    Task DeleteAsync(Guid id);
    Task<Beneficiary> UpdateAsync(Guid id, Beneficiary beneficiary);
    Task<Beneficiary> GetByIdAsync(Guid id);
    Task<List<Beneficiary>> GetAllAsync();
    
}