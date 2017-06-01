using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace slutprojekt_programmering2 {
    class Player : Car {
        public int Score;
        public bool MoveTop;
        public bool MoveBottom;
        public bool MoveLeft;
        public bool MoveRight;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startPosition">the entire game-screen width and height</param>
        public Player( Vector2 startPosition ) : base( startPosition ) {
        }

        public override void LoadContent( ContentManager content ) {
            Texture = content.Load<Texture2D>( "_carBlue" );
        }

        public int GetPoints() {
            return Score;
        }

        public void AddPoints( int points ) {
            Score += points;
        }

        /// <summary>
        /// Also sets the spawnposition
        /// Updating the Position when arrow-Keys is pressed.
        /// The bool "MoveTop" is assigned in Collision, If false the car cant go up
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update( GameTime gameTime ) {
            State = Keyboard.GetState();
            // Keyboard input
            // Left key, car go left
            if ( State.IsKeyDown( Keys.Left ) && MoveLeft ) {
                Position.X -= 8;
            }
            // Right key, car go right
            else if ( State.IsKeyDown( Keys.Right ) && MoveRight ) {
                Position.X += 8;
            }

            // Up key, car go forward
            if ( State.IsKeyDown( Keys.Up ) && MoveTop ) {
                Position.Y -= 8;
            }
            // Down key, car slowing down (going backwards)
            else if ( State.IsKeyDown( Keys.Down ) && MoveBottom ) {
                Position.Y += 8;
            }

           
            base.Update( gameTime );
        }

        public override void Draw( SpriteBatch spriteBatch ) {
            Rotation = (float) Math.PI;
            base.Draw( spriteBatch );
        }
    }
}
