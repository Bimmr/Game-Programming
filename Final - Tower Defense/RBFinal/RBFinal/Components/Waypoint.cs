using Microsoft.Xna.Framework;
using RBFinal.Framework;
using RBFinal.Components;

namespace RBFinal.Components
{
    /// <summary>
    /// Waypoints
    /// </summary>
    public class Waypoint
    {
        public const int PATHSIZE = 32;

        public Waypoint NextPoint;
        public Vector2 Location;

        /// <summary>
        /// Create a waypoint
        /// </summary>
        /// <param name="location"></param>
        /// <param name="nextPoint"></param>
        public Waypoint(Vector2 location, Waypoint nextPoint)
        {
            Location = location;
            NextPoint = nextPoint;
        }

        /// <summary>
        /// Create a 1-0 vector to show the direction to the given location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public Vector2 getVectorToPointFrom(Vector2 location)
        {
            return Vector2.Normalize(Location - location);
        }

        /// <summary>
        /// Get a 1x1 rectangle on the waypoint's location
        /// </summary>
        /// <returns></returns>
        public Rectangle getRectangle()
        {
            return new Rectangle((int) Location.X, (int) Location.Y, 1, 1);
        }

        /// <summary>
        /// Create a rectangle between point A and point B
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <returns></returns>
        public static Rectangle getPathRectangle(Waypoint pointA, Waypoint pointB)
        {
            Rectangle rec = new EasyRectangle(pointA.Location, pointB.Location).getRectangle();

            //If the object is moving along the Y axis
            if (pointA.Location.X == pointB.Location.X)
            {
                if (rec.Width < PATHSIZE)
                    rec.Width = PATHSIZE+1;

                rec.X -=1;
                rec.Height += PATHSIZE;
            }

            //If the object is moving along the X axis
            if (pointA.Location.Y == pointB.Location.Y)
            {
                if(rec.Height< PATHSIZE)
                    rec.Height = PATHSIZE+1;

                rec.Y -= 1;
                rec.Width += PATHSIZE;
            }

            return rec;
        }
    }
}