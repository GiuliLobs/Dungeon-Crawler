using System.Security.Cryptography;

namespace Blastangel
{
    internal class Drop
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Item Item { get; set; }
        internal Drop(int x, int y)
        {
            X = x;
            Y = y;
            switch (RandomNumberGenerator.GetInt32(0, 2))
            {
                case 0:
                    Item = (new Potion("Pozione",""));
                    break;
                case 1:
                    Item = (new Weapon("Arma", ""));
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
            }
        }
    }
}
