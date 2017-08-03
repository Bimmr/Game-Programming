using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RBFinal.Framework
{
    public class Drawer
    {
        private static TowerDefense _towerDefense;


        /// <summary>
        /// Setup the drawer class
        /// </summary>
        /// <param name="towerDefense"></param>
        public static void setup(TowerDefense towerDefense)
        {
            _towerDefense = towerDefense;
        }

        /// <summary>
        /// Draw a rectangle
        /// </summary>
        /// <param name="rectangle">Contains x, y, width, height</param>
        /// <param name="color">Color of the rectangle</param>
        public static void drawRectangle(Rectangle rectangle, Color color)
        {

            Texture2D texture = new Texture2D(_towerDefense.Graphics.GraphicsDevice, 1, 1);
            texture.SetData(new[] {color});
            _towerDefense.SpriteBatch.Draw(texture, rectangle, color);
        }
    }
}