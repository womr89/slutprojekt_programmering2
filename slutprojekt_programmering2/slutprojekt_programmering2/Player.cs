using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace slutprojekt_programmering2
{
    class Player : Car
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewWindow">the entire game-screen width and height</param>
        public Player(Vector2 viewWindow): base(viewWindow)
        {
            
        }

        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("_carWhite");
        }

        /// <summary>
        /// Also sets the spawnposition
        /// Updating the Position when arrow-Keys is pressed.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            // Start Position               // Texture / 4 to get the car in center.
            //ViewWindow.Y = ViewWindow.Y / 2 + Texture.Height / 4;
            //ViewWindow.X = ViewWindow.X / 2 + Texture.Width / 4;
            //SpawnPosition = ViewWindow;

            State = Keyboard.GetState();
            // Keyboard input
            // Left key, car go left
            if (State.IsKeyDown(Keys.Left))
            {
                Position.X -= 8;
            }
            // Right key, car go right
            else if (State.IsKeyDown(Keys.Right))
            {
                Position.X += 8;
            }
            // Up key, car go forward
            if (State.IsKeyDown(Keys.Up))
            {
                Position.Y -= 8;
            }
            // Down key, car slowing down (going backwards)
            else if (State.IsKeyDown(Keys.Down))
            {
                Position.Y += 8;
            }
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rotation = (float) Math.PI;
            base.Draw(spriteBatch);
        }
    }
}
