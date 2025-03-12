using System.Reflection;

namespace Uebung1;

internal class ReflexionAdder
{
    private int a = 1;
    private int b = 2;
    private int c = 3;

    public static void Main(string[] args)
    {
        ReflexionAdder reflexionAdder = new();
        reflexionAdder.DisplayValues();

        Console.WriteLine("Bitte geben Sie den Namen der zu ändernden Variable ein:");
        var variableName = Console.ReadLine();

        if (variableName != null)
        {
            //Check if variable exists
            var field = typeof(ReflexionAdder).GetField(variableName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                Console.WriteLine($"Der aktuelle Wert von '{variableName}' ist '{field.GetValue(reflexionAdder)}'. Geben Sie einen neuen Wert ein.");
                var newValue = Console.ReadLine();
                if (int.TryParse(newValue, out var intValue))
                {
                    field.SetValue(reflexionAdder, intValue);
                    Console.WriteLine($"Der neue Wert von '{variableName}' ist '{field.GetValue(reflexionAdder)}'.");
                }
                else
                {
                    Console.WriteLine("Ungültige Eingabe. Bitte geben Sie eine ganze Zahl ein.");
                }
            }
            else
            {
                Console.WriteLine($"Variable '{variableName}' nicht gefunden.");
            }
        }
        else
        {
            Console.WriteLine("Ungültige Eingabe. Bitte geben Sie einen gültigen Variablennamen ein.");
        }

        reflexionAdder.DisplayValues();
    }

    private void DisplayValues()
    {
        Console.WriteLine($"a = {a}, b = {b}, c = {c} und a + b + c = {a + b + c}");
    }
}