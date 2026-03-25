using Domain.Exceptions;

namespace Domain.Validators;

public class AnsRegistrationCodeValidator: IValidator
{
    private const int LengthCode = 6;
    
    public static void IsValid(string code)
    {
        var isAllNumbers = code.All(char.IsDigit);

        if (!isAllNumbers)
        {
            throw new InvalidAnsCode("Todos os valores devem ser números!");
        }

        if (code.StartsWith('0'))
        {
            throw new InvalidAnsCode("O Código ANS não pode ser iniciado por 0!");
        }

        if (code.Length != LengthCode)
        {
            throw new InvalidLengthException("O Código inserido deve ter 6 caracteres!");
        }
        
    }
}