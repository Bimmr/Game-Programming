using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RBFinal.Framework
{
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    public class ScrollingBackground
    {
        private Sprite _sprite;
        private Direction _direction;
        private Vector2 _speed;
        private Vector2 _location, _location2;

        public ScrollingBackground(Sprite sprite, Vector2 location, int speed, Direction direction)
        {
            _sprite = sprite;
            _location = location;
            _direction = direction;
            switch (direction)
            {
                case Direction.Left:
                    _speed = new Vector2(-speed, 0);
                    _location2 = new Vector2(location.X + sprite.getWidth(), _location.Y);
                    break;
                case Direction.Right:
                    _speed = new Vector2(speed, 0);
                    _location2 = new Vector2(location.X - sprite.getWidth(), _location.Y);
                    break;
                case Direction.Up:
                    _speed = new Vector2(0, -speed);
                    _location2 = new Vector2(location.X, _location.Y + sprite.getHeight());
                    break;
                case Direction.Down:
                    _speed = new Vector2(0, speed);
                    _location2 = new Vector2(location.X, _location.Y - sprite.getHeight());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public void move()
        {
            _location += _speed;
            _location2 += _speed;


            switch (_direction)
            {
                case Direction.Left:
                    if (_location.X <= -_sprite.getWidth())
                        _location.X = _location2.X + _sprite.getWidth();
                    if (_location2.X <= -_sprite.getWidth())
                        _location2.X = _location.X + _sprite.getWidth();
                    break;
                case Direction.Right:
                    if (_location.X >= _sprite.getWidth())
                        _location.X = _location2.X - _sprite.getWidth();
                    if (_location2.X >= _sprite.getWidth())
                        _location2.X = _location.X - _sprite.getWidth();
                    break;
                case Direction.Up:
                    if (_location.Y <= -_sprite.getHeight())
                        _location.Y = _location2.Y + _sprite.getHeight();
                    if (_location2.Y <= -_sprite.getHeight())
                        _location2.Y = _location.Y + _sprite.getHeight();
                    break;
                case Direction.Down:
                    if (_location.Y >= _sprite.getHeight())
                        _location.Y = _location2.Y - _sprite.getHeight();
                    if (_location2.Y >= _sprite.getHeight())
                        _location2.Y = _location.Y - _sprite.getHeight();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            _sprite.draw(spriteBatch, _location);
            _sprite.draw(spriteBatch, _location2);

        }
    }
}