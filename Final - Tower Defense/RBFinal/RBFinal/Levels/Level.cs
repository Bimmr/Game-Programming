using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using RBFinal.Framework;
using RBFinal.Components;
using RBFinal.Screens;
using RBFinal.towers;

namespace RBFinal.Levels
{
    public abstract class Level
    {
        public GameScreen GameScreen;

        public List<Wave> Waves;

        public List<Tower> Towers;

        private List<Waypoint> _waypoints;
        private Sprite _sprite;


        protected Level(GameScreen gameScreen, Sprite sprite)
        {
            GameScreen = gameScreen;
            _sprite = sprite;
            Waves = new List<Wave>();
            Towers = new List<Tower>();
            _waypoints = new List<Waypoint>();
        }

        public bool canSet(Vector2 location, Tower tower)
        {
            return GameScreen.Player.Gold >= tower.Cost && !Towers.Any(t => tower.getRectangle().Intersects(t.getRectangle())) &&  !getPathRectangles().Any(rec => tower.getRectangle().Intersects(rec));
        }

        public void setTower(Vector2 locaton, Tower tower)
        {
            Towers.Add(tower);
            SoundHandler.sfDrill.Play();
            tower.setLocation(locaton);
        }

        public List<Rectangle> getPathRectangles()
        {
            var recs = new List<Rectangle>();
            foreach(var w in _waypoints)
                if(w.NextPoint != null)
                    recs.Add(Waypoint.getPathRectangle(w, w.NextPoint));

            return recs;
        }

        public void Update(GameTime gameTime)
        {
            List<Tower> t2 = new List<Tower>(Towers);
            foreach (Tower t in t2)
            {
                if (t.Selected && GameScreen.Placing != t)
                    t.SellButton.Update(gameTime);
            }
            //If the game is playing
            if (GameScreen.GameState == GameState.Playing)
            {
                //Update all towers
                foreach (Tower t in Towers)
                    t.Update(gameTime);

                //Update current wave
                GameScreen.getWave()?.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            _sprite.draw(GameScreen.SpriteBatch, new Vector2(0,32));
            foreach (Tower t in Towers)
            {
                t.Draw(gameTime);

                if (t.Selected && GameScreen.Placing != t)
                    t.SellButton.Draw(gameTime);
            }

            GameScreen.getWave()?.Draw(gameTime);
        }

        /// <summary>
        /// Add a waypoint to the level, and set the variables needed for the waypoint
        /// </summary>
        /// <param name="loc"></param>
        public void addWaypoint(Vector2 loc)
        {
            Waypoint tp = new Waypoint(loc, null);
            if (_waypoints.Count != 0)
                _waypoints[_waypoints.Count - 1].NextPoint = tp;

            _waypoints.Add(tp);
        }

        /// <summary>
        /// Get the starting waypoint
        /// </summary>
        /// <returns></returns>
        public Waypoint getStart()
        {
            return _waypoints[0];
        }

        /// <summary>
        /// Get the ending waypoint
        /// </summary>
        /// <returns></returns>
        public Waypoint getEnd()
        {
            return _waypoints[_waypoints.Count - 1];
        }

        /// <summary>
        /// Add a wave
        /// </summary>
        /// <returns></returns>
        public Wave addWave()
        {
            return addWave(null, null);
        }

        /// <summary>
        /// Add a wave, with a custom on start event
        /// </summary>
        /// <param name="onStart"></param>
        /// <returns></returns>
        public Wave addWave(Action<Wave> onStart, Action<Wave> onEnd)
        {
            Wave wave = new Wave(this, onStart, onEnd);
            Waves.Add(wave);
            return wave;
        }


    }
}