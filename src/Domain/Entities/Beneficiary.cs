namespace Domain;

public class Beneficiary
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
    public required string Cpf { get; set; }
    public DateTime BirthDate { get; set;  }
    public Status Status { get; set; }
    public Guid PlanId { get; set; }
    public DateTime RegistrationDate { get; set; }
}