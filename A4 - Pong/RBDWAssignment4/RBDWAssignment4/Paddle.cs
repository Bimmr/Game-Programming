/*
 * Assignment 4
 * Paddle.cs
 * 
 * Randy Bimm - 11/05/16 - Created Class Framework
 * David Wagner - 11/08/16 - Coded Paddle logic
 * Randy Bimm - 11/08/16 - Added comments
 * Randy Bimm - 11/10/16 - Works with new GameState
 *
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RBDWAssignment4
{
    /// <summary>
    /// Class for the Paddle
    /// </summary>
    public class Paddle : DrawableGameComponent

    {
        //Game
        private Pong _game;
        private SpriteBatch _spriteBatch;

        //Object
        private Vector2 _location;
        private Texture2D _texture;
        private Keys _up, _down;

        //Property
        private int _speed = 6;

        #region Properties

        public Vector2 Location
        {
            get { return _location; }

            set { _location = value; }
        }

        public Texture2D Texture
        {
            get { return _texture; }

            set { _texture = value; }
        }

        #endregion

        /// <summary>
        /// Create a paddle
        /// </summary>
        /// <param name="game">The Game</param>
        /// <param name="texture">The Texture</param>
        /// <param name="location">The Location</param>
        /// <param name="up">Key to move up</param>
        /// <param name="down">Key to move down</param>
        public Paddle(Pong game, Texture2D texture, Vector2 location, Keys up, Keys down)
            : base(game)
        {
            _game = game;
            _spriteBatch = _game.SpriteBatch;
            _location = location;
            _texture = texture;
            _up = up;
            _down = down;
        }

        /// <summary>
        /// Update the paddle
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (_game.GameState == GameState.Playing)
            {
                KeyboardState ks = Keyboard.GetState();

                //Move up
                if (ks.IsKeyDown(_up) && _location.Y > 0)
                    _location.Y -= _speed;

                //Move down
                if (ks.IsKeyDown(_down) && _location.Y + _texture.Height <
                    _game.Graphics.PreferredBackBufferHeight - Display.scoreboardHeight)
                {
                    _location.Y += _speed;
                }
            }
        }

        /// <summary>
        /// Draw the paddle
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_texture, _location, Color.White);
            _spriteBatch.End();
        }

        /// <summary>
        /// Get a rectangle that represents the paddle
        /// </summary>
        /// <returns>Rectangle</returns>
        public Rectangle rectangle()
        {
            return new Rectangle((int)Location.X, (int)Location.Y, _texture.Width, _texture.Height);
        }
    }
}