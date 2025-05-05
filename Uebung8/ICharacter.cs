namespace Uebung8;

interface ICharacter
{
    IAttack Attack { get; set; }
    string Name { get; }
    int Health { get; set; }
    int HealthPotionsCount { get; }
    void Defend(int damage);
    void TryUseItem(IItem item);
}