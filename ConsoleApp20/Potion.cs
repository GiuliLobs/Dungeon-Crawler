using System.Security.Cryptography;

namespace Blastangel
{
    internal class Potion : Item
    {
        public int HealAmount { get; set; }
        public Potion(string name, string s)
            : base(name, s)
        {
            HealAmount = RandomNumberGenerator.GetInt32(60, 81);
            Symbol = "♥";
            Name = "Pozione di vita";
        }
    }
}
