using Domain.Enums;

namespace Application.DTOs.Requests;

public record BeneficiaryRequest(
    string FullName,
    string Cpf,
    DateOnly BirthDate,
    Status Status,
    Guid PlanId
);