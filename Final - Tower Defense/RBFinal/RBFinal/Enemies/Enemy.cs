using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RBFinal.Framework;
using RBFinal.Components;
using RBFinal.Levels;

namespace RBFinal.Enemies
{
    public abstract class Enemy
    {
        public int MaxHealth;
        public int Health;
        public float Speed;
        public float MaxSpeed;
        public int Damage;
        public int Reward;

        public Vector2 Location;

        public Waypoint LastWaypoint;

        public Sprite Sprite;

        private Wave _wave;

        private int frameCount = 0;
        private int frameDelay = 2;

        /// <summary>
        /// Use SetTexture in the extended class' constructor
        /// </summary>
        /// <param name="towerDefense"></param>
        /// <param name="health"></param>
        /// <param name="speed"></param>
        /// <param name="damage"></param>
        protected Enemy(int health, float speed, int damage, int reward)
        {
            Health = health;
            MaxHealth = health;
            Speed = speed;
            MaxSpeed = speed;
            Damage = damage;
            Reward = reward;
        }

        public void setWave(Wave wave)
        {
            _wave = wave;
        }

        public void setSprite(Sprite sprite)
        {
            Sprite = sprite;
        }

        /// <summary>
        /// Get the bounding rectangle for the tower
        /// </summary>
        /// <returns></returns>
        public Rectangle getRectangle()
        {
            return new Rectangle((int) Location.X, (int) Location.Y, Sprite.getWidth(), Sprite.getHeight());
        }

        /// <summary>
        /// Draw the texture
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime)
        {

            SpriteBatch sb = _wave.Level.GameScreen.SpriteBatch;

            if (LastWaypoint?.NextPoint != null)
            {

                //Drawer.drawRectangle(getRectangle(), Color.Gray);
                //Drawer.drawRectangle(new Rectangle((int) LastWaypoint.Location.X, (int) LastWaypoint.Location.Y, 10, 10),Color.Green);
                //Drawer.drawRectangle(new Rectangle((int) LastWaypoint.NextPoint.Location.X,(int) LastWaypoint.NextPoint.Location.Y, 10, 10), Color.Blue);
            }

            Sprite.drawFrame(sb, Location);
            SpriteHandler.SPCracked.draw(sb, Location, 1f-(float)Health/MaxHealth);
        }

        /// <summary>
        /// Update the component
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {

            if (LastWaypoint != null)
            {
                frameCount++;
                if (frameCount > frameDelay)
                {
                    Sprite.nextFrame();
                    frameCount = 0;
                }

                //If location is equal to the next point set the next point
                if (LastWaypoint.NextPoint != null && Location == LastWaypoint.NextPoint.Location)
                    LastWaypoint = LastWaypoint.NextPoint;

                if (LastWaypoint == _wave.Level.getEnd())
                    _wave.Level.GameScreen.Player.doDamage(Damage);

                //If the next waypoint exists
                if (LastWaypoint.NextPoint != null)
                {
                    //Add to the viruses location (Normalized Vector from last to next * movement speed)
                    Location += LastWaypoint.NextPoint.getVectorToPointFrom(Location) * Speed;

                    //If the next distance gap is less then the speed the enemy moves, just move the enemy
                    if (Vector2.Distance(Location, LastWaypoint.NextPoint.Location) <= Speed)
                    {
                        Location = LastWaypoint.NextPoint.Location;
                    }
                }
                //Despawn the enemy if the next point is null
                else
                {
                    _wave.despawnEnemy(this);
                }
            }
        }

    }
}