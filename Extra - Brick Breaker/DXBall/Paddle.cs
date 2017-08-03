using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DXBall
{
    public class Paddle : DrawableGameComponent
    {
        private const int PADDLE_SPEED = 7;

        private Game1 _game;
        private SpriteBatch _spriteBatch;
        private Vector2 _stage;
        private Texture2D _texture2D;
        private Vector2 _location;

        private int _speed;


        private Rectangle _srcRectangle;
        private Rectangle _rectangle;

        public Rectangle Rectangle
        {
            get { return _rectangle; }
            set { _rectangle = value; }
        }

        public Vector2 Location
        {
            get { return _location; }

            set { _location = value; }
        }

        public Texture2D Texture2D
        {
            get { return _texture2D; }

            set { _texture2D = value; }
        }

        public Paddle(Game1 game, SpriteBatch spriteBatch, Texture2D texture2D, Vector2 location, Vector2 stage)
            : base(game)
        {
            _game = game;
            _spriteBatch = spriteBatch;
            _texture2D = texture2D;
            _location = location;
            _stage = stage;


            _speed = PADDLE_SPEED;
            _srcRectangle = new Rectangle(0, 0, _texture2D.Width, _texture2D.Height);
            _rectangle = new Rectangle((int) _location.X - _texture2D.Width / 2, (int) _location.Y, _texture2D.Width,
                _texture2D.Height);
        }


        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_texture2D, _rectangle, _srcRectangle, Color.White);
            _spriteBatch.End();

            //base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            bool movingLeft = false;
            bool movingRight = false;
            //Hits Right
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && _rectangle.Right < _stage.X)
            {
                movingRight = true;
                _location.X += _speed;
            }

            //Hits Left
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) && _rectangle.Left > 0)
            {
                movingLeft = true;
                _location.X -= _speed;
            }


            //Hits Paddle
            foreach (Ball ball in _game.balls)
            {

                if (_rectangle.Intersects(ball.Rectangle))
                {
                    ball.SpeedY = -Math.Abs(ball.SpeedY);


                    if (movingLeft)
                        ball.SpeedX = -Math.Abs(ball.SpeedX);

                    else if (movingRight)
                        ball.SpeedX = Math.Abs(ball.SpeedX);
                }
            }


            _rectangle.X = (int) _location.X;
            _rectangle.Y = (int) _location.Y;

            base.Update(gameTime);
        }
    }
}