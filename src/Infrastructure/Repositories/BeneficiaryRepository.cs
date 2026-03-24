using Application.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class BeneficiaryRepository(AppDbContext context) : IBeneficiaryRepository
{

    public Beneficiary Create(Beneficiary beneficiary)
    {
        throw new NotImplementedException();
    }

    public Beneficiary GetById(Guid beneficiaryId)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid beneficiaryId)
    {
        throw new NotImplementedException();
    }

    public List<Beneficiary> GetAll()
    {
        throw new NotImplementedException();
    }
}