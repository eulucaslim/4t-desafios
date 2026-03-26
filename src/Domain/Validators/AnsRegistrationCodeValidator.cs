using System.Text.RegularExpressions;
using Domain.Exceptions;

namespace Domain.Validators;

public class AnsRegistrationCodeValidator: IValidator
{
    private const string RegexPattern = @"^ANS-\d{6}$";
    
    public static void IsValid(string value)
    {
        if (!Regex.IsMatch(value, RegexPattern))
        {
            throw new InvalidAnsCode("O Código verficado não possui o padrão correto!");
        }
        
    }
}