using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace slutprojekt_programmering2
{
    /// <summary>
    /// https://www.dotnetperls.com/enum
    /// lets me set a string to a number
    /// </summary>
    public enum CarColor
    {
        Blue = 0,
        Green = 1,
        Purple = 2,
        Red = 3,
        White = 4
    }

    class Car
    {
        /// <summary>
        /// Variables mainly used by classes that inherit
        /// </summary>
        protected Texture2D Texture;
        protected Vector2 Position;
        protected KeyboardState State;
        protected Vector2 ViewWindow;
        protected Vector2 SpawnPosition;
        protected float Rotation;

        // Random color
        protected readonly CarColor color;
        protected static Random random = new Random((int)DateTime.Now.Ticks);

        // Spawn Position-list
        List<Vector2> _avalibleSpawnPositions = new List<Vector2>();

        /// <summary>
        /// Car constructor
        /// </summary>
        /// <param name="viewWindow">Size of the game-screen, width and height.</param>
        protected Car(Vector2 viewWindow)
        {
            this.ViewWindow = viewWindow;
            Position = viewWindow;

            color = (CarColor)random.Next(0, 5);
        }

        /// <summary>
        /// Loads texture with random car-color. $"_car{color}" is instead of typing: "_carBlue, _carGreen, ... ect".
        /// color is fetched random from CarColor method.
        /// </summary>
        /// <param name="content">Used to load content</param>
        public virtual void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>($"_car{color}");
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }
        /// <summary>
        /// Draws the texture for all classes that inherit from Car.cs
        /// </summary>
        /// <param name="spriteBatch">Used to draw Texture</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
               Texture
               , new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height)
               , null
               , Color.White
               , Rotation                            // Rotation
               , SpawnPosition                       // Start Position
               , SpriteEffects.None
               , 0);
        }
    }
}
