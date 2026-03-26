using Domain.Enums;

namespace Api.Dto.Responses;

public record UpdateBeneficiaryResponse(
    Guid Id,
    string FullName,
    string Cpf,
    DateOnly BirthDate,
    DateTime RegistrationDate,
    Status Status,
    Guid PlanId
);