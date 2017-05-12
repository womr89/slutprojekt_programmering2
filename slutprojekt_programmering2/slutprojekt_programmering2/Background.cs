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
    class Background
    {
        private Texture2D _background;

        // Position of background
        private Vector2 _position = new Vector2(0, 0);
        private Vector2 _position2 = new Vector2(0, -1080); // Set the Y value to -(picture height)

        public void LoadContent(ContentManager content)
        { 
            // Load images
            _background = content.Load<Texture2D>("background");
        }
        public void Update()
        {
            // Position of background constant speed
            _position.Y += 7;
            _position2.Y += 7;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 viewWindow)
        {
            spriteBatch.Draw(_background, new Rectangle(0, (int)_position.Y, 1000, 1080), Color.White);
            spriteBatch.Draw(_background, new Rectangle(0, (int)_position2.Y, 1000, 1080), Color.White);

            // If the position is outside view window.
            if (_position.Y >= viewWindow.Y)
            {
                // position of first background = position of second background - the background picture height
                _position.Y = _position2.Y - _background.Height;
            }
            else if (_position2.Y >= viewWindow.Y)
            {
                _position2.Y = _position.Y - _background.Height;
            }
        }

    }
}
