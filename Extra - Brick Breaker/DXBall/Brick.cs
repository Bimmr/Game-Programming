using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DXBall
{
    public class Brick : DrawableGameComponent
    {
        public static int WIDTH = 60;
        public static int HEIGHT = 20;

        private Game1 _game;
        private SpriteBatch _spriteBatch;

        private Texture2D _texture2D;
        private Point _location;
        private Rectangle _srcRectangle;
        private Rectangle _rectangle;

        public Rectangle Rectangle
        {
            get { return _rectangle; }
            set { _rectangle = value; }
        }

        private int _health;

        public int MaxHealth
        {
            get { return _maxHealth; }
            set { _maxHealth = value; }
        }

        private int _maxHealth;

        public int Health
        {
            get { return _health; }
            set
            {
                if (value < 0)
                    value = 0;
                _health = value;
            }
        }

        public Brick(Game1 game, SpriteBatch spriteBatch, Texture2D texture2D, Point location, int maxHealth)
            : base(game)
        {
            _game = game;
            _spriteBatch = spriteBatch;
            _texture2D = texture2D;
            _location = location;
            _maxHealth = maxHealth;
            _health = maxHealth;

            _srcRectangle = new Rectangle(0, 0, WIDTH, HEIGHT);
            _rectangle = new Rectangle(_location.X, _location.Y, WIDTH, HEIGHT);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            if (_health > 0)
            {

                _spriteBatch.Draw(_texture2D, _rectangle, _srcRectangle, Color.White);
                _spriteBatch.Draw(_game.brickBrakeTexture, _rectangle, _srcRectangle,
                    Color.White * (1f - (float) _health / _maxHealth));
            }
            _spriteBatch.End();
        }

        public Rectangle getFace(Face face)
        {
            switch (face)
            {
                case Face.TOP:
                    return new Rectangle(_rectangle.Left, _rectangle.Top, WIDTH - 6, 1);
                case Face.BOTTOM:
                    return new Rectangle(_rectangle.Left, _rectangle.Bottom, WIDTH - 6, 1);
                case Face.LEFT:
                    return new Rectangle(_rectangle.Left, _rectangle.Top, 1, HEIGHT);
                case Face.RIGHT:
                    return new Rectangle(_rectangle.Right, _rectangle.Top, 1, HEIGHT);
                default:
                    throw new ArgumentOutOfRangeException(nameof(face), face, null);
            }
        }
    }
}