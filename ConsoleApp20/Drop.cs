namespace Blastangel
{
    internal class Drop
    {
        Item item;
        public int X { get; set; }
        public int Y { get; set; }
        public Item Item { get { return item; } set { item = value; } }
        internal Drop(int x, int y, Player player)
        {
            GenerateItem(player);
            X = x;
            Y = y;
        }
        public void TakeDrop(Player player)
        {
            if (X == player.X && Y == player.Y)
            {
                Item.Symbol = " ";
                if (Item is Potion potion)
                {
                    player.Heal(potion);
                }
                else if (Item is Weapon weapon)
                {
                    player.Damage = weapon.Damage;
                    player.Item = weapon;
                }
                else if (Item is Armor armor)
                {
                    if (armor.Name == "Helmet")
                    {
                        if (player.Helmet.Name == armor.Name)
                            player.Defense -= player.Helmet.Defense;

                        player.Helmet = armor;
                    }
                    else if (armor.Name == "Chestplate")
                    {
                        if (player.Chestplate.Name == armor.Name)
                            player.Defense -= player.Chestplate.Defense;

                        player.Chestplate = armor;
                    }
                    player.Defense += armor.Defense;
                }
            }
        }
        public void GenerateItem(Player player)
        {
            Random rnd = new Random();
            Rarity rarity = Rarity.Common;
            int dropIndexArmor;
            int dropIndexWeapon;
            int materialIndex;
            int n;
            double randomIndex;
            bool foundItem = false;
            do
            {
                if (player.Health < 50 && rnd.NextDouble() <= 0.5)
                    n = 0;
                else
                    n = rnd.Next(0, 3);
                randomIndex = rnd.NextDouble();
                dropIndexArmor = rnd.Next(0, 2);
                dropIndexWeapon = rnd.Next(0, 3);
                materialIndex = rnd.Next(0, 3);
                switch (n)
                {
                    case 0:
                        if (randomIndex <= 0.1)
                        {
                            rarity = Rarity.Legendary;
                            foundItem = true;
                        }
                        else if (randomIndex <= 0.3)
                        {
                            rarity = Rarity.Epic;
                            foundItem = true;
                        }
                        else if (randomIndex <= 0.5)
                        {
                            rarity = Rarity.Rare;
                            foundItem = true;
                        }
                        else if (randomIndex <= 0.7)
                        {
                            rarity = Rarity.Uncommon;
                            foundItem = true;
                        }
                        else
                        {
                            foundItem = true;
                        }
                        break;
                    case 1:
                        if (randomIndex <= 0.1 && dropIndexWeapon == 0)
                        {
                            rarity = Rarity.Legendary;
                            foundItem = true;
                        }
                        else if (randomIndex <= 0.3 && (dropIndexWeapon == 0 || dropIndexWeapon == 1))
                        {
                            rarity = Rarity.Epic;
                            foundItem = true;
                        }
                        else if (randomIndex <= 0.5 && (dropIndexWeapon == 0 || dropIndexWeapon == 1 || dropIndexWeapon == 2))
                        {
                            rarity = Rarity.Rare;
                            foundItem = true;
                        }
                        else if (randomIndex <= 0.7 && (dropIndexWeapon == 2 || dropIndexWeapon == 1))
                        {
                            rarity = Rarity.Uncommon;
                            foundItem = true;
                        }
                        else if (dropIndexWeapon == 2)
                        {
                            foundItem = true;
                        }
                        else
                            foundItem = false;
                        break;
                    case 2:
                        if (randomIndex <= 0.1 && materialIndex == 0)
                        {
                            rarity = Rarity.Legendary;
                            foundItem = true;
                        }
                        else if (randomIndex <= 0.3 && (materialIndex == 0 || materialIndex == 1))
                        {
                            rarity = Rarity.Epic;
                            foundItem = true;
                        }
                        else if (randomIndex <= 0.5 && (materialIndex == 0 || materialIndex == 1 || materialIndex == 2))
                        {
                            rarity = Rarity.Rare;
                            foundItem = true;
                        }
                        else if (randomIndex <= 0.7 && (materialIndex == 1 || materialIndex == 2))
                        {
                            rarity = Rarity.Uncommon;
                            foundItem = true;
                        }
                        else if (materialIndex == 2)
                        {
                            rarity = Rarity.Common;
                            foundItem = true;
                        }
                        else
                            foundItem = false;
                        break;
                }
            } while (!foundItem);

            switch (n)
            {
                case 0: //Potion
                    switch (rarity)
                    {
                        case Rarity.Legendary:
                            Item = new Potion("Life potion", "♥", Rarity.Legendary, rnd.Next(80, 101));
                            break;
                        case Rarity.Epic:
                            Item = new Potion("Life potion", "♥", Rarity.Epic, rnd.Next(60, 81));
                            break;
                        case Rarity.Rare:
                            Item = new Potion("Life potion", "♥", Rarity.Rare, rnd.Next(50, 61));
                            break;
                        case Rarity.Uncommon:
                            Item = new Potion("Life potion", "♥", Rarity.Uncommon, rnd.Next(40, 51));
                            break;
                        case Rarity.Common:
                            Item = new Potion("Life potion", "♥", Rarity.Common, rnd.Next(20, 41));
                            break;
                    }
                    break;
                case 1: //Weapon
                    switch (dropIndexWeapon)
                    {
                        case 0:
                            switch (rarity)
                            {
                                case Rarity.Legendary:
                                    Item = new Weapon("Sword", "♦", rnd.Next(37, 39), rarity, 15, 1.2);
                                    break;
                                case Rarity.Epic:
                                    Item = new Weapon("Sword", "♦", rnd.Next(34, 36), rarity, 16, 1.2);
                                    break;
                                case Rarity.Rare:
                                    Item = new Weapon("Sword", "♦", rnd.Next(32, 34), rarity, 17, 1.2);
                                    break;
                            }
                            break;
                        case 1:
                            switch (rarity)
                            {
                                case Rarity.Epic:
                                    Item = new Weapon("Axe", "♠", rnd.Next(30, 32), rarity, 18, 1.3);
                                    break;
                                case Rarity.Rare:
                                    Item = new Weapon("Axe", "♠", rnd.Next(28, 30), rarity, 20, 1.3);
                                    break;
                                case Rarity.Uncommon:
                                    Item = new Weapon("Axe", "♠", rnd.Next(26, 28), rarity, 22, 1.3);
                                    break;
                            }
                            break;
                        case 2:
                            switch (rarity)
                            {
                                case Rarity.Rare:
                                    Item = new Weapon("Mace", "♣", rnd.Next(24, 26), rarity, 25, 1.5);
                                    break;
                                case Rarity.Uncommon:
                                    Item = new Weapon("Mace", "♣", rnd.Next(22, 24), rarity, 27, 1.5);
                                    break;
                                case Rarity.Common:
                                    Item = new Weapon("Mace", "♣", rnd.Next(18, 21), rarity, 30, 1.5);
                                    break;
                            }
                            break;
                    }
                    break;
                case 2: //Armor
                    switch (dropIndexArmor)
                    {
                        case 0:
                            switch (materialIndex)
                            {
                                case 0:
                                    switch (rarity)
                                    {
                                        case Rarity.Legendary:
                                            Item = new Armor("Chestplate", "≡", rnd.Next(24, 26), "Iron", rarity);
                                            break;
                                        case Rarity.Epic:
                                            Item = new Armor("Chestplate", "≡", rnd.Next(22, 24), "Iron", rarity);
                                            break;
                                        case Rarity.Rare:
                                            Item = new Armor("Chestplate", "≡", rnd.Next(20, 22), "Iron", rarity);
                                            break;
                                    }
                                    break;
                                case 1:
                                    switch (rarity)
                                    {
                                        case Rarity.Epic:
                                            Item = new Armor("Chestplate", "≡", rnd.Next(19, 21), "Chainmail", rarity);
                                            break;
                                        case Rarity.Rare:
                                            Item = new Armor("Chestplate", "≡", rnd.Next(17, 19), "Chainmail", rarity);
                                            break;
                                        case Rarity.Uncommon:
                                            Item = new Armor("Chestplate", "≡", rnd.Next(15, 17), "Chainmail", rarity);
                                            break;
                                    }
                                    break;
                                case 2:
                                    switch (rarity)
                                    {
                                        case Rarity.Rare:
                                            Item = new Armor("Chestplate", "≡", rnd.Next(14, 16), "Leather", rarity);
                                            break;
                                        case Rarity.Uncommon:
                                            Item = new Armor("Chestplate", "≡", rnd.Next(12, 14), "Leather", rarity);
                                            break;
                                        case Rarity.Common:
                                            Item = new Armor("Chestplate", "≡", rnd.Next(10, 12), "Leather", rarity);
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case 1:
                            switch (materialIndex)
                            {
                                case 0:
                                    switch (rarity) // Legendary
                                    {
                                        case Rarity.Legendary:
                                            Item = new Armor("Helmet", "∩", rnd.Next(15, 17), "Iron", rarity);
                                            break;
                                        case Rarity.Epic:
                                            Item = new Armor("Helmet", "∩", rnd.Next(13, 15), "Iron", rarity);
                                            break;
                                        case Rarity.Rare:
                                            Item = new Armor("Helmet", "∩", rnd.Next(11, 13), "Iron", rarity);
                                            break;
                                    }
                                    break;
                                case 1:
                                    switch (rarity)
                                    {
                                        case Rarity.Epic:
                                            Item = new Armor("Helmet", "∩", rnd.Next(12, 14), "Chainmail", rarity);
                                            break;
                                        case Rarity.Rare:
                                            Item = new Armor("Helmet", "∩", rnd.Next(10, 12), "Chainmail", rarity);
                                            break;
                                        case Rarity.Uncommon:
                                            Item = new Armor("Helmet", "∩", rnd.Next(8, 10), "Chainmail", rarity);
                                            break;
                                    }
                                    break;
                                case 2:
                                    switch (rarity)
                                    {
                                        case Rarity.Rare:
                                            Item = new Armor("Helmet", "∩", rnd.Next(7, 9), "Leather", rarity);
                                            break;
                                        case Rarity.Uncommon:
                                            Item = new Armor("Helmet", "∩", rnd.Next(5, 7), "Leather", rarity);
                                            break;
                                        case Rarity.Common:
                                            Item = new Armor("Helmet", "∩", rnd.Next(3, 5), "Leather", rarity);
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
            }
        }
    }
}
