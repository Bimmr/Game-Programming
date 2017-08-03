/*
 * Assignment 4
 * Display.cs
 * 
 * Randy Bimm - 11/05/16 - Created Class Framework
 * David Wagner - 11/08/16 - Get scoreboard texture and size outside class
 * Randy Bimm - 11/08/16 - Added comments
 * Randy Bimm & David Wagner - 11/09/16 - Added score and start strings 
 * Randy Bimm - 11/10/16 - Added Max Score & Added Restart
 * David Wagner- 11/10/16 - Fixed player strings and victory strings 
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RBDWAssignment4
{
    /// <summary>
    /// Class for the Display
    /// </summary>
    public class Display : DrawableGameComponent
    {
        //Texture
        public static Texture2D scoreboadTexture;
        public static int scoreboardHeight = 25;

        private static SpriteFont myFont;

        //Game
        private Pong _game;
        private SpriteBatch _spriteBatch;

        private int[] localWins = { 0, 0 };

        #region Properties
        public int[] LocalWins
        {
            get
            {
                return localWins;
            }

            set
            {
                localWins = value;
            }
        }
        #endregion

        /// <summary>
        /// Create a display
        /// </summary>
        /// <param name="game"></param>
        public Display(Pong game) : base(game)
        {
            _game = game;
            _spriteBatch = _game.SpriteBatch;

            if (scoreboadTexture == null)
                scoreboadTexture = _game.Content.Load<Texture2D>("images/ScoreBar");

            if (myFont == null)
                myFont = _game.Content.Load<SpriteFont>("Fonts/myFont");

        }

        /// <summary>
        /// Draw the display
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            int width = Pong.WIDTH;
            int height = Pong.HEIGHT;

            //Draw middle line
            drawRectangle(new Rectangle(width / 2 - 1, 0, 2, height), Color.DarkGray);

            //Draw scoreboard on bottom
            _spriteBatch.Draw(scoreboadTexture,
                new Rectangle(0, height - scoreboardHeight, width, scoreboardHeight), Color.White);

            string randyWins = "RANDY: " + _game.getWins(1);
            string davidWins = "DAVID: " + _game.getWins(2);

            //Draw Randy's Score
            _spriteBatch.DrawString(myFont, randyWins,
                new Vector2(200, height - scoreboardHeight / 2 - myFont.LineSpacing / 2),
                Color.Black);

            //Draw David's Score
            _spriteBatch.DrawString(myFont, davidWins,
                new Vector2(width - myFont.MeasureString(davidWins).Length() - 200,
                    height - scoreboardHeight / 2 - myFont.LineSpacing / 2),
                Color.Black);

            //Get if anyone just got a point
            int gotPoint = 0;
            for (int i = 0; i < 2; i++)
                if (_game.getWins(i + 1) != LocalWins[i])
                {
                    gotPoint = i + 1;
                    if (_game.GameState == GameState.Playing)
                        LocalWins[i] = _game.getWins(i + 1);
                }

            //Draw information text
            if (_game.GameState == GameState.Paused)
            {

                //If someone recently got a point, say who
                if (gotPoint != 0)
                {

                    //Press Enter to resume
                    _spriteBatch.DrawString(myFont, "PRESS ENTER TO RESUME",
                    new Vector2(width / 2 - myFont.MeasureString("PRESS ENTER TO RESUME").Length() / 2,
                        height / 2 - myFont.LineSpacing / 2 - 100), Color.Black);



                    string whoGotPoint = "- " + (gotPoint == 1 ? "RANDY" : "DAVID") + " SCORED -";
                    _spriteBatch.DrawString(myFont, whoGotPoint,
                        new Vector2(width / 2 - myFont.MeasureString(whoGotPoint).Length() / 2,
                            height / 2 - myFont.LineSpacing / 2 - 50),
                        Color.Black);
                }
                else
                {
                    _spriteBatch.DrawString(myFont, "PRESS ENTER TO START",
                   new Vector2(width / 2 - myFont.MeasureString("PRESS ENTER TO START").Length() / 2,
                       height / 2 - myFont.LineSpacing / 2 - 100), Color.Black);
                }
            }

            if (_game.GameState == GameState.GameOver)
            {
                string restart = "PRESS SPACE BAR TO RESTART GAME";
                string won = "- " + (gotPoint == 1 ? "RANDY" : "DAVID") + " WON -";

                //Press Space bar to end
                _spriteBatch.DrawString(myFont, restart,
                    new Vector2(width / 2 -
                        myFont.MeasureString("PRESS SPACE BAR TO RESTART GAME").Length() / 2,
                        height / 2 - myFont.LineSpacing / 2 - 100),
                    Color.Black);

                //Who won
                _spriteBatch.DrawString(myFont, won,
                    new Vector2(width / 2 - myFont.MeasureString(won).Length() / 2,
                        height / 2 - myFont.LineSpacing / 2 - 50),
                    Color.Black);
            }

            _spriteBatch.End();
        }

        /// <summary>
        /// Draw a rectangle
        /// </summary>
        /// <param name="rectangle">Contains x, y, width, height</param>
        /// <param name="color">Color of the rectangle</param>
        private void drawRectangle(Rectangle rectangle, Color color)
        {
            Texture2D texture = new Texture2D(_game.Graphics.GraphicsDevice, 1, 1);
            texture.SetData(new[] { color });
            _spriteBatch.Draw(texture, rectangle, color);
        }
    }
}