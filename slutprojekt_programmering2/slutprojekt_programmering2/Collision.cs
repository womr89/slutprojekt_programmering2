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
        
        public Collision()
        {

        }

        public void CheckCollisions(ref List<Car> carList)
        {
            for (int i = 0; i < carList.Count; i++)
            {
                for (int j = 0; j < carList.Count; j++)
                {
                    if (carList[j].CarRectangle.Intersects(carList[i].CarRectangle) && i != j && i != 0 && j != 0)
                    {

                        carList.RemoveAt(i);
                        carList.RemoveAt(j);
                        //remove each

                    }

                }
            }
        }
    }
}
