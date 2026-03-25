using Domain.Repositories; 
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class BeneficiaryRepository(AppDbContext context) : IBeneficiaryRepository
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
        if (beneficiary != null) context.Beneficiaries.Remove(beneficiary);
        await context.SaveChangesAsync();
    }

    public async Task<Beneficiary> UpdateAsync(Guid id, Beneficiary beneficiaryToUpdate)
    {
        var beneficiary = await context.Beneficiaries.FirstOrDefaultAsync(b => b.Id == id);
        context.Beneficiaries.Update(beneficiaryToUpdate);
        await context.SaveChangesAsync();
        return beneficiaryToUpdate;
    }

    public async Task<Beneficiary?> GetByIdAsync(Guid id)
    {
        return await context.Beneficiaries.FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Beneficiary?> GetByCpfAsync(string cpf)
    {
        return await context.Beneficiaries.FirstOrDefaultAsync(b => b.Cpf == cpf);
    }

    public async Task<List<Beneficiary>> GetAllAsync()
    {
        var beneficiaries = await context.Beneficiaries.ToListAsync();
        return beneficiaries;
    }
}