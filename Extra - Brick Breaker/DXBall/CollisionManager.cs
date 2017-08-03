using System;
using Microsoft.Xna.Framework;

namespace DXBall
{
    public class CollisionManager : GameComponent
    {
        private Game1 _game;

        public CollisionManager(Game1 game) : base(game)
        {
            _game = game;
        }

        public override void Update(GameTime gameTime)
        {
            Paddle paddle = _game.paddle;
            Level level = _game.getCurrentLevel();
            foreach (Ball ball in _game.balls)
            {
                    Brick top = level.collidedWithBrick(ball, new[] {Face.TOP});
                    Brick bottom = level.collidedWithBrick(ball, new[] {Face.BOTTOM});
                    Brick left = level.collidedWithBrick(ball, new[] {Face.LEFT});
                    Brick right = level.collidedWithBrick(ball, new[] {Face.RIGHT});

                    if (top != null)
                    {
                        ball.SpeedY = -Math.Abs(ball.SpeedY);
                        top.Health -= 1;
                    }
                    if (bottom != null)
                    {
                        ball.SpeedY = Math.Abs(ball.SpeedY);
                        bottom.Health -= 1;
                    }
                    if (left != null)
                    {
                        ball.SpeedX = -Math.Abs(ball.SpeedX);
                        left.Health -= 1;
                    }
                    if (right != null)
                    {
                        ball.SpeedX = Math.Abs(ball.SpeedX);
                        right.Health -= 1;
                    }
                    if (!level.hasBricksLeft())
                    {
                        level.hide();
                        _game.activeLevel++;
                        _game.levelLoaded = false;
                    }
                }

            }
    }
}