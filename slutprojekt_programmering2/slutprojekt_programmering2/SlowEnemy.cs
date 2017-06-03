using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace slutprojekt_programmering2 {
    class SlowEnemy : Car {
        public SlowEnemy(Vector2 startPosition) : base(startPosition) {
        }
        /// <summary>
        /// Position.Y += 5
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime) {
            Position = new Vector2( Position.X, Position.Y + 5 );
            base.Update(gameTime);
        }
        /// <summary>
        /// Sets rotation, then calls for base Draw in Car
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch) {
            Rotation = (float)Math.PI;
            base.Draw(spriteBatch);
        }
    }
}
