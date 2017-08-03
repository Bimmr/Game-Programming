/*
 * Assignment 4
 * Ball.cs
 * 
 * Randy Bimm - 11/05/16 - Created Class Framework
 * David Wagner - 11/08/16 - Coded Ball logic
 * Randy Bimm - 11/-8/16 - Added Comments
 * Randy Bimm - 11/10/16 - Works with new GameState
 *
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace RBDWAssignment4
{
    /// <summary>
    /// The class for the Ball
    /// </summary>
    public class Ball : DrawableGameComponent
    {
        //Texture
        public static Texture2D texture;

        //Game
        private Pong _game;
        private SpriteBatch _spriteBatch;

        //Object
        private Vector2 _location;
        private Vector2 _speed;

        #region Properties

        public float SpeedX
        {
            get { return _speed.X; }

            set { _speed.X = value; }
        }

        public float SpeedY
        {
            get { return _speed.Y; }

            set { _speed.Y = value; }
        }

        public Vector2 Location
        {
            get { return _location; }

            set { _location = value; }
        }

        #endregion

        /// <summary>
        /// Create a new instance of a Ball
        /// </summary>
        /// <param name="game"></param>
        public Ball(Pong game) : base(game)
        {
            _game = game;
            _spriteBatch = _game.SpriteBatch;

            if (texture == null)
                texture = _game.Content.Load<Texture2D>("images/Ball");


            //Set init ball location
            _location = new Vector2(Pong.WIDTH / 2 - texture.Width / 2,
                Pong.HEIGHT / 2 - texture.Height / 2);

            //Create random x and y
            _speed.X = new Random().Next(3, 9);
            _speed.Y = new Random().Next(3, 9);

            //Randomize init ball speed
            if (new Random(DateTime.Now.Millisecond).Next(10) % 2 == 0)
                _speed.X = -Math.Abs(_speed.X);

            if (new Random(DateTime.Now.Second).Next(10) % 2 == 0)
                _speed.Y = -Math.Abs(_speed.Y);
        }

        /// <summary>
        /// Get a recrangle that represents the ball
        /// </summary>
        /// <returns>Rectangle</returns>
        public Rectangle rectangle()
        {
            return new Rectangle((int) _location.X, (int) _location.Y, texture.Width, texture.Height);
        }


        /// <summary>
        /// Update the ball
        /// </summary>
        /// <param name="gameTime">The Gametime</param>
        public override void Update(GameTime gameTime)
        {
            if (_game.GameState == GameState.Playing)
            {
                _location += _speed;
            }
        }

        /// <summary>
        /// Draw the ball
        /// </summary>
        /// <param name="gameTime">The Gametime</param>
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(texture, Location, Color.White);
            _spriteBatch.Draw(texture, Location, Color.LightGreen*.99f);
            _spriteBatch.End();
        }
    }
}