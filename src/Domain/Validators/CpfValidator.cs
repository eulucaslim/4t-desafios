using System.Text.RegularExpressions;
using Domain.Exceptions;

namespace Domain.Validators;

public class CpfValidator: IValidator
{
    private const string RegexPattern = @"^\d{11}$";
    private const int ValidLength = 11;
    
    public static void IsValid(string value)
    {
        if (value.Length != ValidLength)
        {
            throw new InvalidLengthException("O CPF não possui 11 caracteres, verifique a quantidade correta!");
        }
        if (!Regex.IsMatch(value, RegexPattern))
        {
            throw new InvalidCpfException("Todos os valores do CPF devem ser números!");
        }

        int sumOfMultiples = 0;

        for (int i = 0; i < 9; i++)
        {
            sumOfMultiples += ( 10 - i ) * Convert.ToInt32(value[i].ToString());
        }

        int restOfDivision = sumOfMultiples % 11;
        int firstDigit = restOfDivision < 2 ? 0 : 11 - restOfDivision;

        if (firstDigit != Convert.ToInt32(value[9].ToString()))
        {
            throw new InvalidCpfException("O CPF é Inválido!");
        }

        sumOfMultiples = 0;
        for (int i = 0; i < 10; i++)
        {
            sumOfMultiples += (11 - i) * Convert.ToInt32(value[i].ToString());
        } 
        restOfDivision = sumOfMultiples % 11;
        int secondDigit = restOfDivision < 2 ? 0 : 11 - restOfDivision;

        if (secondDigit != Convert.ToInt32(value[10].ToString()))
        {
            throw new InvalidCpfException("O CPF é Inválido!");
        }

    }
}