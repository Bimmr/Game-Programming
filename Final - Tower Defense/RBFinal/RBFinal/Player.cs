using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RBFinal.Screens;
using RBFinal.Framework;

namespace RBFinal.player
{
    public class Player
    {
        private const int STARTING_HEALTH = 100;
        private const int STARTING_GOLD = 275;

        public int Gold;
        public int Health;

        public GameScreen GameScreen;

        public Player(GameScreen gameScreen)
        {
            GameScreen = gameScreen;
            Health = STARTING_HEALTH;
            Gold = STARTING_GOLD;
        }
        

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime)
        {
            SpriteBatch sb = GameScreen.TowerDefense.SpriteBatch;
            SpriteHandler.SpDollar.draw(sb, new Vector2(450, -2), .9f);
            sb.DrawString(FontHandler.TitleFont, "" + Gold, new Vector2(479, 6),
                Color.Black);
            sb.DrawString(FontHandler.TitleFont, "" + Gold, new Vector2(480, 5),
                Color.LawnGreen);
            SpriteHandler.SpHeart.draw(sb, new Vector2(530, 0), .8f);
            sb.DrawString(FontHandler.TitleFont, "" + Health, new Vector2(559, 6),
                Color.Black);
            sb.DrawString(FontHandler.TitleFont, "" + Health, new Vector2(560, 5),
                Color.OrangeRed);
        }

        public void addReward(int reward)
        {
            Gold += reward;
        }

        public void doDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                GameScreen.GameState = GameState.GameOver;
            }
        }
    }
}