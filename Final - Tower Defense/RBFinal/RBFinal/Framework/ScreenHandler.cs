using Microsoft.Xna.Framework;
using RBFinal.Screens;
using RBFinal.towers;

namespace RBFinal.Framework
{
    public static class ScreenHandler
    {

        public static Screen CurrentScreen;
        public static Screen LastScreen;
        public static MainScreen MainScreen;
        public static GameScreen GameScreen;
        public static HowToScreen HowToScreen;

        public static AboutScreen AboutScreen;

        public static void setup(TowerDefense towerDefense)
        {

            MainScreen = new MainScreen(towerDefense);
            GameScreen = new GameScreen(towerDefense);
            HowToScreen = new HowToScreen(towerDefense);
            AboutScreen = new AboutScreen(towerDefense);
        }


        public static void showScreen(Screen from, Screen to)
        {
            LastScreen = from;
            CurrentScreen = to;
            from?.hide();
            to?.show();
        }
    }
}