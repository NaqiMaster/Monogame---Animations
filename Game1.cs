using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Monogame___Animations
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        enum Screen
        {
            Intro,
            TribbleYard,
            EndScreen
        }
        Screen screen;
        MouseState mouseState;
        SpriteFont intro, end;

        SoundEffect tribbleSound, backgroundMusic;
        SoundEffectInstance backgroundMusicInstance;
        Random generator = new Random();
        Rectangle window;
        Texture2D greyTribbleTexture, creamTribbleTexture, orangeTribbleTexture,brownTribbleTexture, windowTexture,
            seaBackground, spaceBackground, tribbleIntroTexture, gameOver;
        Rectangle greyTribbleRect, creamTribbleRect, orangeTribbleRect, brownTribbleRect;
        Vector2 greyTribbleSpeed, creamTribbleSpeed, orangeTribbleSpeed, brownTribbleSpeed;
        int bounces;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screen = Screen.Intro;

            window = new Rectangle(0,0,800,600);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            bounces = 0;

            greyTribbleRect = new Rectangle(generator.Next(window.Width - 100), generator.Next(window.Height - 100), 100, 100);
            greyTribbleSpeed = new Vector2(5, 5);

            creamTribbleRect = new Rectangle(generator.Next(window.Width - 100), generator.Next(window.Height - 100),100, 100);
            creamTribbleSpeed = new Vector2(5, 0);

            orangeTribbleRect = new Rectangle(generator.Next(window.Width - 100), generator.Next(window.Height - 100), 100, 100);
            orangeTribbleSpeed = new Vector2(0, 5);

            brownTribbleRect = new Rectangle(generator.Next(window.Width - 100), generator.Next(window.Height - 100), 100,100);
            brownTribbleSpeed = new Vector2(1, 5);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            greyTribbleTexture = Content.Load<Texture2D>("tribbleGrey");
            creamTribbleTexture = Content.Load<Texture2D>("tribbleCream");
            orangeTribbleTexture = Content.Load<Texture2D>("tribbleOrange");
            brownTribbleTexture = Content.Load<Texture2D>("tribbleBrown");
            windowTexture = Content.Load<Texture2D>("tribbleGalaxy");
            seaBackground = Content.Load<Texture2D>("tribbleUnderwater");
            spaceBackground = Content.Load<Texture2D>("tribbleGalaxy");
            tribbleSound = Content.Load<SoundEffect>("tribble_coo");
            tribbleIntroTexture = Content.Load<Texture2D>("tribbleIntro");
            intro = Content.Load<SpriteFont>("intro");
            end = Content.Load<SpriteFont>("End");
            gameOver = Content.Load<Texture2D>("game-over");
            backgroundMusic = Content.Load<SoundEffect>("Beethoven");
            backgroundMusicInstance = backgroundMusic.CreateInstance();
        }

        protected override void Update(GameTime gameTime)
        {

            mouseState = Mouse.GetState();

            if (bounces == 50)
            {
                screen = Screen.EndScreen;
            }

            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    screen = Screen.TribbleYard;

                backgroundMusicInstance.Play();

            }
            else if (screen == Screen.TribbleYard)
            {
                backgroundMusicInstance.Pause();
                greyTribbleRect.X += (int)greyTribbleSpeed.X;
                if (greyTribbleRect.Right > window.Width || greyTribbleRect.Left < 0)
                {
                    greyTribbleSpeed.X *= -1;
                    windowTexture = seaBackground;
                    tribbleSound.Play();
                    bounces += 1;
                }

                greyTribbleRect.Y += (int)greyTribbleSpeed.Y;
                if (greyTribbleRect.Top < 0 || greyTribbleRect.Bottom > window.Height)
                {
                    greyTribbleSpeed.Y *= -1;
                    windowTexture = spaceBackground;
                    tribbleSound.Play();
                    bounces += 1;
                }


                creamTribbleRect.X += (int)creamTribbleSpeed.X;
                creamTribbleRect.Y += (int)creamTribbleSpeed.Y;

                if (creamTribbleRect.Right > window.Width || creamTribbleRect.Left < 0)
                {
                    creamTribbleSpeed.X *= -1;
                    windowTexture = seaBackground;
                    tribbleSound.Play();
                    bounces += 1;
                }


                orangeTribbleRect.X += (int)orangeTribbleSpeed.X;
                orangeTribbleRect.Y += (int)orangeTribbleSpeed.Y;


                if (orangeTribbleRect.Top < 0 || orangeTribbleRect.Bottom > window.Height)
                {
                    orangeTribbleSpeed.Y *= -1;
                    windowTexture = spaceBackground;
                    tribbleSound.Play();
                    bounces += 1;
                }


                brownTribbleRect.X += (int)brownTribbleSpeed.X;
                brownTribbleRect.Y += (int)brownTribbleSpeed.Y;

                if (brownTribbleRect.Right > window.Width || brownTribbleRect.Left < 0)
                {
                    brownTribbleSpeed.X *= -1;
                    windowTexture = seaBackground;
                    tribbleSound.Play();
                    bounces += 1;
                }
                if (brownTribbleRect.Top < 0 || brownTribbleRect.Bottom > window.Height)
                {
                    brownTribbleSpeed.Y *= -1;
                    windowTexture = spaceBackground;
                    tribbleSound.Play();
                    bounces += 1;
                }
            }
            else if (screen == Screen.EndScreen)
            {
                backgroundMusicInstance.Play();
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Red);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(tribbleIntroTexture, new Rectangle(0, 0, 800, 600), Color.White);
                _spriteBatch.DrawString(intro, "Do you want to see the TRIBBLES?", new Vector2(450, 10), Color.Black);
                _spriteBatch.DrawString(intro, "LEFT CLICK to see them!", new Vector2(470, 30), Color.Black);

            }
            else if (screen == Screen.TribbleYard)
            {
                _spriteBatch.Draw(windowTexture, window, Color.White);
                _spriteBatch.Draw(greyTribbleTexture, greyTribbleRect, Color.White);
                _spriteBatch.Draw(creamTribbleTexture, creamTribbleRect, Color.White);
                _spriteBatch.Draw(orangeTribbleTexture, orangeTribbleRect, Color.White);
                _spriteBatch.Draw(brownTribbleTexture, brownTribbleRect, Color.White);
            }
            else if (screen == Screen.EndScreen)
            {
                _spriteBatch.Draw(gameOver, window, Color.White);
                _spriteBatch.DrawString(end, "You got 50 bounces!", new Vector2(20, 50), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}