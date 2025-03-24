namespace Uebung5;

public static class Program
{
    private const int StartingBalance = 10000;
    private static Bank _bank = new Bank(StartingBalance);
    private static Dictionary<string, User> _users = new Dictionary<string, User>();

    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8; //Ensures € can be displayed by Console
        
        _bank.TransactionOccurred += OnTransactionOccurred;
        AskForAction();
        while (true)
        {
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ungültiger Befehl. Bitte versuchen Sie es erneut.");
                continue;
            }

            var parts = input.Split(' ');

            if (parts.Length == 1 && parts[0].Equals("stopp", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Programm wird beendet.");
                break;
            }

            switch (parts.Length)
            {
                case 2 when parts[0].Equals("Bank", StringComparison.OrdinalIgnoreCase) && int.TryParse(parts[1], out int bankAmount):
                    ExecuteBankRefillCommand(bankAmount);
                    break;
                case 2 when parts[0].Equals("Guthaben", StringComparison.OrdinalIgnoreCase):
                    ExecuteBalanceInfoCommand(parts[1]);
                    break;
                case 2 when int.TryParse(parts[1], out int amount):
                    ExecuteTransactionCommand(parts[0], amount);
                    break;
                default:
                    Console.WriteLine("Ungültiger Befehl. Bitte versuchen Sie es erneut.");
                    break;
            }
        }
    }

    private static void AskForAction()
    {
        Console.WriteLine("Geben Sie bitte einen gültigen Befehl ein.\n" +
                          "Zur Verfügung stehen:\n" +
                          "stopp\n" +
                          "[Name] [Betrag]\n" +
                          "Bank [Betrag]\n" +
                          "Guthaben [Name]");
    }

    private static void ExecuteBankRefillCommand(int bankAmount)
    {
        _bank.Refill(bankAmount);
        Console.WriteLine($"Die Bank hat {bankAmount}€ gedruckt.");
    }

    private static void ExecuteBalanceInfoCommand(string name)
    {
        if (name.Equals("Bank", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine($"Das Guthaben der Bank beträgt {_bank.Balance}€.");
        }
        else if (_users.TryGetValue(name, out User? user))
        {
            Console.WriteLine($"Das Guthaben von {name} beträgt {user.Balance}€.");
        }
        else
        {
            Console.WriteLine($"Kein Benutzer mit dem Namen {name} gefunden.");
        }
    }

    private static void ExecuteTransactionCommand(string name, int amount)
    {
        try
        {
            if (!_users.ContainsKey(name))
            {
                _users[name] = new User(name, 0);
            }

            _bank.Transfer(_users[name], amount);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void OnTransactionOccurred(object sender, TransactionEventArgs e)
    {
        Console.WriteLine($"Es werden von der Bank {e.Amount}€ an {e.User.Name} überwiesen.");
    }
}

public class Bank(int balance)
{
    public int Balance { get; private set; } = balance;

    public delegate void TransactionEventHandler(object sender, TransactionEventArgs e);

    public event TransactionEventHandler? TransactionOccurred;

    public void Transfer(User user, int amount)
    {
        if (Balance - amount < 0)
        {
            throw new InvalidOperationException("Nicht genügend Guthaben in der Bank.");
        }

        Balance -= amount;
        user.Balance += amount;
        OnTransactionOccurred(new TransactionEventArgs(user, amount));
    }

    public void Refill(int amount)
    {
        Balance += amount;
    }

    private void OnTransactionOccurred(TransactionEventArgs e)
    {
        TransactionOccurred?.Invoke(this, e);
    }
}

public class User(string name, int balance)
{
    public string Name { get; } = name;
    public int Balance { get; set; } = balance;
}

public class TransactionEventArgs(User user, int amount) : EventArgs
{
    public User User { get; } = user;
    public int Amount { get; } = amount;
}