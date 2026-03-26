using Domain.Enums;

namespace Application.DTOs.Requests;

public record CreateBeneficiaryRequest(
    string FullName,
    string Cpf,
    DateOnly BirthDate,
    Status Status,
    Guid PlanId
);