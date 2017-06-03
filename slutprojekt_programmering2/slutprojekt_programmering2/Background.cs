using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace slutprojekt_programmering2 {
    class Background {
        private Texture2D _background;

        private Vector2 _position = new Vector2( 0, 0 );
        private Vector2 _position2 = new Vector2( 0, -1080 ); // Set the Y value to -(picture height)

        /// <summary>
        /// Loads background image
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent( ContentManager content ) {
            _background = content.Load<Texture2D>( "background" );
        }
        /// <summary>
        /// Background with 7 as contant speed
        /// </summary>
        public void Update() {
            _position.Y += 7;
            _position2.Y += 7;
        }

        /// <summary>
        /// Draws two background images
        /// if the position is outside view window the position will be changed
        /// This creats a loop where the road (image) looks to be moving backwards from the car.
        /// </summary>
        /// <param name="spriteBatch">Enable a group of sprites to be drawn</param>
        /// <param name="viewWindow">Game window seen by the player</param>
        public void Draw( SpriteBatch spriteBatch, Vector2 viewWindow ) {
            spriteBatch.Draw( _background, new Rectangle( 0, (int) _position.Y, 1000, 1080 ), Color.White );
            spriteBatch.Draw( _background, new Rectangle( 0, (int) _position2.Y, 1000, 1080 ), Color.White );

            if ( _position.Y >= viewWindow.Y ) {
                _position.Y = _position2.Y - _background.Height;
            }
            else if ( _position2.Y >= viewWindow.Y ) {
                _position2.Y = _position.Y - _background.Height;
            }
        }
    }
}