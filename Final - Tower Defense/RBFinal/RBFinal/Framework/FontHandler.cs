using Microsoft.Xna.Framework.Graphics;

namespace RBFinal.Framework
{
    public static class FontHandler
    {
        public static SpriteFont MenuFont;
        public static SpriteFont TitleFont;
        public static SpriteFont TextFont;
        public static SpriteFont RegularFont;


        public static void setup(TowerDefense towerDefense)
        {
            MenuFont = towerDefense.Content.Load<SpriteFont>("fonts/menu");
            TitleFont = towerDefense.Content.Load<SpriteFont>("fonts/title");
            TextFont = towerDefense.Content.Load<SpriteFont>("fonts/text");
            RegularFont = towerDefense.Content.Load<SpriteFont>("fonts/regular");
        }
    }
}