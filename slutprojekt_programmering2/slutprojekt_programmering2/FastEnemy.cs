﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace slutprojekt_programmering2
{
    class FastEnemy : Car
    {  
        
        /// <summary>
        /// Setting value to SpawnPosition.Y
        /// </summary>
        /// <param name="startPosition">the entire game-screen width and height</param>
        public FastEnemy(Vector2 startPosition):base (startPosition) {
            
        }

        /// <summary>
        /// Update metod, used to set SpawnPosition.X and constant speed to Position.Y
        /// </summary>
        /// <param name="gameTime">Currently not used, inherited from Game.cs Update</param>
        public override void Update(GameTime gameTime) {
            Position = new Vector2( Position.X, Position.Y + 12 );
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rotation = 0;
            base.Draw(spriteBatch);
        }
    }
}