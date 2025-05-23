using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blastangel
{
    internal class Armor : Item
    {
        Random rnd = new Random();
        public int Defense { get; set; }
        public string Material { get; set; }
        public Armor(string name, string sym,string material, Player player) 
            : base(name, sym)
        {
            Use(player);
            switch (rnd.Next(0,6))
            {
                case 0:
                    Symbol = "∩";
                    Defense = 10;
                    Name = "Elmo";
                    Material = "Pelle";
                    break;
                case 1:
                    Symbol = "∩";
                    Defense = 20;
                    Name = "Elmo";
                    Material = "Maglia";
                    break;
                case 2:
                    Symbol = "∩";
                    Defense = 30;
                    Name = "Elmo";
                    Material = "Ferro";
                    break;
                case 3:
                    Symbol = "≡";
                    Defense = 20;
                    Name = "Corazza";
                    Material = "Pelle";
                    break;
                case 4:
                    Symbol = "≡";
                    Defense = 30;
                    Name = "Corazza";
                    Material = "Maglia";
                    break;
                case 5:
                    Symbol = "≡";
                    Defense = 40;
                    Name = "Corazza";
                    Material = "Ferro";
                    break;

            }
        }
        public void Use(Player player)
        {
            player.Defense += Defense;
        }
    }
}
