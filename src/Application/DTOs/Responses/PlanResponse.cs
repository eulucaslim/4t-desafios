namespace Application.DTOs.Responses;

public record PlanResponse(
    Guid Id,
    string Name,
    string AnsRegistrationCode
);