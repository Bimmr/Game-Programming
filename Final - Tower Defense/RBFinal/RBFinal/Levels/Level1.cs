using Microsoft.Xna.Framework;
using RBFinal.Enemies;
using RBFinal.Screens;
using RBFinal.Framework;
using RBFinal.Components;

namespace RBFinal.Levels
{
    public class Level1 : Level
    {
        public Level1(TowerDefense game, GameScreen gameScreen): base(gameScreen, SpriteHandler.BkgLevel1)
        {

            addWaypoint(new Vector2(-30, 150));
            addWaypoint(new Vector2(100, 150));
            addWaypoint(new Vector2(100, 75));
            addWaypoint(new Vector2(200, 75));
            addWaypoint(new Vector2(200, 300));
            addWaypoint(new Vector2(75, 300));
            addWaypoint(new Vector2(75, 400));
            addWaypoint(new Vector2(300, 400));
            addWaypoint(new Vector2(300, 100));
            addWaypoint(new Vector2(500, 100));
            addWaypoint(new Vector2(500, 510));

            //Wave 1
            Wave wave1 = addWave(
                wave => GameScreen.showNotification("Welcome!", new[] {"The first level is pretty easy.", "Just hit the green play button to start."}, null),
                wave => GameScreen.Player.addReward(100));
            wave1.addScheduale(1, 20, typeof(RedEnemy));

            //Wave 2
            Wave wave2 = addWave(
                null,
                wave => GameScreen.Player.addReward(100));
            wave2.addScheduale(1, 30, typeof(RedEnemy));

            //Wave 3
            Wave wave3 = addWave(
                    wave => GameScreen.showNotification("Nice!", new[] {"Just a heads up!", "Blue enemies have x2 health"}, null),
                    wave => GameScreen.Player.addReward(100));
            wave3.addScheduale(1, 15, typeof(RedEnemy));
            wave3.addScheduale(1, 5, typeof(BlueEnemy));

            //Wave 4

            Wave wave4 = addWave(
                null,
                wave => GameScreen.Player.addReward(100));
            wave4.addScheduale(1, 20, typeof(RedEnemy));
            wave4.addScheduale(.5, 5, typeof(RedEnemy));
            wave4.addScheduale(1, 15, typeof(BlueEnemy));

            //Wave 5
            Wave wave5 = addWave(
                wave => GameScreen.showNotification("Awesome!", new[] {"Now what if most of them have more health?"}, null),
                wave => GameScreen.Player.addReward(100));
            wave5.addScheduale(1, 5, typeof(RedEnemy));
            wave5.addScheduale(.5, 10, typeof(RedEnemy));
            wave5.addScheduale(1, 20, typeof(BlueEnemy));

            //Wave 6
            Wave wave6 = addWave(
                null,
                wave => GameScreen.Player.addReward(100));

            wave6.addScheduale(1, 5, typeof(BlueEnemy));
            wave6.addScheduale(1, 10, typeof(RedEnemy));
            wave6.addScheduale(.75, 15, typeof(RedEnemy));
            wave6.addScheduale(1, 20, typeof(BlueEnemy));

            //Wave 7
            Wave wave7 = addWave(
                wave => GameScreen.showNotification("Watch Out!", new[] {"The green enemies are speedy little buggers."}, null),
                wave => GameScreen.Player.addReward(100));
            wave7.addScheduale(1, 15, typeof(BlueEnemy));
            wave7.addScheduale(1, 20, typeof(RedEnemy));
            wave7.addScheduale(.4, 5, typeof(BlueEnemy));
            wave7.addScheduale(1, 10, typeof(GreenEnemy));
            wave7.addScheduale(1, 20, typeof(RedEnemy));

            //Wave 8
            Wave wave8 = addWave(
                wave => GameScreen.showNotification("...", new[] {"Think your good, eh?"}, null),
                wave => GameScreen.Player.addReward(100));
            wave8.addScheduale(1, 20, typeof(BlueEnemy));
            wave8.addScheduale(.4, 20, typeof(RedEnemy));
            wave8.addScheduale(.4, 10, typeof(BlueEnemy));
            wave8.addScheduale(1, 20, typeof(GreenEnemy));
            wave8.addScheduale(1, 10, typeof(BlueEnemy));
        }
    }
}