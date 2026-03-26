using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class PlanRepository(AppDbContext context) : IPlanRepository
{
    public async Task<Plan> CreateAsync(Plan plan)
    {
        await context.Plans.AddAsync(plan);
        await context.SaveChangesAsync();
        return plan;
    }

    public async Task DeleteAsync(Guid id)
    {
        var plan = await context.Plans.FirstOrDefaultAsync(b => b.Id == id);
        if (plan != null) context.Plans.Remove(plan);
        await context.SaveChangesAsync();
    }

    public async Task<Plan> UpdateAsync(Guid id, Plan planToUpdate)
    {
        context.Plans.Update(planToUpdate);
        await context.SaveChangesAsync();
        return planToUpdate;
    }

    public async Task<Plan?> GetByIdAsync(Guid id)
    {
        return await context.Plans.FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Plan?> GetByAnsCode(string code)
    {
        return await context.Plans.FirstOrDefaultAsync(b => b.AnsRegistrationCode == code);
    }

    public async Task<List<Plan>> GetAllAsync()
    {
        var plans = await context.Plans.ToListAsync();
        return plans;
    }
}