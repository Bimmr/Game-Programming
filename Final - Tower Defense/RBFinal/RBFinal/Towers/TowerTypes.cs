using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RBFinal.Framework;
using RBFinal.Levels;
using RBFinal.Enemies;

namespace RBFinal.towers
{
    public class TowerHelper
    {
        public static void setup(Tower tower, Sprite sprite)
        {
            tower.setSprite(sprite);
        }
    }

    public class OrbTower : Tower
    {
        public static int radius = 64;
        public static double attackSpeed = 1.3;
        public static int damage = 15;
        public static int cost = 200;


        public OrbTower(Level level)
            : base(level, cost, radius, attackSpeed, damage)
        {
            TowerHelper.setup(this, SpriteHandler.TowerOrb);
        }

        public override void attack()
        {
            Enemy e = getFurthestAlong();

            if (e != null)
                spawnProjectile(new Projectile(Level.GameScreen.getWave(), SpriteHandler.ProjOrb, Location, e,
                    8));
        }
    }

    public class GlowTower : Tower
    {
        public static int radius = 50;
        public static double attackSpeed = 4.2;
        public static int damage = 10;
        public static int cost = 325;

        private const int maxAttack = 5;

        private const float scaleChange = 0.4f;
        private const float maxScale = 7f;


        private float _scale;
        private bool _attacking;

        public static Rectangle frame = new Rectangle(0, 0, 30, 30);


        public GlowTower(Level level)
            : base(level, cost, radius, attackSpeed, damage)
        {
            TowerHelper.setup(this, SpriteHandler.TowerGlow);


            beforeDraw = tower =>
            {
                SpriteBatch sb = Level.GameScreen.SpriteBatch;

                if (_attacking)
                {
                    sb.Draw(SpriteHandler.ProjGlow.Texture,
                        new Vector2(Location.X + SpriteHandler.ProjGlow.Source.X / 2,
                            Location.Y + SpriteHandler.ProjGlow.Source.Height / 2), SpriteHandler.ProjGlow.Source,
                        Color.White, 0f,
                        new Vector2(SpriteHandler.ProjGlow.getWidth() / 2, SpriteHandler.ProjGlow.getHeight() / 2),
                        _scale,
                        SpriteEffects.None, 1f);
                }
            };
            beforeUpdate = tower =>
            {
                if (_attacking)
                    if (_scale < maxScale)
                        _scale += scaleChange;
                    else
                        _attacking = false;
            };
            onReset = tower => {
                _attacking = false;
            };
        }

        public override void attack()
        {
            _scale = 1f;
            int attack = 0;

            List<Enemy> e2 = new List<Enemy>(Level.GameScreen.getWave().Enemies);
            foreach (Enemy e in e2)
            {
                if (withinRange(e))
                {
                    attack++;
                    doDamage(e);
                    _attacking = true;
                    if (attack > maxAttack)
                        break;
                }
            }
        }
    }
}