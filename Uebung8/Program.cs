namespace Uebung8;

internal static class Program
{
    static void Main(string[] _)
    {
        var warrior = new Warrior("Himmel", 100, new WarriorAttack());
        var wizard = new Wizard("Frieren", 75, new WizardAttack());
        
        var game = new Game();
        game.Battle(warrior, wizard);
    }
}