using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace slutprojekt_programmering2 {
    class Score {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fastEnemies"></param>
        /// <param name="slowEnemies"></param>
        /// <param name="player"></param>
        public void AddPoints( List<FastEnemy> fastEnemies, List<SlowEnemy> slowEnemies, Player player ) {
            if ( player.CarPosition.X > 510 ) {
                foreach ( SlowEnemy slowEnemy in slowEnemies ) {
                    if ( (int) player.CarPosition.Y == (int) slowEnemy.CarPosition.Y ) {
                        //TODO Counts every frame, need to be fixed TABORT
                        player.Score += 1;
                    }
                }
            }
            else if ( player.CarPosition.X < 510 ) {
                foreach ( FastEnemy enemy in fastEnemies ) {
                    if ( player.CarPosition.Y == enemy.CarPosition.Y ) {
                        //TODO Counts every frame, need to be fixed TABORT
                        player.Score += 2;
                    }
                }
            }
        }
    }
}
