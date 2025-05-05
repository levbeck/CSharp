namespace Uebung8;

class HealthPotion(int healAmount = 20) : IItem
{
    public string Name => "Health Potion";
    private int HealAmount { get; } = healAmount;

    public void ApplyEffect(ICharacter character)
    {
        if (character is Character c && c.HealthPotionsCount > 0)
        {
            c.Health += HealAmount;
            c.UseHealthPotion();
            Console.WriteLine($"{character.Name} used a Health Potion and restored {HealAmount} health! Remaining health: {character.Health}");
        }
    }
}