using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RBFinal.Framework;

namespace RBFinal.Enemies
{
    /// <summary>
    /// Help make viruses
    /// </summary>
    public class EnemyHelper
    {
        public static Texture2D Texture;
        public static int Rows = 1;
        public static int cols = 14;

        public static Texture2D getTexture(TowerDefense towerDefense)
        {
            return Texture ?? (Texture = towerDefense.Content.Load<Texture2D>("images/enemytextures"));
        }

        public static void setup(TowerDefense towerDefense, Enemy enemy, Rectangle rec)
        {
            enemy.setSprite(new Sprite(getTexture(towerDefense), new FrameHelper(rec.X, rec.Y, rec.Width, rec.Height, EnemyHelper.Rows, EnemyHelper.cols, true)));
        }
    }

    /// <summary>
    /// Basic Enemy
    /// </summary>
    public class RedEnemy : Enemy
    {
        private const int health = 10;
        private const float speed = 2f;
        private const int damage = 1;
        private const int reward = 1;

        public static Rectangle frame = new Rectangle(0, 0, 32, 32);

        public RedEnemy(TowerDefense towerDefense) : base(health, speed, damage, reward)
        {
           EnemyHelper.setup(towerDefense, this, frame);
        }
    }


    /// <summary>
    /// Tank Enemy
    /// </summary>
    public class BlueEnemy : Enemy
    {
        private const int health = 20;
        private const float speed = 2.5f;
        private const int damage = 2;
        private const int reward = 2;

        public static Rectangle frame = new Rectangle(0, 32, 32, 32);

        public BlueEnemy(TowerDefense towerDefense) : base( health, speed, damage, reward)
        {
            EnemyHelper.setup(towerDefense, this, frame);
        }
    }
    /// <summary>
    /// Fast moving enemy
    /// </summary>
    public class GreenEnemy: Enemy
    {
        private const int health = 25;
        private const float speed = 4f;
        private const int damage = 5;
        private const int reward = 3;
        public static Rectangle frame = new Rectangle(0, 64, 32, 32);

        public GreenEnemy(TowerDefense towerDefense) : base( health, speed, damage,reward)
        {
            EnemyHelper.setup(towerDefense, this, frame);
        }
    }
}