namespace Uebung8;

interface IAttack
{
    string Type { get; }
    int Damage { get; }
    bool HasDoubleAttackChance { get; }
}