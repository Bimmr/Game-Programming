using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RBFinal.Framework;

namespace RBFinal.Framework
{
    /// <summary>
    /// Sprite
    /// </summary>
    public class Sprite
    {
        public FrameHelper FrameHelper;
        public Texture2D Texture;
        public Rectangle Source;

        /// <summary>
        /// Create a sprite
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="source"></param>
        public Sprite(Texture2D texture, Rectangle source)
        {
            Texture = texture;
            Source = source;
        }

        public Sprite(Texture2D texture, FrameHelper frameHelper)
        {
            Texture = texture;
            FrameHelper = frameHelper;
        }


        /// <summary>
        /// Draw a Framed Texture with 1f Opacity
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="location"></param>
        public void drawFrame(SpriteBatch spriteBatch, Vector2 location)
        {
            drawFrame(spriteBatch, location, 1f);
        }

        /// <summary>
        /// Draw a Framed Texture
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="location"></param>
        /// <param name="opacity"></param>
        public void drawFrame(SpriteBatch spriteBatch, Vector2 location, float opacity)
        {
            Rectangle rec = FrameHelper.getCurrentFrame();
            if (rec != Rectangle.Empty)
                draw(spriteBatch, location, rec, opacity);
        }

        public void nextFrame()
        {
            FrameHelper.getNextFrame();
        }

        /// <summary>
        /// Draw a sprite using the Source with 1f Opacity
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="location"></param>
        public void draw(SpriteBatch spriteBatch, Vector2 location)
        {
            draw(spriteBatch, location, 1f);
        }

        /// <summary>
        /// Draw a sprite using the Source
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="location"></param>
        /// <param name="opacity"></param>
        public void draw(SpriteBatch spriteBatch, Vector2 location, float opacity)
        {
            draw(spriteBatch, location, Source, opacity);
        }

        public void draw(SpriteBatch spriteBatch, Vector2 location, Rectangle source, float opacity)
        {
            Rectangle loc = new Rectangle((int) location.X, (int) location.Y, getWidth(), getHeight());
            spriteBatch.Draw(Texture, loc, source, Color.White * opacity);
        }

        public int getWidth()
        {
            return Source != Rectangle.Empty ? Source.Width : FrameHelper.Width;
        }

        public int getHeight()
        {
            return Source != Rectangle.Empty ? Source.Height : FrameHelper.Height;
        }

        public Vector2 getSize()
        {
            return new Vector2(getWidth(), getHeight());
        }
    }
}