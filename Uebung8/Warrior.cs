namespace Uebung8;

class Warrior(string name, int health, IAttack attack) : Character(name, health, attack);

class WarriorAttack : IAttack
{
    public string Type => "Sword Slash";
    public int Damage => new Random().Next(5, 11); // 5-10 damage
    public bool HasDoubleAttackChance => true;
}