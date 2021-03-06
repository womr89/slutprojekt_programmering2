using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        bool _loadPreviousGameFromFile;
        string _loadPreviousGameFromInternet;
        JsonSerializerSettings _serializerSettings = new JsonSerializerSettings() {
            ContractResolver = new XnaFriendlyResolver(),
        };

        SpriteFont _font;
        Vector2 _fontPos;
        Texture2D _debugTexture;
        /// <summary>
        /// Avalible SpawnPositions for fastEnemy
        /// </summary>
        readonly List<Vector2> _fastEnemySpawnPos = new List<Vector2>( new Vector2[] {
            new Vector2( 90, -225 ),
            new Vector2( 120, -225 ),
            new Vector2( 150, -225 ),
            new Vector2( 350, -225 ),
            new Vector2( 380, -225 ),
            new Vector2( 410, -225 )
        } );
        /// <summary>
        /// Avalible SpawnPositions for slowEnemy
        /// </summary>
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

        static Vector2 _viewWindow;

        /// <summary>
        /// Question window, if user want to load previous game, 
        /// Try load a game from internet
        /// </summary>
        public Game() {
            graphics = new GraphicsDeviceManager( this );
            Content.RootDirectory = "Content";
            
            if ( File.Exists(@"saved_game_data.json") && MessageBox.Show(
                    "A previous game has been found. Would you like to load it?",
                    "Previous Game Found!",
                    MessageBoxButtons.YesNo
                ) == DialogResult.Yes
            ) {
                _loadPreviousGameFromFile = true; 
            }

            var loadFromInternet = MessageBox.Show(
                "Do you like to try to load a game from the internet?",
                "Load From Internet?",
                MessageBoxButtons.YesNo
            );

            if ( loadFromInternet == DialogResult.Yes ) {
                using ( WebClient client = new WebClient() ) {
                    _loadPreviousGameFromInternet =
                        client.DownloadString( "http://localhost:8080/load_game" );
                }
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// Load previous game or load previous game from internet
        /// player is set to values from last saved game
        /// allCars is set to fastEnemies and slowEnemies values from last saved game
        /// Adds fastEnemies and slowEnemies to gameData 
        /// If there is no saved game, player will spawn in right lane.
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

            if ( _loadPreviousGameFromFile || ! String.IsNullOrEmpty( _loadPreviousGameFromInternet ) ) {
                var gameData = JsonConvert.DeserializeObject<SaveGameData>(
                    _loadPreviousGameFromFile ?
                        File.ReadAllText( @"saved_game_data.json" )
                        :
                        _loadPreviousGameFromInternet, _serializerSettings
                );

                _player = gameData.player;
                _allCars = new List<Car>()
                    .Concat( gameData.fastEnemies )
                    .Concat( gameData.slowEnemies ).ToList();
            }
            else {
                _player = new Player(new Vector2(890, 750));
            }

            base.Initialize();
        }

        /// <summary>
        /// Create a new SpriteBatch, which can be used to draw textures.
        /// Loads images of background, player and _allCars
        /// Creats an instance of state and collision class.
        /// Loads font and fontPos for drawing Score
        /// </summary>
        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch( GraphicsDevice );

            // Debug
            _debugTexture = new Texture2D( GraphicsDevice, 1, 1, false, SurfaceFormat.Color );
            _debugTexture.SetData<Color>( new Color[] {Color.White} );

            _background.LoadContent( Content );
            _player.LoadContent( Content );
            _player.LoadDebugTexture( _debugTexture ); // Debug load texture for player
            
            foreach ( var car in _allCars ) {
                car.LoadContent( Content );
            }

            _state = new KeyboardState();
            _collision = new Collision();

            _font = Content.Load<SpriteFont>("SpriteFont1");
            _fontPos = new Vector2(
                graphics.GraphicsDevice.Viewport.Width * 0.1f,
                graphics.GraphicsDevice.Viewport.Height * 0.1f
            );

            base.LoadContent();
        }

        /// <summary>
        /// Adds new fastEnemy and slowEnemy in an certain interval
        /// Updates _allCars, _player and background
        /// Checks collisions between player and enemies
        /// Checks collision between _allCars
        /// Gives points to player, if player collides with Enemy there will be minus points
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update( GameTime gameTime ) {

            _randomNumber = _randomSpawn.Next( _fastEnemySpawnPos.Count );
            _fastEnemySpawnTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if ( _fastEnemySpawnTimer > 700 ) {
                FastEnemy fastEnemy = new FastEnemy( _fastEnemySpawnPos[_randomNumber] );

                _allCars.Add( fastEnemy );
                _allCars.Last().LoadContent( Content );
                _allCars.Last().LoadDebugTexture( _debugTexture ); // Debug
                _fastEnemySpawnTimer = 0;
            }
            
            _slowEnemySpawnTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            _randomNumber = _randomSpawn.Next( _slowEnemySpawnPos.Count );

            if ( _slowEnemySpawnTimer > 1700 ) {
                SlowEnemy slowEnemy = new SlowEnemy( _slowEnemySpawnPos[_randomNumber] );

                _allCars.Add( slowEnemy );
                _allCars.Last().LoadContent( Content );
                _allCars.Last().LoadDebugTexture( _debugTexture ); // Debug
                _slowEnemySpawnTimer = 0;
            }

            foreach ( Car car in _allCars ) {
                car.Update( gameTime );
            }
            
            _player.Update( gameTime );
            _background.Update();
            
            var collisions = _collision.CheckPlayerCollision( ref _allCars, _player );
            _player.AddPoints( collisions * ( _collision.IsInLeftLane(_player) ? -8 : -2 ) );

            _collision.CheckEnemiesCollision( ref _allCars );
            _collision.CheckWallCollision( ref _allCars, _player );

            _givePointsTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if ( _givePointsTimer >= ( _collision.IsInLeftLane( _player ) ? 250 : 1000 ) ) {
                _player.AddPoints( 1 );
                _givePointsTimer = 0;
            }
            
            base.Update( gameTime );
        }

        /// <summary>
        /// Draws background, player _allCars and Score string
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw( GameTime gameTime ) {
            _spriteBatch.Begin();

            _background.Draw( _spriteBatch, _viewWindow );
            _player.Draw( _spriteBatch );

            foreach ( Car car in _allCars ) {
                car.Draw( _spriteBatch );
            }
            
            string points = $"Score: {_player.GetPoints()}";
            Vector2 fontOrigin = new Vector2( 0, 0 );
            _spriteBatch.DrawString(_font, points, _fontPos, Color.Blue, 0, fontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            _spriteBatch.End();

            base.Draw( gameTime );
        }
        /// <summary>
        /// Will ask if user wants to save the game before exit
        /// If choice is not cancel, fastEnemies, slowEnemies and player get ready to write to save_game_data.json
        /// If Yes, save to saved_game_data.json
        /// If No, upload to internet with application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected override void OnExiting( Object sender, EventArgs args ) {
            var choice = MessageBox.Show(
                "Do you want to save to a file (yes), internet (no) or not save at all (cancel)?",
                "Save Game?",
                MessageBoxButtons.YesNoCancel
            );

            

            if ( choice != DialogResult.Cancel  ) {
                var gameData = JsonConvert.SerializeObject(
                    new SaveGameData {
                        fastEnemies = _allCars.FindAll( car => car is FastEnemy ).Cast<FastEnemy>().ToList(),
                        slowEnemies = _allCars.FindAll( car => car is SlowEnemy ).Cast<SlowEnemy>().ToList(),
                        player = _player
                    },
                    Formatting.Indented,
                    _serializerSettings
                );

                switch ( choice ) {
                    case DialogResult.Yes:
                        File.WriteAllText(
                            @"saved_game_data.json",
                            gameData
                            );
                        break;

                    case DialogResult.No:
                        using ( WebClient client = new WebClient() ) {
                            client.Headers.Add( "Content-Type", "application/json" );
                            client.Encoding = System.Text.Encoding.UTF8;
                            _loadPreviousGameFromInternet =
                                client.UploadString( "http://localhost:8080/save_game", gameData );
                        }
                        break;
                }
            }
        }
    }
}
