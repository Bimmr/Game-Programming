using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RBFinal.Framework
{
    /// <summary>
    /// Misc class of Utilities
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// Get a vector to center the given text within the rectangle
        /// </summary>
        /// <param name="spriteFont"></param>
        /// <param name="text"></param>
        /// <param name="rec"></param>
        /// <returns></returns>
        public static Vector2 centerText(SpriteFont spriteFont, string text, Rectangle rec)
        {
            Vector2 vec = Vector2.Zero;

            Vector2 textSize = spriteFont.MeasureString(text);

            vec.X = rec.X + (rec.Width / 2 - textSize.X / 2);
            vec.Y = rec.Y + (rec.Height / 2 - textSize.Y / 2);

            return vec;
        }
    }
}