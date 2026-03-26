using Domain.Entities;

namespace Domain.Repositories;

public interface IPlanRepository
{
    Task<Plan> CreateAsync(Plan plan);
    Task DeleteAsync(Guid id);
    Task<Plan> UpdateAsync(Guid id, Plan plan);
    Task<Plan?> GetByIdAsync(Guid id);
    Task<Plan?> GetByAnsCode(string code);
    Task<List<Plan>> GetAllAsync();
}