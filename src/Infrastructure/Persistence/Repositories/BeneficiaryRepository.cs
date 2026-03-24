using Application.Repositories; 
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class BeneficiaryRepository(AppDbContext context) : IBeneficiaryRepository
{

    public Beneficiary Create(Beneficiary beneficiary)
    {
        context.Beneficiaries.Add(beneficiary);
        context.SaveChanges();
        return beneficiary;
    }

    public void Delete(Guid id)
    {
        var beneficiary = context.Beneficiaries.Find(id);

        if (beneficiary == null)
        {
            throw new ArgumentNullException("The Beneficiary with id = " + id + " does not exist");
        }
        context.Beneficiaries.Remove(beneficiary);
        context.SaveChanges();
    }

    public Beneficiary Update(Guid id, Beneficiary beneficiaryToUpdate)
    {
        var beneficiary = context.Beneficiaries.Find(id);
        if (beneficiary == null)
        {
            throw new ArgumentNullException("The Beneficiary with id = " + id + " does not exist");
        }
        context.Beneficiaries.Update(beneficiaryToUpdate);
        context.SaveChanges();
        return beneficiaryToUpdate;
    }

    public Beneficiary Get(Guid id)
    {
        var beneficiary = context.Beneficiaries.Find(id);
        return beneficiary ?? throw new ArgumentNullException("The Beneficiary with id = " + id + " not found");
    }

    public IEnumerable<Beneficiary> GetAll()
    {
        var beneficiaries = context.Beneficiaries.ToList();
        return beneficiaries;
    }
}