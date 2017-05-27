using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace slutprojekt_programmering2
{
    class Collision
    {
        /// <summary>
        /// Checks if all cars intercets with each other
        /// Then removes both cars if intersected
        /// </summary>
        /// <param name="carList"> reference of all cars</param>
        public void CheckAllyEnemyCollisions(ref List<Car> carList)
        {
            for (int i = 0; i < carList.Count; i++)
            {
                for (int j = 0; j < carList.Count; j++)
                {
                    // TODO fix Collision
                    if (carList[j].CarRectangle.Intersects(carList[i].CarRectangle) && i != j && i != 0 && j != 0)
                    {
                        carList.RemoveAt(i);
                        carList.RemoveAt(j);
                    }

                }
            }
        }

        public void CheckPlayerCollision(ref List<Car> carList, Player player)
        {
            for (int i = 0; i < carList.Count; i++)
            {
                if (player.CarRectangle.Intersects(carList[i].CarRectangle))
                {
                    carList.RemoveAt(i);
                }
            }
        }

        public void CheckCollisionWalls(ref List<Car> carList, Player player)
        {
            for (int i = 0; i < carList.Count; i++)
            {
                if (carList[i].CarPosition.X > 1000)
                {
                    carList.RemoveAt(i);
                }
                if (carList[i].CarPosition.X < 0)
                {
                    carList.RemoveAt(i);
                }
                if (carList[i].CarPosition.Y > 1100)
                {
                    carList.RemoveAt(i);
                }
                // if player moves over the screen
                if (player.CarPosition.Y < 100)
                {
                    player.Move = false;
                }
                else
                {
                    player.Move = true;
                }
            }
        }
    }
}
