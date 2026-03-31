using Application.DTOs.Requests;
using Domain.Entities;

namespace Application.Mappers;

public sealed class PlanMapper
{
    public void UpdateEntity(Plan entity, PlanRequest request)
    {
       entity.Name = request.Name;
       entity.AnsRegistrationCode = request.AnsRegistrationCode;
    }
}