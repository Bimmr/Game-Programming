using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace DXBall
{
    public abstract class Level : GameComponent
    {
        private Game1 _game;

        public Game1 Game
        {
            get { return _game; }
            set { _game = value; }
        }

        private List<Brick> _bricks;

        protected Level(Game1 game) : base(game)
        {
            _game = game;
            _bricks = new List<Brick>();
        }

        public void addBrick(Brick brick)
        {
            _bricks.Add(brick);
        }

        public Brick collidedWithBrick(Ball ball, Face[] faces)
        {
            if (_bricks.Count > 0)
            {
                Brick brick =
                    _bricks.Where(b => b.Health > 0)
                        .FirstOrDefault(b => faces.Any(face => ball.Rectangle.Intersects(b.getFace(face))));

                _game.dingSound.Play();

                //base.Update(gameTime);
                return brick;
            }
            return null;
        }

        public override void Update(GameTime gameTime)
        {
        }

        public void display()
        {
            create();
            _game.Components.Add(this);
            if (_bricks.Count > 0)
                foreach (Brick brick in _bricks)
                {
                    brick.Health = brick.MaxHealth;
                    _game.Components.Add(brick);
                }
        }

        public void hide()
        {
            if (_bricks.Count > 0)
                foreach (Brick brick in _bricks)
                    _game.Components.Remove(brick);
        }

        public bool hasBricksLeft()
        {
            if (_bricks.Count > 0)
                return _bricks.Any(brick => brick.Health > 0);
            return true;
        }

        public abstract void create();
    }
}