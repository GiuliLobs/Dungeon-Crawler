﻿namespace Blastangel
{
    internal enum MonsterState { Idle, Patrol, Chase, Attack }

    internal class Monster
    {
        private static readonly Random rng = new();
        char _symbol = 'o';
        public char Symbol { get { return _symbol; } set { _symbol = value; } }
        public int X { get; set; }
        public int Y { get; set; }
        bool Droppato { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public int AttackDelay { get; set; } = 1;

        public MonsterState State = MonsterState.Idle;
        private int patrolCooldown = 0;
        private (int dx, int dy) patrolDir;
        public int VisionRange = 6;
        public int AttackRange = 1;

        public Monster(int x, int y)
        {
            X = x;
            Y = y;
            Health = 100;
            Damage = 9;
            Droppato = false;
        }
        public Monster(int x, int y, int health) //boss
        {
            X = x;
            Y = y;
            Symbol = '§';
            Health = health;
            Damage = 25;
            Droppato = false;
        }
        public void Update(char[,] layout, Player player, List<Monster> others, Room room)
        {

            if (Health <= 0)
            {

                if (!Droppato)
                    room.DropList.Add(new Drop(X, Y, player));
                Droppato = true;
                return;
            }


            int dy = player.Y - Y;
            int dx = player.X - X;
            int manhattan = Math.Abs(dx) + Math.Abs(dy);

            switch (State)
            {
                case MonsterState.Idle:
                    if (manhattan <= VisionRange) State = MonsterState.Chase;
                    else if (rng.NextDouble() < 0.2) StartPatrol();
                    break;

                case MonsterState.Patrol:
                    if (manhattan <= VisionRange) State = MonsterState.Chase;
                    else if (--patrolCooldown <= 0) StartPatrol();
                    break;

                case MonsterState.Chase:
                    if (manhattan > VisionRange) State = MonsterState.Idle;
                    else if (manhattan <= AttackRange) State = MonsterState.Attack;
                    break;

                case MonsterState.Attack:
                    if (manhattan > AttackRange) State = MonsterState.Chase;
                    break;
            }

            switch (State)
            {
                case MonsterState.Patrol:
                    TryMove(patrolDir.dx, patrolDir.dy, layout, others);
                    break;

                case MonsterState.Chase:
                    int mdx = Math.Sign(dx);
                    int mdy = Math.Sign(dy);
                    if (Math.Abs(dx) > Math.Abs(dy))
                        TryMove(mdx, 0, layout, others);
                    else
                        TryMove(0, mdy, layout, others);
                    break;

                case MonsterState.Attack:
                    if (AttackDelay > 0)
                    {
                        AttackDelay--;
                        return;
                    }
                    double percDif = player.Defense / 100f;
                    player.Health -= Convert.ToInt32(Damage - (percDif * Damage));
                    AttackDelay = 1;
                    break;
            }
        }
        private void TryMove(int dx, int dy, char[,] layout, List<Monster> others)
        {
            int newX = X + dx;
            int newY = Y + dy;

            if (newX < 2 || newX > layout.GetLength(0) - 2 ||
                newY < 2 || newY > layout.GetLength(1) - 2
                )
                return;
            if (layout[newX, newY] == '█')
                return;
            foreach (var o in others)
                if (o != this && o.X == newX && o.Y == newY)
                    return;
            X = newX;
            Y = newY;
        }

        private void StartPatrol()
        {
            State = MonsterState.Patrol;
            patrolCooldown = rng.Next(6, 15);
            (int dx, int dy)[] dirs = { (-1, 0), (1, 0), (0, -1), (0, 1) };
            patrolDir = dirs[rng.Next(dirs.Length)];
        }
    }
}
