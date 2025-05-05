namespace Uebung8;

class Wizard(string name, int health, IAttack attack) : Character(name, health, attack);

class WizardAttack : IAttack
{
    public string Type => "Zoltraak";
    public int Damage => new Random().Next(8, 16); // 8-15 damage
    public bool HasDoubleAttackChance => false;
}