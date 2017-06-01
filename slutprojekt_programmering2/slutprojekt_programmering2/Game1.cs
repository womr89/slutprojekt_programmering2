using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
        private KeyboardState State;
        GraphicsDeviceManager graphics;
        SpriteBatch _spriteBatch;
        private Player _player;
        private List<Enemy> _enemies = new List<Enemy>();
        private float _enemySpawnTimer;
        private int _randomNumber;
        private List<Ally> _allies = new List<Ally>();
        private float _allySpawnTimer;
        private List<Car> _allCars = new List<Car>();
        private Collision _collision;
        private Score _score;

        // Debug
        private Texture2D _debugTexture;

        private readonly List<Vector2> _enemySpawnPos = new List<Vector2>(new Vector2[]
        {
            new Vector2(120, -225),
            new Vector2(380, -225)
        }); 
        private readonly List<Vector2> _allySpawnPos = new List<Vector2>(new Vector2[]
        {
            new Vector2(635, -225),
            new Vector2(890, -225)
        }); 
        private readonly Random _randomSpawn = new Random(/*(int)DateTime.Now.Ticks*/);
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
            
            // New player
            _player = new Player(new Vector2(400, 450));
            

            // Add new enemy in list
            _randomNumber = _randomSpawn.Next(_enemySpawnPos.Count);
            _enemies.Add(new Enemy(_enemySpawnPos[_randomNumber]));

            // Add new allie in list
            _randomNumber = _randomSpawn.Next(_allySpawnPos.Count);
            _allies.Add(new Ally(_allySpawnPos[_randomNumber]));

            // Adds Ally to Allcar list
            foreach (Ally ally in _allies)
            {
                _allCars.Add(ally);
            }
            // Adds enemy to Allcar list
            foreach (Enemy enemy in _enemies)
            {
                _allCars.Add(enemy);
            }

            // Adds player to Allcar
            //_allCars.Add(_player);

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

            // Debug
            _debugTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            _debugTexture.SetData<Color>(new Color[] { Color.White });


            // Load images:
            // Background
            _background.LoadContent(Content);
            // Player
            _player.LoadContent(Content);
            _player.LoadDebugTexture(_debugTexture);    // Debug load texture for player
            // Allies
            foreach (var car in _allies)
            {
                car.LoadContent(Content);
                car.LoadDebugTexture(_debugTexture);    // Debug
            }
            // Enemies
            foreach (var car in _enemies)
            {
                car.LoadContent(Content);
                car.LoadDebugTexture(_debugTexture);    // Debug
            }
            State = new KeyboardState();
            
            _collision = new Collision();
            _score = new Score();

            
            
            base.LoadContent(); 
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

        
        /// <summary>
        /// Allows the game to run logic such as updating,
        /// checking for collisions, gathering input.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit // TODO Exit fungerar inte.
            if (State.IsKeyDown(Keys.Escape))
                this.Exit();

            // add new enemy on random position every x milliseconds
            _randomNumber = _randomSpawn.Next(_enemySpawnPos.Count);
            _enemySpawnTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_enemySpawnTimer > 700)
            {
                Enemy temp = new Enemy(_enemySpawnPos[_randomNumber]);

                 // Added to _enemies to use in Score.cs
                _enemies.Add(temp);
                _allCars.Add(temp);
                _allCars.Last().LoadContent(Content);
                _allCars.Last().LoadDebugTexture(_debugTexture);    // Debug
                _enemySpawnTimer -= 700;
            }
            // Add new allie on random position every x milliseconds
            _allySpawnTimer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
            _randomNumber = _randomSpawn.Next(_allySpawnPos.Count);
            if (_allySpawnTimer > 1700)
            {
                Ally temp = new Ally(_allySpawnPos[_randomNumber]);
                
                // Added to _allies to use in Score.cs
                _allies.Add(temp);
                _allCars.Add(temp);
                _allCars.Last().LoadContent(Content);
                _allCars.Last().LoadDebugTexture(_debugTexture);       // Debug
                _allySpawnTimer -= 1700;

                
            }

            foreach (Car car in _allCars)
            {
               car.Update(gameTime); 
            }
            // Update Player
            _player.Update(gameTime);
            // Update background
            _background.Update();
            
            _collision.CheckPlayerCollision(ref _allCars, _player);
            _collision.CheckCollisionWalls(ref _allCars, _player);
            _collision.CheckAllyEnemyCollisions(ref _allCars);
            _score.AddPoints(_enemies, _allies, _player);
            
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
            _player.Draw(_spriteBatch);
            
            // Draw Enemy and Ally
            foreach (Car car in _allCars)
            {
                car.Draw(_spriteBatch);
            }
            
             _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
