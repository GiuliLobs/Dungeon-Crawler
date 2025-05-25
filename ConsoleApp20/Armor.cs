namespace Blastangel
{
    internal class Armor : Item
    {
        public int Defense { get; set; }
        public string Material { get; set; }
        public Armor(string name, string sym, Rarity rarity)
            : base(name, sym, rarity)
        {
            Name = name;
            Defense = 0;
            Material = "No";
        }
        public Armor(string name, string sym, int defense, string material, Rarity rarity)
            : base(name, sym, rarity)
        {
            Symbol = sym;
            Defense = defense;
            Name = name;
            Material = material;
            ItemRarity = rarity;
        }
    }
}
