using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace slutprojekt_programmering2 {
    class Collision {
        // TODO Behöver nog inte denna collision
        /// <summary>
        /// Checks if all cars intercets with each other
        /// Then removes both cars if intersected
        /// </summary>
        /// <param name="carList"> reference of all cars</param>
        public void CheckEnemiesCollision( ref List<Car> allCars ) {
            for ( int i = 0; i < allCars.Count; i++ ) {
                for ( int j = 0; j < allCars.Count; j++ ) {
                    if ( allCars[i].CarRectangle.Intersects( allCars[j].CarRectangle ) && ( i != j ) ) {
                        Car car1 = allCars[i];
                        Car car2 = allCars[j];

                        car1.Die();
                        car2.Die();
                        allCars.Remove( car1 );
                        allCars.Remove( car2 );
                    }
                }
            }
        }

        /// <summary>
        /// Checks if player intersects with all other cars
        /// </summary>
        /// <param name="carList">List of all cars</param>
        /// <param name="player">Player car</param>
        public int CheckPlayerCollision( ref List<Car> carList, Player player ) {
            int collisions = 0;

            for ( int i = 0; i < carList.Count; i++ ) {
                if ( player.CarRectangle.Intersects( carList[i].CarRectangle ) ) {
                    carList.RemoveAt( i );
                    collisions++;
                }
            }

            return collisions;
        }

        public bool IsInLeftLane( Player player ) {
            return player.CarPosition.X < 510;
        }

        /// <summary>
        /// Checks if Player collides with top wall.
        /// </summary>
        /// <param name="player">Player car</param>
        private bool CollidesWithTopWall( Player player ) {
            return player.CarPosition.Y >= 100;
        }

        /// <summary>
        /// Checks if Player collides with bottom wall.
        /// </summary>
        /// <param name="player">Player car</param>
        private bool CollidesWithBottomWall( Player player ) {
            return player.CarPosition.Y <= 890;
        }

        /// <summary>
        /// Checks if Player collides with left wall.
        /// </summary>
        /// <param name="player">Player car</param>
        private bool CollidesWithLeftWall( Player player ) {
            return player.CarPosition.X >= 70;
        }

        /// <summary>
        /// Checks if Player collides with right wall.
        /// </summary>
        /// <param name="player">Player car</param>
        private bool CollidesWithRightWall( Player player ) {
            return player.CarPosition.X <= 935;
        }

        /// <summary>
        /// Checks if FastEnemies and SlowEnemies moves outside the game screen, 
        /// if the car has moved outside the sidewall or below the gamescreen the car will be removed from the list.
        /// This metod also checks if the player is moving in to the walls
        /// </summary>
        /// <param name="carList">Cars FastEnemies and SlowEnemies</param>
        /// <param name="player">Player car</param>
        public void CheckWallCollision( ref List<Car> carList, Player player ) {
            for ( int i = 0; i < carList.Count; i++ ) {
                // FastEnemies and SlowEnemies
                if ( carList[i].CarPosition.X > 1000 ) {
                    carList.RemoveAt( i );
                }
                else if ( carList[i].CarPosition.X < 0 ) {
                    carList.RemoveAt( i );
                }
                else if ( carList[i].CarPosition.Y > 1100 ) {
                    carList.RemoveAt( i );
                }
            }
            // TODO Fråga Henrik
            player.MoveTop = CollidesWithTopWall( player );
            player.MoveBottom = CollidesWithBottomWall( player );
            player.MoveLeft = CollidesWithLeftWall( player );
            player.MoveRight = CollidesWithRightWall( player );
        }
    }
}
