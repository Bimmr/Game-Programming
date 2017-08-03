using Microsoft.Xna.Framework;

namespace RBFinal.Framework
{
    /// <summary>
    /// Create an easy rectangle by giving 2 points
    /// Do not worry about if values are bigger/smaller then other values as EasyRectangle sorts it out for you.
    /// </summary>
    public class EasyRectangle
    {
        private Vector2 _p1;
        private Vector2 _p2;

        /// <summary>
        /// Create the rectangle
        /// </summary>
        /// <param name="p1">Point 1</param>
        /// <param name="p2">Point 2</param>
        public EasyRectangle(Vector2 p1, Vector2 p2)
        {
            if (p1.X < p2.X)
            {
                _p1.X = p1.X;
                _p2.X = p2.X;
            }
            else
            {
                _p2.X = p1.X;
                _p1.X = p2.X;
            }
            if (p1.Y < p2.Y)
            {
                _p1.Y = p1.Y;
                _p2.Y = p2.Y;
            }
            else
            {
                _p2.Y = p1.Y;
                _p1.Y = p2.Y;
            }
        }

        /// <summary>
        /// Get the Rectangle
        /// </summary>
        /// <returns>Rectangle</returns>
        public Rectangle getRectangle()
        {
            return new Rectangle((int)_p1.X, (int)_p1.Y, (int)(_p2.X - _p1.X), (int)(_p2.Y - _p1.Y));
        }
    }
}