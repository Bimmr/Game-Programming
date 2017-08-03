/*
 * Assignment 4
 * Pong.cs
 * 
 * Randy Bimm - 11/05/16 - Generated Class
 * Randy Bimm - 11/07/16 - Added variables to track game
 * Randy Bimm - 11/08/16 - Tracks wins & Added comments
 * Randy Bimm - 11/10/16 - Added Max Score & Added Restart
 *
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RBDWAssignment4
{

    /// <summary>
    /// Enum for the gamestate
    /// </summary>
    public enum GameState { Playing, Paused, GameOver }

    /// <summary>
    /// Class for the whole game
    /// </summary>
    public class Pong : Game
    {
        public static int WIDTH, HEIGHT;

        public const int MAXWINS = 2;
        //Game
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //Object
        private Display _display;
        private CollisionManager _collisionManager;
        private Ball _ball;
        private Paddle _leftPaddle, _rightPaddle;
        private GameState _gameState = GameState.Paused;

        //Other
        private KeyboardState _lastKeyBoardState;
        private int[] wins = { 0, 0 };

        //Textures
        private Texture2D _leftPaddleTexture, _rightPaddleTexture;

        #region Properties

        public CollisionManager CollisionManager
        {
            get { return _collisionManager; }

            set { _collisionManager = value; }
        }

        public Ball Ball
        {
            get { return _ball; }

            set { _ball = value; }
        }

        public Paddle LeftPaddle
        {
            get { return _leftPaddle; }

            set { _leftPaddle = value; }
        }

        public Paddle RightPaddle
        {
            get { return _rightPaddle; }

            set { _rightPaddle = value; }
        }

        public Display Display
        {
            get { return _display; }

            set { _display = value; }
        }

        public GraphicsDeviceManager Graphics
        {
            get { return _graphics; }

            set { _graphics = value; }
        }

        public SpriteBatch SpriteBatch
        {
            get { return _spriteBatch; }

            set { _spriteBatch = value; }
        }

        public GameState GameState
        {
            get
            {
                return _gameState;
            }

            set
            {
                _gameState = value;
            }
        }


        #endregion

        /// <summary>
        /// Initialize the game
        /// </summary>
        public Pong()
        {
            _graphics = new GraphicsDeviceManager(this);
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
            base.Initialize();

            WIDTH = _graphics.PreferredBackBufferWidth;
            HEIGHT = _graphics.PreferredBackBufferHeight;

            createPong();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Init the objects, which loads textures if not already loaded
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _collisionManager = new CollisionManager(this);
            _display = new Display(this);
            Components.Add(_display);
            Components.Add(_collisionManager);

            //Create Paddles
            _rightPaddleTexture = Content.Load<Texture2D>("images/BatLeft");
            _leftPaddleTexture = Content.Load<Texture2D>("images/BatRight");

            createPong();
        }

        /// <summary>
        /// Set everything up needed to play the game
        /// </summary>
        public void createPong()
        {
            if (Ball != null)
            {
                Components.Remove(_ball);
                Components.Remove(_leftPaddle);
                Components.Remove(_rightPaddle);
            }

            //Create Components
            _ball = new Ball(this);

            Vector2 leftPaddleLocation = 
                new Vector2(0 + 10, HEIGHT / 2 - _leftPaddleTexture.Height / 2);
            Vector2 rightPaddleLocation =
                new Vector2((WIDTH - 10) - _rightPaddleTexture.Width,
                    HEIGHT / 2 - _rightPaddleTexture.Height / 2);

            LeftPaddle = new Paddle(this, _leftPaddleTexture, leftPaddleLocation, Keys.A, Keys.Z);
            RightPaddle = new Paddle(this, _rightPaddleTexture, rightPaddleLocation, 
                Keys.Up, Keys.Down);

            CollisionManager = new CollisionManager(this);

            Components.Add(_ball);
            Components.Add(_leftPaddle);
            Components.Add(_rightPaddle);
        }

        /// <summary>
        /// Reset the game's win count
        /// </summary>
        public void resetGame()
        {
            createPong();
            wins = new[] { 0, 0 };
            _display.LocalWins = new[] { 0, 0 };
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Enter) && !_lastKeyBoardState.IsKeyDown(Keys.Enter) 
                && _gameState == GameState.Paused)
            {
                createPong();
                _gameState = GameState.Playing;
            }

            if (ks.IsKeyDown(Keys.Space) && !_lastKeyBoardState.IsKeyDown(Keys.Space) 
                && _gameState == GameState.GameOver)
            {
                resetGame();
                _gameState = GameState.Paused;
            }

            //Remember the keystates that were pressed
            _lastKeyBoardState = Keyboard.GetState();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            base.Draw(gameTime);
        }

        /// <summary>
        /// Get how many wins a player has
        /// </summary>
        /// <param name="player">Player (1-2)</param>
        /// <returns>Wins</returns>
        public int getWins(int player)
        {
            return wins[player - 1];
        }

        /// <summary>
        /// Gives a player a point, and gets a new game ready to go
        /// </summary>
        /// <param name="player">Player (1-2)</param>
        public void givePoint(int player)
        {
            if (_gameState == GameState.Playing)
            {
                wins[player - 1]++;
                if (wins[player - 1] == MAXWINS)
                    _gameState = GameState.GameOver;
                else
                    _gameState = GameState.Paused;
            }
        }
    }
}