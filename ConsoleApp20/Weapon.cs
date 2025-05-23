using System.Security.Cryptography;

namespace Blastangel
{
    internal class Weapon : Item
    {
        public int Damage { get; set; }
        public Weapon(string name, string s, int a)
            : base(name, s)
        {
            Name = "Pugni";
            Damage = a;
        }
        public Weapon(string name, string s)
            : base(name, s)
        {
            switch (RandomNumberGenerator.GetInt32(1, 4))
            {
                case 1:
                    Symbol = "♦";
                    Name = "Spada";
                    Damage = RandomNumberGenerator.GetInt32(35, 41);
                    break;
                case 2:
                    Symbol = "♠";
                    Name = "Ascia";
                    Damage = RandomNumberGenerator.GetInt32(20, 36);
                    break;
                case 3:
                    Symbol = "♣";
                    Name = "Mazza";
                    Damage = RandomNumberGenerator.GetInt32(15, 21);
                    break;
            }
        }
    }
}
