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
        public Ally(Vector2 viewWindow) : base(viewWindow)
        {
            // Changed
            //this.ViewWindow = SpawnPosition;
            //SpawnPosition = viewWindow;
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            //Position.Y += (float)0.5;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rotation = (float) Math.PI;
            base.Draw(spriteBatch);
        }
    }
}
