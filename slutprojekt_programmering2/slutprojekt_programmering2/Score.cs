﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace slutprojekt_programmering2
{
    class Score
    {
        public void AddPoints(List<Enemy> enemies, List<Ally> allies, Player player)
        {
            if (player.CarPosition.X < 510) {
            foreach (Enemy enemy in enemies)
            {
                if (player.CarPosition.Y > enemy.CarPosition.Y)
                {   
                    //TODO Counts every frame, need to be fixed
                    player.Score += 2;
                }
                }
            }


            else if (player.CarPosition.X > 510) { 
            foreach (Ally ally in allies)
            {
                if (player.CarPosition.Y > ally.CarPosition.Y)
                {
                    //TODO Counts every frame, need to be fixed
                    player.Score += 1;
                }
                }
            }

        }
    }
}
