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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace slutprojekt_programmering2 {
    /// <summary>
    /// https://www.dotnetperls.com/enum
    /// lets me set a string to a number
    /// </summary>
    public enum CarColor {
        Blue,
        Green,
        Purple,
        Red,
        White
    }

    class Car {
        /// <summary>
        /// Variables mainly used by classes that inherit
        /// </summary>
        protected Texture2D Texture;

        public Vector2 Position { get; set; }
        public float Rotation;

        public Rectangle CarRectangle { get; private set; }
        public Vector2 CarPosition { get; set; }

        // Debug
        protected Texture2D DebugTexture;

        // Random Color
        public CarColor Color { get; private set; }
        private static Random random = new Random( Guid.NewGuid().GetHashCode() );

        /// <summary>
        /// Car constructor
        /// </summary>
        /// <param name="startPosition">Gets the vector2 start position cordinates.</param>
        protected Car( Vector2 startPosition ) {
            Position = startPosition;

            Color = (CarColor) random.Next( 0, 5 );
        }

        protected Car() {}

        public virtual void Initialize() {
        }

        /// <summary>
        /// Loads texture with random car-Color. $"_car{Color}" is instead of typing: "_carBlue, _carGreen, ... ect".
        /// Color is fetched random from CarColor method.
        /// </summary>
        /// <param name="content">Used to load content</param>
        public virtual void LoadContent( ContentManager content ) {
            Texture = content.Load<Texture2D>( $"_car{Color}" );
            CarRectangle = new Rectangle( (int) Position.X, (int) Position.Y, Texture.Width, Texture.Height );
            CarPosition = new Vector2( (int) Position.X, (int) Position.Y );
        }

        // Used when debugging
        public void LoadDebugTexture( Texture2D debugTexture ) {
            DebugTexture = debugTexture;
        }

        public virtual void Update( GameTime gameTime ) {
            CarRectangle = new Rectangle( (int) Position.X - Texture.Width / 2, (int) Position.Y - Texture.Height / 2,
                Texture.Width, Texture.Height );
            CarPosition = new Vector2( (int) Position.X, (int) Position.Y );
        }

        /// <summary>
        /// Draws the texture for all classes that inherit from Car.cs
        /// </summary>
        /// <param name="spriteBatch">Used to draw Texture</param>
        public virtual void Draw( SpriteBatch spriteBatch ) {
            spriteBatch.Draw(
                Texture,
                Position,
                null,
                Microsoft.Xna.Framework.Color.White,
                Rotation,
                new Vector2( (float) Texture.Width / 2, (float) Texture.Height / 2 ),
                new Vector2( 1, 1 ),
                SpriteEffects.None,
                0 );

            // Draw debugg Rectangle
            //spriteBatch.Draw(DebugTexture , CarRectangle, Microsoft.Xna.Framework.Color.White);
        }
    }
}
