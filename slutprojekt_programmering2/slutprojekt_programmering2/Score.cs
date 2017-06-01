using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace slutprojekt_programmering2
{
    class Score
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enemies"></param>
        /// <param name="allies"></param>
        /// <param name="player"></param>
        public void AddPoints(List<Enemy> enemies, List<Ally> allies, Player player)
        {
            
            if (player.CarPosition.X > 510) { 
            foreach (Ally ally in allies)
            {
                if ((int)player.CarPosition.Y == (int)ally.CarPosition.Y)
                {
                    //TODO Counts every frame, need to be fixed
                    player.Score += 1;
                }
                }
            }
            else if (player.CarPosition.X < 510)
            {
                foreach (Enemy enemy in enemies)
                {
                    if (player.CarPosition.Y == enemy.CarPosition.Y)
                    {
                        //TODO Counts every frame, need to be fixed
                        player.Score += 2;
                    }
                }
            }

        }
    }
}
