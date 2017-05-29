using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace slutprojekt_programmering2
{
    class Ally : Car
    {
        Random _randomCrash = new Random();
        private int _random;
        private float _rotation2;
        private bool _startRotate;
        public Ally(Vector2 startPosition) : base(startPosition)
        {
            _rotation2 = (float)Math.PI;
            
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            
            Position.Y += 1;
            // TODO used when debugging collision between Ally and Enemy
            //Position.X -= 4;


            //_random = _randomCrash.Next(0, 500);

            /*if (_random == 1)
            {
                _startRotate = true;
            }
            while (_startRotate)
            {
                _rotation2 -= (float)1;
                if (_rotation2 == (float)Math.PI - 1)
                {
                    _startRotate = false;
                }
            }
            */
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rotation = _rotation2;
            
            base.Draw(spriteBatch);
        }
    }
}
