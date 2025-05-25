namespace Blastangel
{
    internal class Room
    {
        public List<Monster> Mob = new List<Monster>();
        public List<Drop> DropList = new List<Drop>();
        public static readonly Random rnd = new Random();
        public int X { get; set; }
        public int Monsters { get; set; }
        public int Y { get; set; }
        public char[,] Layout { get; set; }

        public Room(int x, int y, char[,] layout, Map map)
        {

            X = x;
            Y = y;
            Layout = layout;
            bool isBossRoom = (X == map.BossRoomX && Y == map.BossRoomY);
            bool isSpawnRoom = (X == map.SpawnRoomX && Y == map.SpawnRoomY);
            for (int i = 0; i < rnd.Next(1, 5); i++)
            {
                if (isSpawnRoom || isBossRoom)
                {
                    if (isBossRoom)
                        Mob.Add(new Monster(rnd.Next(5, 13), rnd.Next(5, 18), 260));
                    return;
                }
                else
                {
                    var mob = new Monster(rnd.Next(5, 13), rnd.Next(5, 18));
                    Mob.Add(mob);
                }
            }
        }
        public void DisplayRoom(Player player)
        {
            
            foreach (var m in Mob)
                m.Update(Layout, player, Mob, this);

            var dropHere = DropList.FirstOrDefault(d => d.X == player.X && d.Y == player.Y);

            if (dropHere != null)
            {
                player.isOnItem = true;
                if (player.isTaking)
                {
                    dropHere.TakeDrop(player);
                    player.isTaking = false;
                    DropList.RemoveAll(d => d.X == player.X && d.Y == player.Y);
                }
            }
            else
                player.isOnItem = false;

            Console.WriteLine();
            for (int r = 0; r < Layout.GetLength(0); r++)
            {
                for (int c = 0; c < Layout.GetLength(1); c++)
                {
                    if (player.isAttacking)
                    {
                        int attX0 = player.X - player.AttackRange;
                        int attX1 = player.X + player.AttackRange;
                        int attY0 = player.Y - player.AttackRange;
                        int attY1 = player.Y + player.AttackRange;
                        if (attX0 == r && player.Y == c ||
                            attX1 == r && player.Y == c ||
                            player.X == r && attY1 == c ||
                            player.X == r && attY0 == c)
                        {
                            if (!(Layout[r, c] == '█'))
                            {
                                Console.Write(player.AttSymbol);
                                c++;
                            }
                        }
                    }
                    if (player.X == r && player.Y == c)
                    {
                        Console.Write(player.Symbol);
                        continue;
                    }

                    var mob = Mob.FirstOrDefault(m => m.X == r && m.Y == c && m.Health > 0);
                    if (mob != null)
                    {
                        Console.Write(mob.Symbol);
                        continue;
                    }

                    var drop = DropList.FirstOrDefault(d => d.X == r && d.Y == c);
                    if (drop != null)
                    {
                        drop.Item.PrintItem();
                        continue;
                    }
                    try
                    {
                        Console.Write(Layout[r, c]);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.Write(" ");
                    }
                }
                if (player.isOnItem)
                {
                    Console.Write("     ");
                    dropHere.Item.ItemInfo(r, dropHere);
                    if ((dropHere.Item is Weapon weapon &&
                        player.Item !=null) ||
                        (dropHere.Item is Armor armor &&
                        (player.Helmet.Name == armor.Name ||
                        player.Chestplate.Name == armor.Name)))
                    {
                        player.EquippedItemInfo(r, dropHere);
                    }
                }
                Console.WriteLine();
            }
        }
        
        
    }
}