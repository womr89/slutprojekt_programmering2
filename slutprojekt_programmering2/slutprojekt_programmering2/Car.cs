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
        protected Vector2 StartPosition;
        protected Vector2 SpawnPosition;
        protected float Rotation;
        protected Collision Collision;
        public Rectangle CarRectangle { get; private set; }
        public Vector2 CarPosition { get; private set; }
        

        // Debug
        protected Texture2D DebugTexture;


        // Random Color
        protected readonly CarColor Color;
        protected static Random random = new Random((int)DateTime.Now.Ticks);

        /// <summary>
        /// Car constructor
        /// </summary>
        /// <param name="startPosition">Gets the vector2 start position cordinates.</param>
        protected Car(Vector2 startPosition)
        {
            StartPosition = startPosition;
            Position = startPosition;

            Color = (CarColor)random.Next(0, 5);
        }

        public virtual void Initialize()
        {
            

        }
        /// <summary>
        /// Loads texture with random car-Color. $"_car{Color}" is instead of typing: "_carBlue, _carGreen, ... ect".
        /// Color is fetched random from CarColor method.
        /// </summary>
        /// <param name="content">Used to load content</param>
        public virtual void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>($"_car{Color}");
            CarRectangle = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            CarPosition = new Vector2((int)Position.X, (int)Position.Y);
        }
        // for debugging
        public void LoadDebugTexture(Texture2D debugTexture)
        {
            DebugTexture = debugTexture;
        }

        public virtual void Update(GameTime gameTime)
        {
            CarRectangle = new Rectangle((int)Position.X - Texture.Width / 2, (int)Position.Y - Texture.Height / 2, Texture.Width, Texture.Height);
            CarPosition = new Vector2((int)Position.X, (int)Position.Y);
        }
        /// <summary>
        /// Draws the texture for all classes that inherit from Car.cs
        /// </summary>
        /// <param name="spriteBatch">Used to draw Texture</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
               Texture, 
               Position,
               null,
               Microsoft.Xna.Framework.Color.White,
               Rotation,
               new Vector2(Texture.Width / 2, Texture.Height / 2),
               new Vector2(1,1),
               SpriteEffects.None,
               0);

            // Draw debugg Rectangle
            //spriteBatch.Draw(DebugTexture , CarRectangle, Microsoft.Xna.Framework.Color.White);
        }
    }
}
