using RBFinal.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RBFinal.Framework
{
    /// <summary>
    /// SpriteHandler
    /// </summary>
    public static class SpriteHandler
    {
        public static Sprite BtnPlay;
        public static Sprite BtnPause;
        public static Sprite BtnFastForward;
        public static Sprite BtnRegularSpeed;
        public static Sprite Btn;

        public static Sprite BkgMain;
        public static Sprite BkgGame;
        public static Sprite BkgHowTo;
        public static Sprite BkgAbout;

        public static Sprite BkgLevel1;
        public static Sprite BkgLevel2;

        public static Sprite ProjOrb;
        public static Sprite ProjGlow;

        public static Sprite TowerOrb;
        public static Sprite TowerGlow;

        public static Sprite SPCracked;
        public static Sprite SPRange;
        public static Sprite SpTitle;
        public static Sprite SpDollar;
        public static Sprite SpHeart;
        public static Sprite SpNotification;

        /// <summary>
        /// Setup the SpriteHandler
        /// </summary>
        /// <param name="towerDefense"></param>
        public static void setup(TowerDefense towerDefense)
        {
            Texture2D guiTex = towerDefense.Content.Load<Texture2D>("images/guitextures");
            Texture2D mainBackTex = towerDefense.Content.Load<Texture2D>("images/mainbackground");
            Texture2D gameBackTex = towerDefense.Content.Load<Texture2D>("images/gamebackground");
            Texture2D howToTex = towerDefense.Content.Load<Texture2D>("images/howtobackground");
            Texture2D aboutText = towerDefense.Content.Load<Texture2D>("images/aboutbackground");
            Texture2D projectileTex = towerDefense.Content.Load<Texture2D>("images/projectiletextures");
            Texture2D towerTex = towerDefense.Content.Load<Texture2D>("images/towertextures");
            Texture2D enemyTex = towerDefense.Content.Load<Texture2D>("images/enemytextures");

            Texture2D levelTex = towerDefense.Content.Load<Texture2D>("images/levelbackground");

            BtnPause = new Sprite(guiTex, new Rectangle(0, 0, 32, 32));
            BtnPlay = new Sprite(guiTex, new Rectangle(32, 0, 32, 32));
            BtnFastForward = new Sprite(guiTex, new Rectangle(128, 0, 32, 32));
            BtnRegularSpeed = new Sprite(guiTex, new Rectangle(160, 0, 32, 32));
            Btn = new Sprite(guiTex, new Rectangle(0, 32, 98, 32));

            BkgMain = new Sprite(mainBackTex, new Rectangle(0, 0, 800, 480));
            BkgGame = new Sprite(gameBackTex, new Rectangle(0, 0, 800, 480));
            BkgHowTo = new Sprite(howToTex, new Rectangle(0, 0, 800, 480));
            BkgAbout = new Sprite(aboutText, new Rectangle(0, 0, 800, 480));

            BkgLevel1 = new Sprite(levelTex, new Rectangle(0, 0, 600, 458));
            BkgLevel2 = new Sprite(levelTex, new Rectangle(0, 458, 600, 458));

            ProjOrb = new Sprite(projectileTex, new Rectangle(0, 0, 32, 32));
            ProjGlow = new Sprite(projectileTex, new Rectangle(32, 0, 32, 32));

            TowerOrb = new Sprite(towerTex, new Rectangle(0, 0, 32, 32));
            TowerGlow = new Sprite(towerTex, new Rectangle(32, 0, 32, 32));

            SpTitle = new Sprite(guiTex, new Rectangle(0, 224, 480, 64));
            SpHeart = new Sprite(guiTex, new Rectangle(96, 0, 32, 32));
            SpDollar = new Sprite(guiTex, new Rectangle(64, 0, 32, 32));
            SPRange = new Sprite(guiTex, new Rectangle(224, 0, 32, 32));
            SPCracked = new Sprite(enemyTex, new Rectangle(480, 0, 32, 32));
            SpNotification = new Sprite(guiTex, new Rectangle(0, 64, 384, 160));
        }
    }
}