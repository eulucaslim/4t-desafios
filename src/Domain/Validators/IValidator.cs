namespace Domain.Validators;

public interface IValidator
{
    public static abstract void IsValid(string value);
}