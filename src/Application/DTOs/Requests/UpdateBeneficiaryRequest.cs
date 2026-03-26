using Domain.Enums;

namespace Api.Dto.Requests;

public record UpdateBeneficiaryRequest(
    string FullName,
    string Cpf,
    Status Status,
    DateOnly BirthDate,
    Guid PlanId
);