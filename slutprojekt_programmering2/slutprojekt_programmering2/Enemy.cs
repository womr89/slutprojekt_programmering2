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
            Position.Y += 7;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rotation = 0;
            base.Draw(spriteBatch);
        }
    }
}
