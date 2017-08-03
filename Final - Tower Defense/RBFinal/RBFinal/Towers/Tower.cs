using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RBFinal.Components;
using RBFinal.Framework;
using RBFinal.Levels;
using RBFinal.Enemies;

namespace RBFinal.towers
{
    public abstract class Tower
    {
        private const int _rectangleSpace = 4;

        public int Radius;
        public int AttackSpeed;
        public int Damage;
        public int Cost;
        public Vector2 Location;
        public Level Level;
        public Tower UpgradedTower;
        public Sprite Sprite;
        public bool Selected;
        public Color RadiusColor;

        public Action<Tower> beforeDraw;
        public Action<Tower> beforeUpdate;
        public Action<Tower> afterDraw;
        public Action<Tower> afterUpdate;
        public Action<Tower> onReset;

        public Button SellButton;

        private int _attackCounter;

        protected Tower(Level level, int cost, int radius, double attackSpeed, int damage)
        {
            Level = level;
            Cost = cost;
            Radius = radius;
            AttackSpeed = (int) (attackSpeed * 30.0);
            Damage = damage;
            RadiusColor = Color.White;
        }

        public void setLocation(Vector2 location)
        {
            Location = location;
            Vector2 fontSize = FontHandler.RegularFont.MeasureString("Sell For: $" + (int) (Cost * .75));

            Rectangle rec = new Rectangle(
                (int) (location.X + Sprite.getWidth() / 2) - (int) (fontSize.X / 2),
                (int) location.Y + Sprite.getHeight() + 10,
                (int) fontSize.X,
                (int) fontSize.Y
            );

            SellButton = new Button(Level.GameScreen.TowerDefense, rec, FontHandler.RegularFont,
                    "Sell For: $" + (int) (Cost * .75), Color.Red,
                    (button, state) =>
                    {
                        if (Level.GameScreen.LastMouseState.LeftButton != ButtonState.Pressed)
                        {
                            Level.Towers.Remove(this);
                            Level.GameScreen.Player.addReward((int) (Cost * .75));
                            setLocation(Vector2.Zero);
                        }
                    },
                    null);
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
            return new Rectangle((int) Location.X + _rectangleSpace, (int) Location.Y + Sprite.getHeight() / 4,
                Sprite.getWidth() - _rectangleSpace, Sprite.getHeight() - Sprite.getHeight() / 4);
        }

        /// <summary>
        /// Draw the texture
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime)
        {
            beforeDraw?.Invoke(this);
            SpriteBatch sb = Level.GameScreen.SpriteBatch;
            if (Selected)
                drawRange(sb);

            Sprite.draw(sb, Location);
            afterDraw?.Invoke(this);
        }

        /// <summary>
        /// Update the component
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            beforeUpdate?.Invoke(this);

            if (_attackCounter < AttackSpeed)
                _attackCounter++;
            else
            {
                _attackCounter = 0;
                attack();
            }
            afterUpdate?.Invoke(this);
        }

        public void doDamage(Enemy enemy)
        {
            enemy.Health -= Damage;
            if (enemy.Health <= 0)
            {
                Level.GameScreen.getWave()?.destroyEnemy(enemy);
            }
        }

        public Enemy getFurthestAlong()
        {
            return Level.GameScreen.getWave()?.Enemies.FirstOrDefault(withinRange);
        }

        public bool withinRange(Enemy enemy)
        {
            Vector2 middleOfTower = new Vector2(Location.X + Sprite.getWidth() / 2,
                Location.Y + Sprite.getHeight() / 2);
            Vector2 middleOfEnemy = new Vector2(enemy.Location.X + enemy.getRectangle().Width / 2,
                enemy.Location.Y + enemy.getRectangle().Height / 2);

            bool inRange = Vector2.Distance(middleOfTower, middleOfEnemy) <= Radius + enemy.Sprite.getWidth() / 4 &&
                           Level.GameScreen.getWave().Enemies.Any(v => v == enemy);
            return inRange;
        }


        public void spawnProjectile(Projectile projectile)
        {
            Wave wave = Level.GameScreen.getWave();
            projectile.OnHit = projectile1 =>
            {
                doDamage(projectile.Enemy);
                wave.despawnProjectile(projectile);
            };
            wave.Projectiles.Add(projectile);
        }

        public void drawRange(SpriteBatch spriteBatch)
        {
            float scale = Radius / 32f * 2f;
            Vector2 middleOfTower = new Vector2(Location.X + Sprite.getWidth() / 2,
                Location.Y + Sprite.getHeight() / 2);

            spriteBatch.Draw(
                SpriteHandler.SPRange.Texture,
                middleOfTower,
                SpriteHandler.SPRange.Source,
                RadiusColor, 0f,
                new Vector2(
                    SpriteHandler.SPRange.getWidth() / 2,
                    SpriteHandler.SPRange.getHeight() / 2),
                scale,
                SpriteEffects.None, 1f);
        }

        public abstract void attack();

        public void reset()
        {
            _attackCounter = 0;
            onReset?.Invoke(this);
        }
    }
}