namespace Uebung3;

using System;

static class Calculator
{
    static void Main()
    {
        string? input = null;
        while (input == null)
        {
            Console.WriteLine("Geben Sie die Berechnung ein (Beispiel: 2 + 3):");
            input = Console.ReadLine();
        }
        input = input.Replace(" ", "");

        char operatorChar = ' ';
        float operand1 = 0;
        float operand2 = 0;
        
        foreach (char c in input)
        {
            if (c == '+' || c == '-' || c == '*' || c == '/')
            {
                operatorChar = c;
                string[] operands = input.Split(operatorChar);
                operand1 = float.Parse(operands[0]);
                operand2 = float.Parse(operands[1]);
                break;
            }
        }

        switch (operatorChar)
        {
            case '+':
                Console.WriteLine($"{operand1} + {operand2} = {operand1 + operand2}");
                break;
            case '-':
                Console.WriteLine($"{operand1} - {operand2} = {operand1 - operand2}");
                break;
            case '*':
                Console.WriteLine($"{operand1} * {operand2} = {operand1 * operand2}");
                break;
            case '/':
                if (operand2 != 0)
                {
                    double quotient = operand1 / operand2;
                    Console.WriteLine($"{operand1} / {operand2} = {quotient:F2}");
                }
                else
                {
                    Console.WriteLine("Division durch Null ist nicht erlaubt.");
                }
                break;
            default:
                Console.WriteLine("Ungültiger Operator.");
                break;
        }
    }
}