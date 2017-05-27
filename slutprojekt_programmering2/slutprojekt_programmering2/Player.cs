using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public double Score { get; set; }
        public bool Move;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startPosition">the entire game-screen width and height</param>
        public Player(Vector2 startPosition): base(startPosition)
        {
            
        }

        public override void Initialize()
        {
            // Player start score
            Score = 0;
        }
        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("_carBlue");
        }

        /// <summary>
        /// Also sets the spawnposition
        /// Updating the Position when arrow-Keys is pressed.
        /// The bool "Move" is assigned in Collision, If false the car cant go up
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            // Start Position               // Texture / 4 to get the car in center.
            //startPosition.Y = startPosition.Y / 2 + Texture.Height / 4;
            //startPosition.X = startPosition.X / 2 + Texture.Width / 4;
            //SpawnPosition = startPosition;

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
            if (State.IsKeyDown(Keys.Up) && Move)
            {
                Position.Y -= 8;   
            }
            // Down key, car slowing down (going backwards)
            else if (State.IsKeyDown(Keys.Down))
            {
                Position.Y += 8;
            }
            // TODO fix Score
            Debug.WriteLine(Score);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rotation = (float) Math.PI;
            base.Draw(spriteBatch);
        }
    }
}
