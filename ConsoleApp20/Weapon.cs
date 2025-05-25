using System.Security.Cryptography;

namespace Blastangel
{
    internal class Weapon : Item
    {
        public int Damage { get; set; }
        public int ProbCrit { get; set; }
        public int Crit { get; set; }
        public Weapon(string name, string s, Rarity rarity)
            : base(name, s, rarity)
        {
            Name = "Pugni";
            Damage = 15;
            ProbCrit = 55;
            Crit = 18;
        }
        public Weapon(string name, string s, int damage, Rarity rarity, int probCrit, double critDamagePerc)
            : base(name, s, rarity)
        {
            Symbol = s;
            Name = name;
            Damage = damage;
            ProbCrit = probCrit;
            Crit = Convert.ToInt32(Damage * critDamagePerc);
            ItemRarity = rarity;
        }
    }
}
