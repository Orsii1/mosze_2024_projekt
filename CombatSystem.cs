using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace ElfeledettVarosokWPF
{
    public class CombatSystem
    {
        public static string Fight(Player player, Enemy enemy)
        {
            Random rand = new Random();
            int playerHP = 100; // Játékos alap HP-ja
            string log = "";

            log += $"Egy harc kezdődött {enemy.Name} ellen!\n";

            while (playerHP > 0 && enemy.HP > 0)
            {
                int playerDamage = rand.Next(5, 15);
                enemy.HP -= playerDamage;
                log += $"Te sebzel {playerDamage}-t {enemy.Name}-re. ({enemy.HP} HP maradt)\n";

                if (enemy.IsDefeated())
                {
                    log += $"{enemy.Name} legyőzve!\n";
                    return log;
                }

                int enemyDamage = enemy.Damage;
                playerHP -= enemyDamage;
                log += $"{enemy.Name} sebzi rád {enemyDamage}-t. ({playerHP} HP maradt)\n";

                if (playerHP <= 0)
                {
                    log += "Te vesztettél a harcban.\n";
                    return log;
                }
            }

            return log;
        }
    }
}
