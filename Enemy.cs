using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElfeledettVarosokWPF
{
    public class Enemy
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int Damage { get; set; }

        public Enemy(string name, int hp, int damage)
        {
            Name = name;
            HP = hp;
            Damage = damage;
        }

        public bool IsDefeated()
        {
            return HP <= 0;
        }
    }
}
