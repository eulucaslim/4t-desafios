namespace Application.DTOs.Requests;

public record PlanRequest(
    string Name,
    string AnsRegistrationCode
);