using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RBFinal.Components;
using RBFinal.Framework;
using RBFinal.Levels;
using RBFinal.player;
using RBFinal.towers;

namespace RBFinal.Screens
{
    class LastWave
    {
        public List<Tower> Towers;
        public int Gold;
        public int Health;
        public int Level;
        public int Wave;

        public LastWave(int level, int wave, List<Tower> towers, Player player)
        {
            this.Level = level;
            this.Wave = wave;
            this.Towers = towers;
            this.Gold = player.Gold;
            this.Health = player.Health;
        }
    }

    public class GameScreen : Screen
    {
        private Button btnPlay, btnPause;
        private Button btnFastForward, btnRegularSpeed;

        public Player Player;
        private List<Level> _levels;
        private int _currentLevel;
        private int _currentWave;
        private Menu _pauseMenu;
        private Menu _towerMenu;
        public bool fastForward;
        private LastWave _lastWave;

        public Tower Placing;

        private GameState _gameState;
        private GameState _lastGameState;

        public GameState GameState
        {
            get { return _gameState; }
            set
            {
                _lastGameState = _gameState;
                _gameState = value;
            }
        }

        public void startPlacement(Tower tower)
        {
            Placing = tower;
            tower.Selected = true;
        }


        public GameScreen(TowerDefense towerDefense) : base(towerDefense)
        {
            //Tower Menu
            _towerMenu = new Menu(towerDefense, new Vector2(620, 75), Color.Transparent, Color.Black)
            {
                SpriteFont = FontHandler.RegularFont,
                Size = { Height = 75 }
            };
            _towerMenu.addOption(
                new[]
                {
                    "--- Orb Tower ---", "Shoots Orbs at the enemies", "Fire Rate: Medium", "Damage: Medium",
                    "Cost: " + OrbTower.cost
                }, SpriteHandler.TowerOrb,
                (button, state) => startPlacement(new OrbTower(_levels[_currentLevel])));

            _towerMenu.addOption(
                new[]
                {
                    "--- Glow Tower ---", "Attacks most enemies ", "within range", "Fire Rate: Slow", "Damage:Low",
                    "Cost: " + GlowTower.cost
                }, SpriteHandler.TowerGlow,
                (button, state) => startPlacement(new GlowTower(_levels[_currentLevel])));

            //Pause Menu
            _pauseMenu = new Menu(towerDefense,
                Vector2.Zero,
                Color.WhiteSmoke, Color.Black);
            _pauseMenu.Location = new Vector2(Layout.Middle.X - _pauseMenu.Size.Width / 2,
                Layout.Middle.Y - _pauseMenu.Size.Height * 4 / 2);
            _pauseMenu.addOption("Resume", (button, state) =>
            {
                if (LastMouseState.LeftButton != ButtonState.Pressed) GameState = _lastGameState;
            });
            _pauseMenu.addOption("How To Play", (button, state) =>
            {
                if (LastMouseState.LeftButton != ButtonState.Pressed)
                    ScreenHandler.showScreen(this, ScreenHandler.HowToScreen);
            });
            _pauseMenu.addOption("Exit", (button, state) =>
            {
                if (LastMouseState.LeftButton != ButtonState.Pressed)
                {
                    showNotification("Information",
                        new[]
                        {
                            "You've left in the middle of a game,", "so we'll save where you were, so that",
                            "you can continue next time."
                        }, () => { ScreenHandler.showScreen(this, ScreenHandler.MainScreen); });
                }
            });

            btnPause = new Button(towerDefense, new Rectangle(0, 0, 32, 32), SpriteHandler.BtnPause,
                (b, m) =>
                {
                    if (LastMouseState.LeftButton != ButtonState.Pressed)
                        GameState = GameState.WaveNotStarted;
                }, null);
            btnPlay = new Button(towerDefense, new Rectangle(0, 0, 32, 32), SpriteHandler.BtnPlay,
                (b, m) =>
                {
                    if (LastMouseState.LeftButton != ButtonState.Pressed)
                        GameState = GameState.Playing;
                }, null);

            btnFastForward = new Button(towerDefense, new Rectangle(50, 0, 32, 32), SpriteHandler.BtnFastForward,
                (b, m) =>
                {
                    if (LastMouseState.LeftButton != ButtonState.Pressed)
                        fastForward = true;
                }, null);
            btnRegularSpeed = new Button(towerDefense, new Rectangle(50, 0, 32, 32), SpriteHandler.BtnRegularSpeed,
                (b, m) =>
                {
                    if (LastMouseState.LeftButton != ButtonState.Pressed)
                        fastForward = false;
                }, null);
        }


        public override void Update(GameTime gameTime)
        {
            Level level = getLevel();
            Wave wave = null;


            if (level != null)
            {
                //If the level just started, init the wave
                if (_currentWave == -1)
                {
                    _currentWave = 0;
                    wave = level.Waves[_currentWave];
                    wave.OnStart?.Invoke(wave);
                }
                else
                    wave = getWave();

                if (GameState == GameState.WaveNotStarted || GameState == GameState.Playing)
                {
                    if (wave != null)
                    {
                        level.Update(gameTime);
                        if (fastForward)
                            level.Update(gameTime);
                    }

                    //Side Menu controls
                    _towerMenu.Update(gameTime);
                    if (Placing != null)
                    {
                        //Drag the new tower
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            Placing.Location = new Vector2(Mouse.GetState().Position.X,
                                Mouse.GetState().Position.Y);
                            Placing.RadiusColor = level.canSet(Placing.Location, Placing) &&
                                                        new Rectangle(0, 32, 600, 448).Contains(
                                                            Mouse.GetState().Position)
                                ? Color.White
                                : Color.Red;
                        }
                        //Try and place the new tower
                        else if (Mouse.GetState().LeftButton == ButtonState.Released)
                        {
                            if (level.canSet(Placing.Location, Placing) &&
                                new Rectangle(0, 32, 600, 448).Contains(Mouse.GetState().Position))
                            {
                                level.setTower(Placing.Location, Placing);
                                Placing.RadiusColor = Color.White;
                                Player.addReward(-Placing.Cost);
                                Placing.Selected = false;
                                Placing = null;
                            }
                            else
                            {
                                Placing.RadiusColor = Color.White;
                                Placing = null;
                            }
                        }
                    }

                    //Check if a tower was clicked
                    else if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        foreach (var t in level.Towers)
                            t.Selected = t.getRectangle().Contains(Mouse.GetState().Position);
                    }
                }
                if (GameState == GameState.Playing)
                {
                    btnPause.Update(gameTime);
                    if (fastForward)
                        btnRegularSpeed.Update(gameTime);
                    else
                        btnFastForward.Update(gameTime);

                    //If the current wave isn't null, and it is done,
                    if (wave != null && wave.isDone())
                    {
                        GameState = GameState.WaveOver;
                    }
                }
                else if (GameState == GameState.WaveNotStarted)
                {
                    btnPlay.Update(gameTime);
                }
                else if (GameState == GameState.LevelOver)
                {
                    //Go to the next level
                    _currentLevel++;
                    _currentWave = -1;
                    Player = new Player(this);

                    //If we're now done all the levels
                    if (_currentLevel >= _levels.Count)
                    {
                        showNotification("You Win!", new[] { "You've beat all the levels!" },
                            () =>
                            {
                                ScreenHandler.showScreen(this, ScreenHandler.MainScreen);
                                setupLevels();
                                _currentLevel = 0;
                                _currentWave = -1;
                                setupLevels();
                                _lastWave = null;
                                Player = new Player(this);
                            });
                    }
                    else
                    {
                        GameState = GameState.WaveNotStarted;
                        _lastWave = new LastWave(_currentLevel, _currentWave, getLevel().Towers, Player);
                    }
                }
                else if (GameState == GameState.WaveOver)
                {
                    getWave()?.OnEnd?.Invoke(getWave());
                    //Go to the next wave
                    _currentWave++;
                    foreach (var t in level.Towers)
                        t.reset();

                    _lastWave = new LastWave(_currentLevel, _currentWave, getLevel().Towers, Player);

                    //If we're now done all the levels
                    if (_currentWave >= level.Waves.Count)
                    {
                        showNotification("You Win!", new[] { "You've beat all the Waves!" },
                            () => { GameState = GameState.LevelOver; });
                    }
                    else
                    {
                        SoundHandler.sfDone.Play();
                        GameState = GameState.WaveNotStarted;
                        getWave().OnStart?.Invoke(getWave());
                    }
                }
                else if (GameState == GameState.GameOver)
                {
                    showNotification("You Lost!", new[] { "You've ran out of lives!", "Good luck next time!" },
                        () =>
                        {
                            setupLevels();
                            _currentLevel = 0;
                            _currentWave = -1;
                            setupLevels();
                            _lastWave = null;
                            Player = new Player(this);
                            ScreenHandler.showScreen(this, ScreenHandler.MainScreen);
                        });
                }

                if (GameState == GameState.Paused)
                {
                    _pauseMenu.Update(gameTime);
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape) &&
                        !LastKeyBoardState.IsKeyDown(Keys.Escape))
                        GameState = _lastGameState;
                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape) &&
                        !LastKeyBoardState.IsKeyDown(Keys.Escape))
                        GameState = GameState.Paused;
                }
            }
            base.Update(gameTime);
        }

        public override void onShow()
        {
            GameState = GameState.WaveNotStarted;

            if (ScreenHandler.LastScreen != ScreenHandler.HowToScreen)
            {
                SoundHandler.sfDone.Play();
                _currentLevel = 0;
                _currentWave = -1;
                setupLevels();
                Player = new Player(this);

                if (_lastWave != null)
                {
                    _currentLevel = _lastWave.Level;
                    _currentWave = _lastWave.Wave;
                    getLevel().Towers = new List<Tower>(_lastWave.Towers);
                    Player.Gold = _lastWave.Gold;
                    Player.Health = _lastWave.Health;

                    getWave()?.OnStart?.Invoke(getWave());
                }
            }
        }

        public void setupLevels()
        {
            _currentLevel = 0;
            Level1 lvl1 = new Level1(TowerDefense, this);
            Level2 lvl2 = new Level2(TowerDefense, this);
            _levels = new List<Level> { lvl1, lvl2 };
        }

        public override void onHide()
        {
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteHandler.BkgGame.draw(SpriteBatch, Vector2.Zero);
            _towerMenu.Draw(gameTime);
            SpriteBatch.DrawString(FontHandler.TitleFont, "Towers", new Vector2(660, 25), Color.White);

            Level level = getLevel();
            Wave wave = null;
            if (level != null)
                wave = getWave();

            if (wave != null)
            {
                level.Draw(gameTime);

                string line = "Level: " + (_currentLevel + 1) + " - Wave: " + (_currentWave + 1);
                SpriteBatch.DrawString(FontHandler.TitleFont, line,
                    Utils.centerText(FontHandler.TitleFont, line,
                        new Rectangle(0, 0, 600, 30)),
                    Color.Black);
            }
            if (GameState == GameState.Playing)
            {
                btnPause.Draw(gameTime);

                if (fastForward)
                    btnRegularSpeed.Draw(gameTime);
                else
                    btnFastForward.Draw(gameTime);
            }
            else if (GameState == GameState.WaveNotStarted)
            {
                btnPlay.Draw(gameTime);
            }
            else if (GameState == GameState.Paused)
            {
                Drawer.drawRectangle(new Rectangle(0, 0, (int)Layout.Size.X, (int)Layout.Size.Y), Color.DimGray * .9f);

                _pauseMenu.Draw(gameTime);
            }
            base.Draw(gameTime);

            Player.Draw(gameTime);
            Placing?.Draw(gameTime);
            TowerDefense.SpriteBatch.End();
        }

        public Level getLevel()
        {
            //Get the current level
            return _currentLevel < _levels.Count ? _levels[_currentLevel] : null;
        }

        public Wave getWave()
        {
            //Get the current wave
            return _currentWave < getLevel().Waves.Count && _currentWave >= 0
                ? getLevel()?.Waves[_currentWave]
                : null;
        }
    }
}