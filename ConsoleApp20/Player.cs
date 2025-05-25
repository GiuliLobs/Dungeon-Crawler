using System.Data;
using System.Numerics;
using System.Security.Cryptography;

namespace Blastangel
{
    internal class Player
    {
        char _symbol = '♪'; //✠
        char _attSymbol = '*';
        int roomX;
        int roomY;
        int damage;
        int health;
        public char Symbol { get { return _symbol; } }
        public char AttSymbol { get { return _attSymbol; } }
        public int X { get; set; }
        public int Y { get; set; }
        public int RoomX { get { return roomX; } set { roomX = value; } }
        public int RoomY { get { return roomY; } set { roomY = value; } }
        public int Health { get { return health; } set { health = value; } }
        public int Damage { get { return damage; } set { damage = value; } }
        public int MaxHealth { get; set; }
        public bool isAttacking { get; set; }
        public bool isTaking { get; set; }
        public bool isCrit { get; set; }
        public bool isOnItem { get; set; }
        public int AttackRange { get; set; } = 1;
        public int Defense { get; set; }
        public Weapon Item { get; set; }
        public Armor Helmet { get; set; }
        public Armor Chestplate { get; set; }
        public static readonly Random rnd = new Random();
        internal Player() { }
        internal Player(int x, int y, Map map)
        {
            X = x;
            Y = y;
            Damage = 15;
            MaxHealth = 100;
            Health = MaxHealth;
            RoomX = map.SpawnRoomX;
            RoomY = map.SpawnRoomY;
            isAttacking = false;
            Item = new Weapon("Pugni", "", Rarity.Common);
            Helmet = new Armor("No","",Rarity.Common);
            Chestplate = new Armor("No","",Rarity.Common);
            isOnItem = false;
        }
        public void Attack(Monster monster)
        {
            isCrit = rnd.NextDouble() <= 0.01*Item.ProbCrit;
            if (isCrit)
                monster.Health -= (int)(Damage * 1.5);
            monster.Health -= Damage;
        }
        public void Heal(Potion potion)
        {
            Health += potion.HealAmount;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }
        public void DisplayPlayerStats()
        {
            Console.WriteLine("Vita    Arma   Danno   Difesa");
            Console.WriteLine(Health + "     " + Item.Name + "    " + Damage + "      " + Defense);
        }
        public void EquippedItemInfo(int y, Drop drop)
        {
            var item = drop.Item;
            if (y == 8)
            {
                Console.Write("- Your Item -");
            }
            if (y == 9)
            {
                if (item is Weapon weapon)
                {
                    Console.Write("Item: " + Item.Name);
                }
                else if (item is Armor helmet && Helmet.Name == helmet.Name)
                {
                    Console.Write("Item: " + Helmet.Name);
                }
                else if (item is Armor chestplate && Chestplate.Name == chestplate.Name)
                {
                    Console.Write("Item: " + Chestplate.Name);
                }
            }
            if (y == 10)
            {
                if (item is Weapon weapon)
                {
                    Console.Write("ATK: " + Item.Damage);
                }
                else if (item is Armor helmet && Helmet.Name == helmet.Name)
                {
                    Console.Write("DEF: " + Helmet.Defense);
                }
                else if (item is Armor chestplate && Chestplate.Name == chestplate.Name)
                {
                    Console.Write("DEF: " + Chestplate.Defense);
                }
            }
            if (y == 11)
            {
                if (item is Weapon weapon)
                {
                    Console.Write("Crit: " + Item.ProbCrit + "%");
                }
                else if (item is Armor helmet && Helmet.Name == helmet.Name)
                {
                    Console.Write("Material: " + Helmet.Material);
                }
                else if (item is Armor chestplate && Chestplate.Name == chestplate.Name)
                {
                    Console.Write("Material: " + Chestplate.Material);
                }
            }
            if (y == 12)
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
    }
}
