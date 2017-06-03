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
    class FastEnemy : Car
    {  
        /// <param name="startPosition">the entire game-screen width and height</param>
        public FastEnemy(Vector2 startPosition):base (startPosition) {
            
        }

        /// <summary>
        /// Position.Y += 12
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime) {
            Position = new Vector2( Position.X, Position.Y + 12 );
            base.Update(gameTime);
        }
        /// <summary>
        /// Sets rotation, then calls for base Draw in Car
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            Rotation = 0;
            base.Draw(spriteBatch);
        }
    }
}
