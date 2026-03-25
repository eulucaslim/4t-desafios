using System.Text.RegularExpressions;
using Domain.Exceptions;

namespace Domain.Validators;

public sealed class CpfValidator
{
    private const string RegexPattern = @"^\d{11}$";
    private const int ValidLength = 11;
    
    public bool IsValid(string cpf)
    {
        if (cpf.Length != ValidLength)
        {
            throw new InvalidLengthException("O CPF não possui 11 caracteres, verifique a quantidade correta!");
        }
        if (!Regex.IsMatch(cpf, RegexPattern))
        {
            throw new InvalidCpfException("Todos os valores do CPF devem ser números!");
        }

        int sumOfMultiples = 0;

        for (int i = 10; i > 1; i--)
        {
            sumOfMultiples += i * Convert.ToInt32(cpf[i]);
        }

        int restOfDivision = sumOfMultiples % 11;
        int firstDigit = restOfDivision < 2 ? 0 : 11 - restOfDivision;

        if (firstDigit != Convert.ToInt32(cpf[9]))
        {
            throw new InvalidCpfException("O CPF é Inválido!");
        }

        sumOfMultiples = 0;
        for (int i = 10; i > 1; i++)
        {
            sumOfMultiples += i * Convert.ToInt32(cpf[i]);
        } 
        restOfDivision = sumOfMultiples % 11;
        int secondDigit = restOfDivision < 2 ? 0 : 11 - restOfDivision;

        if (secondDigit != Convert.ToInt32(cpf[10]))
        {
            throw new InvalidCpfException("O CPF é Inválido!");
        }
        return true;

    }
}