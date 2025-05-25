using System.Security.Cryptography;

namespace Blastangel
{
    internal class Potion : Item
    {
        public int HealAmount { get; set; }
        public Potion(string name, string s, Rarity rarity, int healAmount)
            : base(name, s, rarity)
        {
            HealAmount = healAmount;
            Symbol = s;
            Name = name;
            ItemRarity = rarity;
        }
    }
}
