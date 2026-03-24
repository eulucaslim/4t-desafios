using Domain.Repositories; 
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class BeneficiaryRepository(AppDbContext context) : IBeneficiaryRepository
{

    public async Task<Beneficiary> CreateAsync(Beneficiary beneficiary)
    {
        await context.Beneficiaries.AddAsync(beneficiary);
        await context.SaveChangesAsync();
        return beneficiary;
    }

    public async Task DeleteAsync(Guid id)
    {
        var beneficiary = await context.Beneficiaries.FirstOrDefaultAsync(b => b.Id == id);

        if (beneficiary == null)
        {
            throw new ArgumentNullException("The Beneficiary with id = " + id + " does not exist");
        }
        context.Beneficiaries.Remove(beneficiary);
        await context.SaveChangesAsync();
    }

    public async Task<Beneficiary> UpdateAsync(Guid id, Beneficiary beneficiaryToUpdate)
    {
        var beneficiary = await context.Beneficiaries.FirstOrDefaultAsync(b => b.Id == id);
        
        if (beneficiary == null)
        {
            throw new ArgumentNullException("The Beneficiary with id = " + id + " does not exist");
        }
        
        context.Beneficiaries.Update(beneficiaryToUpdate);
        await context.SaveChangesAsync();
        return beneficiaryToUpdate;
    }

    public async Task<Beneficiary> GetByIdAsync(Guid id)
    {
        var beneficiary = await context.Beneficiaries.FirstOrDefaultAsync(b => b.Id == id);
        return beneficiary ?? throw new ArgumentNullException("The Beneficiary with id = " + id + " not found");
    }

    public async Task<List<Beneficiary>> GetAllAsync()
    {
        var beneficiaries = await context.Beneficiaries.ToListAsync();
        return beneficiaries;
    }
}