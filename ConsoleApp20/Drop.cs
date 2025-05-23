using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace Blastangel
{
    internal class Drop
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Item Item { get; set; }
        internal Drop(int x, int y, Player player)
        {
            Random rnd = new Random();
            X = x;
            Y = y;
            int n;
            if (player.Health < 50 && rnd.NextDouble() <= 0.5)
                n = 0;
            else
                n = rnd.Next(0, 3);
            switch (n)
            {
                case 0:
                    Item = (new Potion("Pozione", ""));
                    break;
                case 1:
                    Item = (new Weapon("Arma", ""));
                    break;
                case 2:
                    switch (RandomNumberGenerator.GetInt32(0, 6))
                    {
                        case 0:
                            Item = (new Armor("Corazza", "", "Ferro", player));
                            break;
                        case 1:
                            Item = (new Armor("Corazza", "", "Maglia", player));
                            break;
                        case 2:
                            Item = (new Armor("Corazza", "", "Pelle", player));
                            break;
                        case 3:
                            Item = (new Armor("Elmo", "", "Ferro", player));
                            break;
                        case 4:
                            Item = (new Armor("Elmo", "", "Maglia", player));
                            break;
                        case 5:
                            Item = (new Armor("Elmo", "", "Pelle", player));
                            break;
                    }
                    break;

            }
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
                    player.Defense += armor.Defense;
                    if (player.Armors != null)
                        foreach (var a in player.Armors)
                        {
                            if (a.Name == armor.Name)
                            {
                                player.Defense -= a.Defense;
                                player.Armors.Remove(a);
                                break;
                            }
                        }
                    player.Armors.Add(armor);

                }
            }
        }
    }
}
