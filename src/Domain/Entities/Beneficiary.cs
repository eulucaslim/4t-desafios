using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

public class Beneficiary
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "O nome deve ser inserido", AllowEmptyStrings = false)]
    public string FullName { get; set; }
    [Required(ErrorMessage = "O cpf deve ser inserido", AllowEmptyStrings = false)]
    public string Cpf { get; set; }
    [Required]
    public DateOnly BirthDate { get; set; }
    public Status Status { get; set; } = Status.Active;
    [Required]
    public Guid PlanId { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

    public Beneficiary() { }

    public Beneficiary(string fullName, string cpf, DateOnly birthDate, Status status, Guid planId)
    {
        Id = Guid.CreateVersion7();
        FullName = fullName;
        Cpf = cpf;
        BirthDate = birthDate;
        Status = status;
        PlanId = planId;
    }
}