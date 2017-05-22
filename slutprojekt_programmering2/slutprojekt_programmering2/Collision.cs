using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace slutprojekt_programmering2
{
    class Collision
    {
        
        public Collision(List<Car> carList)
        {
            for (int i = 0; i < carList.Count; i++)
            {
                for (int j = 0; j < carList.Count; j++)
                {
                    if (carList[i]._carRectangle.Intersects(carList[j]._carRectangle) && i != j)
                    {
                        
                    }
                    
                }
            }
        }
    }
}
