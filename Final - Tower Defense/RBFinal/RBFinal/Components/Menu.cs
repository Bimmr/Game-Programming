using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RBFinal.Framework;

namespace RBFinal.Components
{
    public class Menu : DrawableGameComponent
    {
        public Rectangle Size = new Rectangle(0, 0, 200, 50);
        private const int spacing = 5;


        public Color ButtonColor = Color.Gray;
        public Color FontColor = Color.Black;

        public int Selected;
        public Vector2 Location;
        public List<Button> Buttons;

        public SpriteFont SpriteFont;

        private TowerDefense _towerDefense;


        public Menu(TowerDefense towerDefense, Vector2 location) : base(towerDefense)
        {
            _towerDefense = towerDefense;
            SpriteFont = FontHandler.MenuFont;
            Location = location;
            Buttons = new List<Button>();
        }

        public Menu(TowerDefense towerDefense, Vector2 location, Color buttonColor, Color fontColor)
            : this(towerDefense, location)
        {
            ButtonColor = buttonColor;
            FontColor = fontColor;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Button btn in Buttons)
                btn.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Button btn in Buttons)
                btn.Draw(gameTime);

            base.Draw(gameTime);
        }

        public void addOption(string text, Sprite sprite, Action<Button,MouseState>onClick)
        {
            addOption(new []{text}, sprite, onClick );
        }

        public void addOption(string[] text, Sprite sprite, Action<Button, MouseState> onClick)
        {
            int x = (int) Location.X;
            int y = (int) Location.Y + (Size.Height + spacing) * Buttons.Count;

            Rectangle rec = new Rectangle(x, y, Size.Width, Size.Height);

            Button btn = new Button(_towerDefense, rec, sprite, SpriteFont, text, FontColor, onClick, null);

            if (sprite != null)
                btn.CenterText = false;

            if (ButtonColor != Color.Transparent)
            {
                btn.BeforeDraw = (time, batch) =>
                {
                    Drawer.drawRectangle(new Rectangle(rec.X - 1, rec.Y - 1, rec.Width + 1, rec.Height + 1), Color.Black);
                    Drawer.drawRectangle(rec, ButtonColor);
                };
            }
            Action<Button, MouseState> hover =
                (button, state) =>
                {
                    button.AfterDraw = (time, batch) =>
                    {
                        if (ButtonColor != Color.Transparent)
                            Drawer.drawRectangle(rec, Color.White * .4f);
                        Selected = Buttons.Count;
                    };
                };
            Action<Button, MouseState> notHover =
                (button, state) =>
                {
                    if (button.AfterDraw != null) button.AfterDraw = null;
                    if (Selected == Buttons.Count)
                        Selected = -1;
                };

            btn.OnHover = hover;
            btn.OnNotHover = notHover;

            Buttons.Add(btn);
        }

        public void addOption(string text, Action<Button, MouseState> onClick)
        {
            addOption(text, null, onClick);
        }
    }
}