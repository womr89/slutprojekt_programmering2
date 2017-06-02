using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace slutprojekt_programmering2 {
    class SlowEnemy : Car {
        Random _randomCrash = new Random();
        private int _random;
        private float _rotation2;
        private bool _startRotate;

        public SlowEnemy(Vector2 startPosition) : base(startPosition) {
            _rotation2 = (float) Math.PI;
        }

        public override void Update(GameTime gameTime) {
            Position.Y += 5;
            // TODO used when debugging collision between SlowEnemy and FastEnemy
            //Position.X -= 4;
            
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            Rotation = _rotation2;

            base.Draw(spriteBatch);
        }
    }
}
