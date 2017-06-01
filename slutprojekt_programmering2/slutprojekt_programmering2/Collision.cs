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
                    if (allCars[i].CarRectangle.Intersects(allCars[j].CarRectangle) && (i != j))
                    {
                        // To not remove a lower number before the geater number. 
                        // If so this might cause intersect to remove 1 wrong car
                        if (j > i)
                        {
                            allCars[i].Die();
                            allCars[j].Die();
                            allCars.RemoveAt(j);
                            allCars.RemoveAt(i);   
                        }
                        else
                        {
                            allCars[i].Die();
                            allCars[j].Die();
                            allCars.RemoveAt(i);
                            allCars.RemoveAt(j);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Checks if player intersects with all other cars
        /// </summary>
        /// <param name="carList">List of all cars</param>
        /// <param name="player">Player car</param>
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
        /// </summary>
        /// <param name="carList">Cars Enemies and allies</param>
        /// <param name="player">Player car</param>
        public void CheckCollisionWalls(ref List<Car> carList, Player player)
        {
            for (int i = 0; i < carList.Count; i++)
            {
                // Enemies and Allies
                if (carList[i].CarPosition.X > 1000)
                {
                    carList.RemoveAt(i);
                }
                else if (carList[i].CarPosition.X < 0)
                {
                    carList.RemoveAt(i);
                }
                else if (carList[i].CarPosition.Y > 1100)
                {
                    carList.RemoveAt(i);
                }
            }

            // Player collision walls
            // Top-wall
            if (player.CarPosition.Y < 100)
            {
                player.MoveTop = false;
            }
            else player.MoveTop = true;

            // Bottom-wall
            if (player.CarPosition.Y > 890)
            {
                player.MoveBottom = false;
            }
            else player.MoveBottom = true;

            // Leftside
            if (player.CarPosition.X < 70)
            {
                player.MoveLeft = false;
            }
            else player.MoveLeft = true;

            // Rightside 
            if (player.CarPosition.X > 935)
            {
                player.MoveRight = false;
            }
            else player.MoveRight = true;
            
        }
    }
}
