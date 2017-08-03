using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using RBFinal.Enemies;
using RBFinal.towers;

namespace RBFinal.Levels
{
    public class Wave
    {
        public List<Projectile> Projectiles;
        public List<Enemy> Enemies;
        public Level Level;

        public Action<Wave> OnStart;
        public Action<Wave> OnEnd;

        private int _spawnTimerPlace;
        private double _waveTimer;

        private Dictionary<double, Enemy> _scheduale;

        public Wave(Level level)
        {
            Level = level;
            Enemies = new List<Enemy>();
            Projectiles = new List<Projectile>();
            _scheduale = new Dictionary<double, Enemy>();
        }

        public Wave(Level level, Action<Wave> onStart, Action<Wave> onEnd) : this(level)
        {
            OnStart = onStart;
            OnEnd = onEnd;
        }

        public void Draw(GameTime gameTime)
        {
            foreach (Enemy v in Enemies)
                if (v.Health != 0)
                    v.Draw(gameTime);

            foreach (Projectile p in Projectiles)
                p.Draw(gameTime);
        }

        public void Update(GameTime gameTime)
        {
            //If there are still spawn scheduales
            if (_spawnTimerPlace < _scheduale.Count)
            {
                //Add to the wave timer
                _waveTimer++;

                //If the scheduale's next value is less then the wave timer(turned into seconds)
                if (_scheduale.Keys.ToList()[_spawnTimerPlace] <= _waveTimer / 30)
                {
                    //Spawn a enemy
                    spawnVirus(_scheduale.Values.ToList()[_spawnTimerPlace]);
                    //Get ready for the next scheduale
                    _spawnTimerPlace++;
                }
            }

            //Update all viruses
            var viruses2 = new List<Enemy>(Enemies);
            foreach (Enemy v in viruses2)
                if (v.Health != 0)
                    v.Update(gameTime);

            //Update all viruses
            var projs2 = new List<Projectile>(Projectiles);
            foreach (Projectile p in projs2)
                p.Update(gameTime);
        }

        /// <summary>
        /// Add a enemy to the level
        /// - Makes it visable and makes it start moving
        /// </summary>
        /// <param name="enemy"></param>
        public void spawnVirus(Enemy enemy)
        {
            enemy.LastWaypoint = Level.getStart();
        }

        /// <summary>
        /// Remove the enemy from the wave
        /// </summary>
        /// <param name="enemy"></param>
        public void despawnEnemy(Enemy enemy)
        {
            Enemies.Remove(enemy);
        }

        public void destroyEnemy(Enemy enemy)
        {
            Level.GameScreen.Player.addReward(enemy.Reward);
            despawnEnemy(enemy);
        }

        /// <summary>
        /// Add a schedualed spawn with a auto time
        /// </summary>
        /// <param name="enemy"></param>
        public void addScheduale(Enemy enemy)
        {
            addScheduale(Enemies.Count, enemy);
        }


        /// <summary>
        /// Add a scheduale based of the time of the last scheduale
        /// </summary>
        /// <param name="sinceLast"></param>
        /// <param name="enemy"></param>
        public void addDepScheduale(double sinceLast, Enemy enemy)
        {
            addScheduale(_scheduale.Count == 0 ? 0 : _scheduale.Keys.ToList()[_scheduale.Count - 1] + sinceLast, enemy);
        }


        /// <summary>
        /// Add multiple at a set interval
        /// </summary>
        /// <param name="timeBetween"></param>
        /// <param name="amount"></param>
        /// <param name="t"></param>
        public void addScheduale(double timeBetween, int amount, Type t)
        {
            for (int i = 0; i < amount; i++)
            {
                Enemy e = (Enemy) Activator.CreateInstance(t, new object[] {Level.GameScreen.TowerDefense});
                addDepScheduale(timeBetween, e);
            }
        }

        /// <summary>
        /// Add a schedualed spawn
        /// </summary>
        /// <param name="time">Time is based off from the time the wave starts(Seconds)</param>
        /// <param name="enemy"></param>
        public void addScheduale(double time, Enemy enemy)
        {
            enemy.Location = Level.getStart().Location;

            Enemies.Add(enemy);
            enemy.setWave(this);
            _scheduale.Add(time, enemy);
        }

        /// <summary>
        /// Get if all viruses are dead
        /// </summary>
        /// <returns></returns>
        private bool allEnemiesDead()
        {
            return Enemies.Count == 0;
        }

        /// <summary>
        /// Get if the scheduale is finished
        /// </summary>
        /// <returns></returns>
        private bool hasFinishedScheudale()
        {
            return _spawnTimerPlace == _scheduale.Count;
        }

        /// <summary>
        /// Check if the wave is done by checking both allEnemiesDead() and hasFinishedScheduale()
        /// </summary>
        /// <returns></returns>
        public bool isDone()
        {
            return allEnemiesDead() && hasFinishedScheudale();
        }

        public void despawnProjectile(Projectile p)
        {
            Projectiles.Remove(p);
        }
    }
}