using Domain.Enums;

namespace Domain.Entities;

public class Beneficiary
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
    public required string Cpf { get; set; }
    public DateTime BirthDate { get; set;  }
    public Status Status { get; set; } = Status.Active;
    public Guid PlanId { get; set; }
    public DateTime RegistrationDate { get; set; }
}