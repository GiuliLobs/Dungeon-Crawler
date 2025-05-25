namespace Blastangel
{
    internal enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }
    internal class Item
    {
        string _symbol;
        Rarity _itemRarity;
        public string Symbol { get { return _symbol; } set { _symbol = value; } }
        public string Name { get; set; }
        public string Type { get; set; }

        public Rarity ItemRarity { get { return _itemRarity; } set { _itemRarity = value; } }
        public Item(string name, string sym, Rarity rarity){}
        public void ItemInfo(int y, Drop drop)
        {
            var item = drop.Item;
            if (y == 2)
            {
                Console.Write("- Item Info -");
            }
            if (y == 3)
            {
                Console.Write("Item: " + item.Name);
            }
            if (y == 4)
            {
                if (item is Weapon weapon)
                {
                    Console.Write("ATK: " + weapon.Damage);
                }
                else if (item is Armor armor)
                {
                    Console.Write("DEF: " + armor.Defense);
                }
                else if (item is Potion potion)
                {
                    Console.Write("HP: +" + potion.HealAmount);
                }
            }
            if (y == 5)
            {
                if (item is Weapon weapon)
                {
                    Console.Write("Crit: " + weapon.ProbCrit + "%");
                }
                else if (item is Armor armor)
                {
                    Console.Write("Material: " + armor.Material);
                }
            }
            if (y == 6)
            {
                if (item is Weapon weapon)
                {
                    Console.Write("Rarity: " + weapon.ItemRarity);
                }
                else if (item is Armor armor)
                {
                    Console.Write("Rarity: " + armor.ItemRarity);
                }
                else if (item is Potion potion)
                {
                    Console.Write("Rarity: " + potion.ItemRarity);
                }
            }
        }
        public void PrintItem()
        {
            switch (ItemRarity)
            {
                case Rarity.Common:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case Rarity.Uncommon:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case Rarity.Rare:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case Rarity.Epic:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case Rarity.Legendary:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
            }
            Console.Write(Symbol);
            Console.ResetColor();
        }
    }
}
