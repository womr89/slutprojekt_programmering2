using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using KeyboardState = Microsoft.Xna.Framework.Input.KeyboardState;
using XNAKeys = Microsoft.Xna.Framework.Input.Keys;

namespace slutprojekt_programmering2 {
    // Source: https://stackoverflow.com/questions/6854461/serializing-xna-rectangle-with-json-net#44238343
    public class XnaFriendlyResolver : DefaultContractResolver {
        protected override JsonContract CreateContract( Type objectType ) {
            if ( objectType == typeof (Rectangle) ) {
                return CreateObjectContract( objectType );
            }

            return base.CreateContract( objectType );
        }
    }

    public class Game : Microsoft.Xna.Framework.Game {
        KeyboardState _state;
        GraphicsDeviceManager graphics;
        SpriteBatch _spriteBatch;
        Player _player;
        double _givePointsTimer;
        List<FastEnemy> _fastEnemies = new List<FastEnemy>();
        double _fastEnemySpawnTimer = 700;
        int _randomNumber;
        List<SlowEnemy> _slowEnemies = new List<SlowEnemy>();
        double _slowEnemySpawnTimer = 1700;
        List<Car> _allCars = new List<Car>();
        Collision _collision;
        Score _score;
        bool _loadPreviousGame;
        JsonSerializerSettings _serializerSettings = new JsonSerializerSettings() {
            ContractResolver = new XnaFriendlyResolver(),
        };

        SpriteFont _font;
        Vector2 _fontPos;

        // Debug
        Texture2D _debugTexture;
        // TODO *******
        readonly List<Vector2> _fastEnemySpawnPos = new List<Vector2>( new Vector2[] {
            new Vector2( 90, -225 ),
            new Vector2( 120, -225 ),
            new Vector2( 150, -225 ),
            new Vector2( 350, -225 ),
            new Vector2( 380, -225 ),
            new Vector2( 410, -225 )
        } );

        readonly List<Vector2> _slowEnemySpawnPos = new List<Vector2>( new Vector2[] {
            new Vector2( 605, -225 ),
            new Vector2( 635, -225 ),
            new Vector2( 665, -225 ),
            new Vector2( 860, -225 ),
            new Vector2( 890, -225 ),
            new Vector2( 920, -225 )
        } );

        readonly Random _randomSpawn = new Random( Guid.NewGuid().GetHashCode() );
        int _randomSelect;

        Background _background;


        // View Window
        static Vector2 _viewWindow;


        public Game() {
            graphics = new GraphicsDeviceManager( this );
            Content.RootDirectory = "Content";
            // TODO **********
            if ( ! File.Exists( @"all_cars.json" ) || ! File.Exists( @"player.json" ) ) {
                return;
            }
            // TODO Question Box
            if (
                MessageBox.Show(
                    "Would you like to load your previous game (yes) or start a new one (no) ?",
                    "Previous Game Found!",
                    MessageBoxButtons.YesNo
                    ) == DialogResult.Yes
                ) {
                _loadPreviousGame = true; 
            }
            else if (
                MessageBox.Show(
                    "Would you like to remove the previous game files?",
                    "Previous Game Files",
                    MessageBoxButtons.YesNo
                    ) == DialogResult.Yes
                ) {
                File.Delete(@"all_cars.json");
                File.Delete(@"player.json");
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // CurrentDisplayMode width and height
            _viewWindow.Y = 1000;
            _viewWindow.X = 1000;

            // Resize the game screen, width and height
            graphics.PreferredBackBufferHeight = (int) _viewWindow.Y;
            graphics.PreferredBackBufferWidth = (int) _viewWindow.X;
            graphics.ApplyChanges();

            _background = new Background();

            if ( _loadPreviousGame ) {
                _player = JsonConvert.DeserializeObject<Player>(
                    File.ReadAllText(@"player.json"),
                    _serializerSettings
                );
                _allCars = JsonConvert.DeserializeObject<List<Car>>(
                    File.ReadAllText(@"all_cars.json"),
                    _serializerSettings
                );
            }
            else {
                _player = new Player(new Vector2(400, 450));
            }

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch( GraphicsDevice );

            // Debug
            _debugTexture = new Texture2D( GraphicsDevice, 1, 1, false, SurfaceFormat.Color );
            _debugTexture.SetData<Color>( new Color[] {Color.White} );


            // Load images:
            // Background
            _background.LoadContent( Content );
            // Player
            _player.LoadContent( Content );
            _player.LoadDebugTexture( _debugTexture ); // Debug load texture for player
            // SlowEnemies
            foreach ( var car in _slowEnemies ) {
                car.LoadContent( Content );
                car.LoadDebugTexture( _debugTexture ); // Debug
            }
            // FastEnemies
            foreach ( var car in _fastEnemies ) {
                car.LoadContent( Content );
                car.LoadDebugTexture( _debugTexture ); // Debug
            }
            _state = new KeyboardState();

            _collision = new Collision();
            _score = new Score();

            _font = Content.Load<SpriteFont>("SpriteFont1");
            _fontPos = new Vector2(
                graphics.GraphicsDevice.Viewport.Width * 0.1f,
                graphics.GraphicsDevice.Viewport.Height * 0.1f
            );
            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
        }


        /// <summary>
        /// Allows the game to run logic such as updating,
        /// checking for collisions, gathering input.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update( GameTime gameTime ) {
            // Allows the game to exit // TODO Exit fungerar inte.
            if ( _state.IsKeyDown( XNAKeys.F ) ) {
                Exit();
                return;
            }
                

            // add new FastEnemy on random position every x milliseconds
            _randomNumber = _randomSpawn.Next( _fastEnemySpawnPos.Count );
            _fastEnemySpawnTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if ( _fastEnemySpawnTimer > 700 ) {
                FastEnemy fastEnemy = new FastEnemy( _fastEnemySpawnPos[_randomNumber] );

                // Added to _fastEnemies to use in Score.cs
                _fastEnemies.Add( fastEnemy );
                _allCars.Add( fastEnemy );
                _allCars.Last().LoadContent( Content );
                _allCars.Last().LoadDebugTexture( _debugTexture ); // Debug
                _fastEnemySpawnTimer -= 700;
            }
            
            // Add new SlowEnemy on random position every x milliseconds
            _slowEnemySpawnTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            _randomNumber = _randomSpawn.Next( _slowEnemySpawnPos.Count );

            if ( _slowEnemySpawnTimer > 1700 ) {
                SlowEnemy slowEnemy = new SlowEnemy( _slowEnemySpawnPos[_randomNumber] );

                // Added to _slowEnemies to use in Score.cs
                _slowEnemies.Add( slowEnemy );
                _allCars.Add( slowEnemy );
                _allCars.Last().LoadContent( Content );
                _allCars.Last().LoadDebugTexture( _debugTexture ); // Debug
                _slowEnemySpawnTimer -= 1700;
            }

            foreach ( Car car in _allCars ) {
                car.Update( gameTime );
            }
            
            _player.Update( gameTime );
            _background.Update();
            // TODO Give points
            var collisions = _collision.CheckPlayerCollision( ref _allCars, _player );
            _player.AddPoints( collisions * ( _collision.IsInLeftLane(_player) ? -8 : -2 ) );

            _collision.CheckEnemiesCollision( ref _allCars );
            _collision.CheckWallCollision( ref _allCars, _player );

            _givePointsTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if ( _givePointsTimer >= ( _collision.IsInLeftLane( _player ) ? 250 : 1000 ) ) {
                _player.AddPoints( 1 );
                _givePointsTimer = 0;
            }

            _score.AddPoints( _fastEnemies, _slowEnemies, _player );
            base.Update( gameTime );
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw( GameTime gameTime ) {
            _spriteBatch.Begin();
            // Background
            _background.Draw( _spriteBatch, _viewWindow );
            // Player
            _player.Draw( _spriteBatch );

            // Draw SlowEnemy and FastEnemy
            foreach ( Car car in _allCars ) {
                car.Draw( _spriteBatch );
            }
            // TODO *******
            string points = $"Score: {_player.GetPoints()}";
            Vector2 fontOrigin = new Vector2( 0, 0 );
            _spriteBatch.DrawString(_font, points, _fontPos, Color.Blue, 0, fontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            _spriteBatch.End();

            base.Draw( gameTime );
        }
        // TODO ********
        protected override void OnExiting( Object sender, EventArgs args ) {
            File.WriteAllText(
                @"all_cars.json",
                JsonConvert.SerializeObject( _allCars, Formatting.Indented, _serializerSettings)
            );
            File.WriteAllText(
                @"player.json",
                JsonConvert.SerializeObject( _player, Formatting.Indented, _serializerSettings )
            );
        }
    }
}
