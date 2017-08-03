using Microsoft.Xna.Framework;

namespace RBFinal.Framework
{
    public class Layout
    {
        //Size of the window
        public static Vector2 Size;

        //Middle point of the window
        public static Vector2 Middle => new Vector2(Size.X / 2, Size.Y / 2);

        public static void setup(TowerDefense towerDefense)
        {
            Size = new Vector2(towerDefense.Graphics.PreferredBackBufferWidth, towerDefense.Graphics.PreferredBackBufferHeight);
        }
    }
}