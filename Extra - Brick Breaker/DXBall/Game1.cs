using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Configuration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace DXBall
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //Game
        private GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        //Textures
        public Texture2D ballTexture;
        public Texture2D paddleTexture;
        public Texture2D brickTexture;
        public Texture2D brickBrakeTexture;

        //Objects
        public List<Ball> balls;
        public Paddle paddle;

        //Levels
        public List<Level> levels;
        public int activeLevel;
        public bool levelLoaded;

        //Sound
        public SoundEffect clickSound;
        public SoundEffect dingSound;

        //Vectors
        public Vector2 stage;
        public Vector2 paddleInitPos;
        public Vector2 ballInitPos;

        private bool spaceToggle;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }

        public Level getCurrentLevel()
        {
            return levels[activeLevel];
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            activeLevel = 0;

            balls = new List<Ball>();

            stage = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            paddleInitPos = new Vector2(stage.X / 2, stage.Y - 50);


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //Load levels

            levels = new List<Level>
            {
                //new LevelTest(this),
                new Level1(this),
                new Level2(this)
            };

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ballTexture = Content.Load<Texture2D>("Images/Ball");
            paddleTexture = Content.Load<Texture2D>("Images/Bat");
            brickTexture = Content.Load<Texture2D>("Images/Brick");
            brickBrakeTexture = Content.Load<Texture2D>("Images/BrickBrake");

            ballInitPos = new Vector2(stage.X / 2 - ballTexture.Width / 2, stage.Y - 50 - ballTexture.Height);

            Song backgroundMusic = Content.Load<Song>("Music/chimes");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);

            clickSound = Content.Load<SoundEffect>("Music/click");
            dingSound = Content.Load<SoundEffect>("Music/ding");

            Components.Add(new CollisionManager(this));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (!levelLoaded)
            {
                getCurrentLevel().display();
                createNewPaddle();

                foreach (Ball ball in balls)
                    Components.Remove(ball);
                balls.Clear();


                createNewBall(ballInitPos);

                levelLoaded = true;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !spaceToggle)
            {
                spaceToggle = true;
                createNewBall(ballInitPos);
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Space) && spaceToggle)
                spaceToggle = false;

            // TODO: Add your update logic here

            Level level = getCurrentLevel();



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Cornsilk);
            base.Draw(gameTime);
        }


        public void createNewBall(Vector2 initPos)
        {
            Ball b = new Ball(this, spriteBatch, ballTexture, initPos, stage);
            Components.Add(b);
            balls.Add(b);
        }

        public void createNewPaddle()
        {
            if (paddle != null)
                Components.Remove(paddle);
            Paddle p = new Paddle(this, spriteBatch, paddleTexture, paddleInitPos, stage);
            Components.Add(p);
            paddle = p;
        }
    }
}