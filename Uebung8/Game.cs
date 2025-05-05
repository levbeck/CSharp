namespace Uebung8;

class Game
{
    private readonly Random _random = new();
    
    public void Battle(ICharacter character1, ICharacter character2)
    {
        Console.WriteLine($"Battle between {character1.Name} and {character2.Name} starts!");
        Console.WriteLine();
        
        var healthPotion = new HealthPotion();
        int round = 1;
        
        while (character1.Health > 0 && character2.Health > 0)
        {
            Console.WriteLine($"Round {round}");
            Console.WriteLine("-------------------------");
            
            // Character 1's turn
            ProcessTurn(character1, character2, healthPotion);
            
            if (character2.Health <= 0)
            {
                Console.WriteLine($"{character1.Name} wins!");
                Console.WriteLine();
                break;
            }
            
            // Character 2's turn
            ProcessTurn(character2, character1, healthPotion);
            
            if (character1.Health <= 0)
            {
                Console.WriteLine($"{character2.Name} wins!");
                Console.WriteLine();
                break;
            }
            
            round++;
            Console.WriteLine();
            Thread.Sleep(1000); // Pause between rounds
        }
    }
    
    private void ProcessTurn(ICharacter attacker, ICharacter defender, IItem healthPotion)
    {
        // Try to use health potion
        attacker.TryUseItem(healthPotion);
        
        // Calculate attack damage
        int damage = attacker.Attack.Damage;
        
        // Check for double attack
        if (attacker.Attack.HasDoubleAttackChance)
        {
            int doubleAttackRoll = _random.Next(1, 7);
            if (doubleAttackRoll == 2)
            {
                Console.WriteLine($"{attacker.Name} is in great shape and attacks twice!");
                damage *= 2;
            }
        }
        
        // Execute attack
        Console.WriteLine($"{attacker.Name} attacks with {attacker.Attack.Type}!");
        defender.Defend(damage);
    }
}