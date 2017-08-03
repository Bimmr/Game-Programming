using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RBFinal.Components;
using RBFinal.Framework;

namespace RBFinal.Screens
{
    public class MainScreen : Screen
    {
        private static int scrollingSpeed = 2;
        private int scrollDelay = 2;
        private int scrollCount;
        private Menu _menu;
        private ScrollingBackground _scrollingBackground;

        public MainScreen(TowerDefense towerDefense) : base(towerDefense)
        {
            _menu = new Menu(towerDefense, Vector2.Zero, Color.DimGray, Color.White);
            _menu.Location = new Vector2(Layout.Middle.X - _menu.Size.Width / 2,
                Layout.Middle.Y - _menu.Size.Height * 4 / 2);
            _menu.addOption("Play", (button, state) =>
            {
                if (LastMouseState.LeftButton != ButtonState.Pressed)
                    ScreenHandler.showScreen(this, ScreenHandler.GameScreen);
            });
            _menu.addOption("How To Play", (button, state) =>
            {
                if (LastMouseState.LeftButton != ButtonState.Pressed)
                    ScreenHandler.showScreen(this, ScreenHandler.HowToScreen);
            });
            _menu.addOption("About", (button, state) =>
            {
                if (LastMouseState.LeftButton != ButtonState.Pressed)
                    ScreenHandler.showScreen(this, ScreenHandler.AboutScreen);
            });
            _menu.addOption("Quit", (button, state) =>
            {
                if (LastMouseState.LeftButton != ButtonState.Pressed)
                    TowerDefense.Exit();
            });
            _scrollingBackground = new ScrollingBackground(SpriteHandler.BkgMain, Vector2.Zero, scrollingSpeed,
                Direction.Left);
        }

        public override void onHide()
        {
        }

        public override void onShow()
        {
            _menu.Selected = 0;
        }


        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            _scrollingBackground.draw(SpriteBatch);
            SpriteHandler.SpTitle.draw(SpriteBatch,
                new Vector2(Layout.Middle.X - SpriteHandler.SpTitle.getWidth() / 2, 25));
            _menu.Draw(gameTime);
            base.Draw(gameTime);

            SpriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            scrollCount++;
            if (scrollCount >= scrollDelay)
                _scrollingBackground.move();

            _menu.Update(gameTime);
            base.Update(gameTime);
        }
    }
}