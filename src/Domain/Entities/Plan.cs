namespace Domain.Entities;

public class Plan
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string AnsRegistrationCode { get; set; } 
    
}