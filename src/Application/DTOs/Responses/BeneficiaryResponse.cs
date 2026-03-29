using Domain.Enums;

namespace Application.DTOs.Responses;

public record BeneficiaryResponse(
    Guid Id,
    string FullName,
    string Cpf,
    DateOnly BirthDate,
    DateTime RegistrationDate,
    Status Status,
    Guid PlanId
);