using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace slutprojekt_programmering2
{
    class SaveGameData {
        public Player player { get; set; }
        public List<FastEnemy> fastEnemies { get; set; }
        public List<SlowEnemy> slowEnemies { get; set; }
    }
}
