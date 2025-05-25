using System.Text;
using System.Threading;
namespace Blastangel
{
    class Program
    {
        public static Map map;
        public static Player player;
        public static SemaphoreSlim s = new SemaphoreSlim(1, 1);
        static bool isEnd;
        static void Main()
        {
            string c;
            Console.OutputEncoding = Encoding.UTF8;
            do
            {
                isEnd = false;
                Console.Clear();
                map = new Map();
                player = new Player(7, 14, map);
                Start(map);
                Thread time = new Thread(Tempo);
                time.Start();
                while (true)
                {
                    if (player.isAttacking)
                    {
                        si();
                        Thread.Sleep(50);
                        Console.Clear();
                        player.isAttacking = false;
                    }
                    si();
                    if (!isPlayerAlive())
                    {
                        Console.WriteLine("Sei morto");
                        isEnd = true;
                        break;
                    }
                    else if (map.rooms[map.BossRoomY, map.BossRoomX].Mob.Any(m => m.Health <= 0))
                    {
                        Console.WriteLine("Hai vinto");
                        isEnd = true;
                        break;
                    }
                    MovePlayer(player.RoomX, player.RoomY);
                    Console.Clear();
                }
                time.Join();

                    Console.WriteLine("Vuoi continuare? (Y/N)");
                do
                {
                    c = Console.ReadLine();
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                } while (c != "Y" && c != "y" && c != "N" && c != "n");
            } while (c == "Y" || c == "y");
            Console.WriteLine("Grazie per aver giocato!");
            Console.WriteLine("\n(premi un tasto qualsiasi...)");
        }
        static void si()
        {
            Console.WriteLine("Dungeon ✠ - GiuliLobs beta v1.6");
            s.Wait();

            //CHEAT
            //map.DisplayMap();

            map.rooms[player.RoomY, player.RoomX].DisplayRoom(player);
            player.DisplayPlayerStats();
            s.Release();
        }
        static void Tempo()
        {
            DateTime tInizio = DateTime.Now;
            Thread.Sleep(1000);
            while (!isEnd)
            {
                s.Wait();
                Console.WriteLine("Tempo di gioco: " + (DateTime.Now - tInizio).ToString(@"mm\:ss"));

                if (!(Console.CursorTop == 0))
                {
                    try
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop + 1);
                    }
                }
                s.Release();
                Thread.Sleep(1000);
            }
            DateTime tFine = DateTime.Now;

            Console.WriteLine("Tempo di gioco: " + (tFine - tInizio).ToString(@"mm\:ss"));
        }

        static void Start(Map map)
        {
            map.GenerateMap(map);
            player.RoomY = map.SpawnRoomY;
            player.RoomX = map.SpawnRoomX;
        }
        static bool isPlayerAlive()
        {
            if (player.Health <= 0)
            {
                player.Health = 0;
                return false;
            }
            return true;
        }
        static void MovePlayer(int roomX, int roomY)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            player.isTaking = false;
            int newX = player.Y;
            int newY = player.X;
            Room room = map.rooms[roomY, roomX];
            int layoutX = room.Layout.GetLength(0);
            int layoutY = room.Layout.GetLength(1);
            bool goingBottomRoom = false;
            bool goingRightRoom = false;
            bool goingTopRoom = false;
            bool goingLeftRoom = false;
            player.isAttacking = false;
            { //WASD + ENTER + E - ARROWS + Z + X
                if (key.Key == ConsoleKey.W || key.Key == ConsoleKey.UpArrow) newY--;
                if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow) newY++;
                if (key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow) newX--;
                if (key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow) newX++;
                if (key.Key == ConsoleKey.E || key.Key == ConsoleKey.X) player.isTaking = true;
                if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Z)
                {
                    player.isAttacking = true;
                    var list = room.Mob;
                    foreach (var mob in list)
                    {
                        if (mob.Health <= 0) continue;
                        if (player.X - 1 == mob.X && player.Y == mob.Y ||
                            player.X + 1 == mob.X && player.Y == mob.Y ||
                            player.X == mob.X && player.Y - 1 == mob.Y ||
                            player.X == mob.X && player.Y + 1 == mob.Y)
                        {
                            player.Attack(mob);
                            break;
                        }
                    }
                }
            }
            bool isMob = room.Mob.Any(m => m.X == newY && m.Y == newX && m.Health > 0);
            if (IsInsideBounds(newX, newY, layoutX, layoutY) && room.Layout[newY, newX] == ' ' && !isMob)
            {
                goingBottomRoom = (newY == layoutX - 1);
                goingRightRoom = (newX == layoutY - 1);
                goingTopRoom = (newY == 0);
                goingLeftRoom = (newX == 0);
                if (goingBottomRoom)
                {
                    player.RoomY++;
                    player.X = 1;
                }
                else if (goingLeftRoom)
                {
                    player.RoomX--;
                    player.Y = layoutY - 2;
                }
                else if (goingTopRoom)
                {
                    player.RoomY--;
                    player.X = layoutX - 2;
                }
                else if (goingRightRoom)
                {
                    player.RoomX++;
                    player.Y = 1;
                }
                else
                {
                    player.Y = newX;
                    player.X = newY;
                }
            }
        }
        public static bool IsInsideBounds(int x, int y, int lX, int lY)
        {
            return ((x > -1 && x < lY) && (y > -1 && y < lX));
        }
    }
}