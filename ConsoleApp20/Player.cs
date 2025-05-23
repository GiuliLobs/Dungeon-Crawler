using System.Data;
using System.Numerics;

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
        public int AttackRange { get; set; } = 1;
        public int Defense { get; set; } = 0;
        public Item Item { get; set; }
        public List<Armor> Armors = new List<Armor>();
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
            Item = new Weapon("Pugni", "P", Damage);
        }
        public void Attack(Monster monster)
        {
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
    }
}
