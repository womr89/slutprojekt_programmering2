using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace slutprojekt_programmering2
{
    class Enemy : Car
    {  
        
        /// <summary>
        /// Setting value to SpawnPosition.Y
        /// </summary>
        /// <param name="viewWindow">the entire game-screen width and height</param>
        public Enemy(Vector2 viewWindow):base (viewWindow)
        {
            SpawnPosition.Y = ViewWindow.Y;
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
        }

        /// <summary>
        /// Update metod, used to set SpawnPosition.X and constant speed to Position.Y
        /// </summary>
        /// <param name="gameTime">Currently not used, inherited from Game1.cs Update</param>
        public override void Update(GameTime gameTime)
        {
            // Spawn position X is viewWindow (1000) - Texture.Width / 2 (125 / 2)
            SpawnPosition.X = ViewWindow.X - Texture.Width/2;

            Position.Y += 5;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rotation = 0;
            base.Draw(spriteBatch);
        }
    }
}
