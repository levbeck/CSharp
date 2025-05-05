namespace Uebung8;

abstract class Character(string name, int health, IAttack attack, int initialPotions = 5)
    : ICharacter
{
    public IAttack Attack { get; set; } = attack;
    public string Name { get; } = name;
    public int Health { get; set; } = health;
    private int _healthPotionsCount = initialPotions;
    public int HealthPotionsCount => _healthPotionsCount;
    private Random Random { get; } = new();

    public virtual void Defend(int damage)
    {
        int roll = Random.Next(1, 7); // Roll 1-6
        
        if (roll == 4)
        {
            damage /= 2;
            Console.WriteLine($"{Name} dodged half of the damage!");
        }
        
        Health -= damage;
        Console.WriteLine($"{Name} received {damage} damage. Remaining health: {Health}");
    }

    public void TryUseItem(IItem item)
    {
        int roll = Random.Next(1, 7); // Roll 1-6
        
        if (roll == 6 && _healthPotionsCount > 0)
        {
            item.ApplyEffect(this);
        }
    }

    public void UseHealthPotion()
    {
        if (_healthPotionsCount > 0)
        {
            _healthPotionsCount--;
        }
    }
}