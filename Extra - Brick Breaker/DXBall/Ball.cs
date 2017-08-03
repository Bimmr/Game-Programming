using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace DXBall
{
    public class Ball : DrawableGameComponent
    {
        private int paddleCoolDown;
        private Game1 _game;
        private SpriteBatch _spriteBatch;
        private Vector2 _stage;
        private Texture2D _texture2D;
        private Vector2 _location;
        private Vector2 _speed;

        private Rectangle _srcRectangle;
        private Rectangle _rectangle;

        public Vector2 Location
        {
            get { return _location; }

            set { _location = value; }
        }

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

        public Ball(Game1 game, SpriteBatch spriteBatch, Texture2D texture2D, Vector2 location, Vector2 stage)
            : base(game)
        {
            _game = game;
            _spriteBatch = spriteBatch;
            _texture2D = texture2D;
            _location = location;
            _stage = stage;

            _speed = new Vector2(5, -5);
            _srcRectangle = new Rectangle(0, 0, _texture2D.Width, _texture2D.Height);
            _rectangle = new Rectangle((int) _location.X, (int) _location.Y, (int)(_texture2D.Width*.75), (int)(_texture2D.Height*.75));
        }

        public Rectangle Rectangle
        {
            get { return _rectangle; }
            set { _rectangle = value; }
        }


        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_texture2D, _rectangle, _srcRectangle, Color.White);
            _spriteBatch.End();
        }


        public override void Update(GameTime gameTime)
        {
            _location += _speed;

            //Hits Bottom
            if (_rectangle.Bottom >= _stage.Y)
            {
                _game.Components.Remove(this);
                //Lost
            }
            // Hits Right
            if (_rectangle.Right >= _stage.X)
            {
                _speed.X = -Math.Abs(_speed.X);
                _game.clickSound.Play();
            }
            //Hits Top
            if (_rectangle.Top <= 0)
            {
                _speed.Y = Math.Abs(_speed.Y);
                _game.clickSound.Play();
            }

            //Hits Left
            if (_rectangle.Left <= 0)
            {
                _speed.X = Math.Abs(_speed.X);
                _game.clickSound.Play();
            }

            _rectangle.X = (int) _location.X;
            _rectangle.Y = (int) _location.Y;


            //Hits Paddle
            if (_rectangle.Intersects(_game.paddle.Rectangle))
            {
                _speed.Y = -Math.Abs(_speed.Y);
                _game.clickSound.Play();
            }

        }
    }
}