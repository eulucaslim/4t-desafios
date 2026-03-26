using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Plan
{
    public Plan(string name, string ansRegistrationCode)
    {
        Id = Guid.CreateVersion7();
        Name = name;
        AnsRegistrationCode = ansRegistrationCode;
    }

    public Guid Id { get; set; }

    [Required(ErrorMessage = "O nome deve ser inserido", AllowEmptyStrings = false)]
    public string Name { get; set; }

    [Required(ErrorMessage = "O código deve ser inserido", AllowEmptyStrings = false)]
    public string AnsRegistrationCode { get; set; }
}