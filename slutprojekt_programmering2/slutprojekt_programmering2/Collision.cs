using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace slutprojekt_programmering2
{
    class Collision
    {

        /*public void CheckAllyEnemyCollisions(ref List<Car> carList)
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
        */
        /// <summary>
        /// Checks if all cars intercets with each other
        /// Then removes both cars if intersected
        /// </summary>
        /// <param name="carList"> reference of all cars</param>
        public void CheckAllyEnemyCollisions(ref List<Car> allCars)
        {
            for (int i = 0; i < allCars.Count; i++)
            {
                for (int j = 0; j < allCars.Count; j++)
                {
                    if (allCars[i].CarRectangle.Intersects(allCars[j].CarRectangle) && (i != j) && (i != 0) && (j != 0))
                    {
                        allCars.RemoveAt(j);
                        allCars.RemoveAt(i);
                    }
                }
            }
        }
        /// <summary>
        /// Checks if player intersects with all other cars
        /// </summary>
        /// <param name="carList"></param>
        /// <param name="player"></param>
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
        /// <summary>
        /// Checks if Enemies and Allies moves outside the game screen, 
        /// if the car has moved outside the sidewall or below the gamescreen the car will be removed from the list.
        /// 
        /// </summary>
        /// <param name="carList">Enemies and Allies</param>
        /// <param name="player"></param>
        public void CheckCollisionWalls(ref List<Car> carList, Player player)
        {
            for (int i = 0; i < carList.Count; i++)
            {
                // Enemies and Allies
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

                // if player is to high up
                if (player.CarPosition.Y < 100)
                {
                    player.MoveTop = false;
                }
                else player.MoveTop = true;
                // if player is to low
                if (player.CarPosition.Y > 890)
                {
                    player.MoveBottom = false;
                }
                else player.MoveBottom = true;
                // if player is to far to the left
                if (player.CarPosition.X < 70)
                {
                    player.MoveLeft = false;
                }
                else player.MoveLeft = true;
                // if player is to far to the right 
                if (player.CarPosition.X > 935)
                {
                    player.MoveRight = false;
                }
                else player.MoveRight = true;
            }
        }
    }
}
