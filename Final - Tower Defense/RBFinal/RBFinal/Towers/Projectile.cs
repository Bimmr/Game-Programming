using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RBFinal.Framework;
using RBFinal.Levels;
using RBFinal.Enemies;

namespace RBFinal.towers
{
    public class Projectile
    {
        private Wave _wave;
        private Sprite _sprite;
        private Vector2 _location;
        public Enemy Enemy;

        private int _timeOutCounter;
        public int TimeOut;

        public Action<Projectile> OnHit;
        private int _speed;

        public Projectile( Wave wave, Sprite sprite, Vector2 location, Enemy enemy, int speed)
        {
            _wave = wave;
            _sprite = sprite;
            _location = location;
            Enemy = enemy;
            _speed = speed;
        }


        public void Update(GameTime gameTime)
        {
            if (TimeOut > 0)
            {
                _timeOutCounter++;
                if (_timeOutCounter >= TimeOut)
                    _wave.despawnProjectile(this);
            }
            if (Vector2.Distance(_location, Enemy.Location) < _speed)
            {
                OnHit.Invoke(this);
            }
            _location += Vector2.Normalize(Enemy.Location - _location) * _speed;
        }

        public void Draw(GameTime gameTime)
        {
            SpriteBatch sb = _wave.Level.GameScreen.SpriteBatch;
            _sprite.draw(sb, _location);
        }
    }
}