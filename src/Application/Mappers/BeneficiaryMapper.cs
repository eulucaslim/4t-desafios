using Application.DTOs.Requests;
using Domain.Entities;

namespace Application.Mappers;

public sealed class BeneficiaryMapper
{
    public void UpdateEntity(Beneficiary entity, BeneficiaryRequest request)
    {
        entity.FullName = request.FullName;
        entity.Cpf = request.Cpf;
        entity.BirthDate = request.BirthDate;
        entity.Status = request.Status;
        entity.PlanId = request.PlanId;
    }
}