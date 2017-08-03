using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RBFinal.Components;
using RBFinal.Framework;

namespace RBFinal.Screens
{
    public class AboutScreen : Screen
    {

        private Button backButton;


        public AboutScreen(TowerDefense towerDefense) : base(towerDefense)
        {
            //Create a rectangle for the button
            Rectangle buttonSize = new Rectangle((int) Layout.Middle.X  - SpriteHandler.Btn.getWidth() / 2,
                (int) Layout.Size.Y - SpriteHandler.Btn.getHeight() - 10 - SpriteHandler.Btn.getHeight(),
                SpriteHandler.Btn.getWidth(), SpriteHandler.Btn.getHeight());


            //Create an action that will draw a gray rectangle over the button
            Action<Button, MouseState> draw =
                (button, state) => { Drawer.drawRectangle(buttonSize, Color.White * .4f); };

            backButton = new Button(towerDefense,
                    buttonSize,
                    SpriteHandler.Btn, FontHandler.MenuFont, "Back", Color.White,
                    //On Click Perform following code
                    (b, m) =>
                    {
                        if (LastMouseState.LeftButton != ButtonState.Pressed)
                        {
                            hide();
                            ScreenHandler.showScreen(this, ScreenHandler.MainScreen);
                        }
                    },
                    //On Hover Perform the following code
                    (b, s) =>
                    {
                        //Set the Draw action to draw the rectangle
                        b.AfterDraw = (b2, s2) => { draw(b, s); };
                    })
                {
                    //When not hoviering, set the Action of draw to null
                    OnNotHover = (b, s) =>
                    {
                        if (b.AfterDraw != null)
                            b.AfterDraw = null;
                    }
                }
                ;
        }

        public override void onHide()
        {
        }

        public override void onShow()
        {
        }


        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteHandler.BkgAbout.draw(SpriteBatch, new Vector2(0, 0));

            backButton.Draw(gameTime);
            base.Draw(gameTime);

            SpriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            backButton.Update(gameTime);
            base.Update(gameTime);
        }
    }
}