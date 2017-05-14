using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace slutprojekt_programmering2
{
    /// <summary>
    /// 
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch _spriteBatch;
        private Player _player;
        private List<Enemy> _enemies = new List<Enemy>();
        private List<Ally> _allies = new List<Ally>();
        // List of avalible spawn positions
        private List<Vector2> _allySpawnPos = new List<Vector2>(new Vector2[]
        {
            new Vector2(695, 225), new Vector2(950, 225),
            new Vector2(695, 470), new Vector2(950, 470),
            new Vector2(695, 710), new Vector2(950, 710),
            new Vector2(695, 950), new Vector2(950, 950)
        });
        private Random _randomSpawn = new Random((int)DateTime.Now.Ticks);
        private int _randomSelect;

        
        private Background _background;

        
        
        // View Window
        private static Vector2 _viewWindow;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        /// <summary>
        /// Random Start spawn position method
        /// </summary>
        /// <returns>retruns avalible start position</returns>
        private int RandomStartSpawn()
        {
            
            int spawn = _randomSpawn.Next(_allySpawnPos.Count);

            return spawn;
        }
        /// <summary>
        /// Removes taken position from list
        /// </summary>
        private void RemoveSpawn()
        {
                _allySpawnPos.RemoveAt(_randomSelect);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            // CurrentDisplayMode width and height
            _viewWindow.Y = 1000;
            _viewWindow.X = 1000;

            // Resize the game screen, width and height
            graphics.PreferredBackBufferHeight = (int)_viewWindow.Y;
            graphics.PreferredBackBufferWidth = (int)_viewWindow.X;
            graphics.ApplyChanges();

            
            for (int i = 0; i < 8; i++)
            {
                // Gets a random number
                _randomSelect = RandomStartSpawn();
                // Adds a new Ally with a start position vector2 random number in list
                _allies.Add(new Ally(_allySpawnPos[_randomSelect]));
                // Removes the used position
                RemoveSpawn();
            }
            

            _background = new Background();
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load images
            _background.LoadContent(Content);
            //_player.LoadContent(Content);
            //_enemy.LoadContent(Content);

            foreach (var car in _allies)
            {
                car.LoadContent(Content);
            }

            base.LoadContent(); 
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //_player.Update(gameTime);
            //_enemy.Update(gameTime);
            //_ally.Update(gameTime);
            
            _background.Update();

            
            



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            // Background
            _background.Draw(_spriteBatch, _viewWindow);
            // Player
            //_player.Draw(_spriteBatch);
            // Enemy
            //_enemy.Draw(_spriteBatch);
            // Ally
            //_ally.Draw(_spriteBatch);
            //_ally.Draw(_spriteBatch);

            foreach (var ally in _allies)
            {
                ally.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
