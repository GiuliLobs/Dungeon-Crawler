using System.Security.Cryptography;

namespace Blastangel
{
    internal class Potion : Item
    {
        public int HealAmount { get; set; }
        public Potion(string name, string s)
            : base(name,s)
        {
            HealAmount = RandomNumberGenerator.GetInt32(10, 20);
            Symbol = "♥";
            Name = "Pozione di vita";
        }
    }
}
