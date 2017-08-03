/*
 * Assignment 4
 * CollisionManager.cs
 *
 * Randy Bimm - 11/05/16 - Created Class Framework
 * Randy Bimm - 11/08/16 - Finished collision detection & Added comments
 * Randy Bimm - 11/09/16 - Ball moves faster on each paddle hit
 * Randy Bimm & DavidWagner - 11/10/16 - Added Sound
 *
 */

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace RBDWAssignment4
{
    /// <summary>
    /// Class for the CollisionManager
    /// </summary>
    public class CollisionManager : GameComponent
    {
        //SoundEffects
        public static SoundEffect clapSound;
        public static SoundEffect dingSound, clickSound;

        //Object
        private Pong _game;

        /// <summary>
        /// Create the collision manager object
        /// </summary>
        /// <param name="game">The Game</param>
        public CollisionManager(Pong game) : base(game)
        {
            _game = game;

            //Load content if not already loaded
            if (clapSound == null || dingSound == null || clickSound == null)
            {
                clapSound = _game.Content.Load<SoundEffect>("sounds/applause1");
                dingSound = _game.Content.Load<SoundEffect>("sounds/ding");
                clickSound = _game.Content.Load<SoundEffect>("sounds/click");
            }
        }

        /// <summary>
        /// Update the collisionManager
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Update(GameTime gameTime)
        {
            Ball ball = _game.Ball;
            Paddle leftPaddle = _game.LeftPaddle;
            Paddle rightPaddle = _game.RightPaddle;


            //Hits paddles
            if (ball.rectangle().Intersects(leftPaddle.rectangle()))
            {
                ball.SpeedX = Math.Abs(Math.Abs(ball.SpeedX) + .25f);
                dingSound.Play();
            }
            if (ball.rectangle().Intersects(rightPaddle.rectangle()))
            {
                ball.SpeedX = -Math.Abs(Math.Abs(ball.SpeedX) + .25f);
                dingSound.Play();
            }

            //Hits Bottom/Top of screen
            if (ball.Location.Y <= 0 ||
                ball.Location.Y + Ball.texture.Height >= Pong.HEIGHT - Display.scoreboardHeight)
            {
                ball.SpeedY *= -1;
                clickSound.Play();
            }


            //Player 1 gets point
            if (ball.Location.X + Ball.texture.Height > Pong.WIDTH)
            {
                if (_game.GameState == GameState.Playing)
                    clapSound.Play();

                _game.givePoint(1);
            }

            //Player 2 gets point
            if (ball.Location.X < 0)
            {
                if (_game.GameState == GameState.Playing)
                    clapSound.Play();

                _game.givePoint(2);
            }
        }
    }
}