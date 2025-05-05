namespace Uebung8;

interface IItem
{
    string Name { get; }
    void ApplyEffect(ICharacter character);
}