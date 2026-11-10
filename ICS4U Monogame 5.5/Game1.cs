using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Xml.Resolvers;

namespace ICS4U_Monogame_5._5
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        MouseState mouseState;
        MouseState prevMouseState;
        KeyboardState keyboardState;
        KeyboardState prevKeyboardState;
        Rectangle window;
        ParticleEngine particleEngine;
        List<Texture2D> particles;
        bool randomizeColor = false;
        bool fadeOut = false;

        bool doGrow = false;
        bool doShrink = false;
        bool doRect = false;

        int scroll;

        Rectangle spawnRect;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            window = new Rectangle(0, 0, 800, 600);

            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;

            _graphics.ApplyChanges();

            particles = new List<Texture2D>();

            spawnRect = new Rectangle(0, 0, window.Width, window.Height);

            base.Initialize();

            particleEngine = new ParticleEngine(particles, Vector2.Zero, false);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            particles.Add(Content.Load<Texture2D>("images/circle"));
            particles.Add(Content.Load<Texture2D>("images/star"));
            particles.Add(Content.Load<Texture2D>("images/diamond"));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
            prevKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            // COLOUR CHANGER

            if (keyboardState.IsKeyDown(Keys.D1) && prevKeyboardState != keyboardState)
                particleEngine.Color = Color.Blue;
            else if (keyboardState.IsKeyDown(Keys.D2) && prevKeyboardState != keyboardState)
                particleEngine.Color = Color.Red;
            else if (keyboardState.IsKeyDown(Keys.D3) && prevKeyboardState != keyboardState)
                particleEngine.Color = Color.Purple;
            else if (keyboardState.IsKeyDown(Keys.D4) && prevKeyboardState != keyboardState)
                particleEngine.Color = Color.Green;
            else if (keyboardState.IsKeyDown(Keys.D5) && prevKeyboardState != keyboardState)
                particleEngine.Color = Color.Teal;
            else if (keyboardState.IsKeyDown(Keys.D6) && prevKeyboardState != keyboardState)
                particleEngine.Color = Color.HotPink;
            else if (keyboardState.IsKeyDown(Keys.D7) && prevKeyboardState != keyboardState)
                particleEngine.Color = Color.Honeydew;
            else if (keyboardState.IsKeyDown(Keys.D8) && prevKeyboardState != keyboardState)
                particleEngine.Color = Color.Yellow;
            else if (keyboardState.IsKeyDown(Keys.D0) && prevKeyboardState != keyboardState)
                particleEngine.Color = Color.White;

            // RANDOM COLOUR TOGGLE

            if (keyboardState.IsKeyDown(Keys.R) && prevKeyboardState != keyboardState && !randomizeColor)
            {
                particleEngine.RandomizeColor = true;
                randomizeColor = true;
            }
            else if (keyboardState.IsKeyDown(Keys.R) && prevKeyboardState != keyboardState && randomizeColor)
            {
                particleEngine.RandomizeColor = false;
                randomizeColor = false;
            }

            // SHIFT HOLD TTL

            scroll = mouseState.ScrollWheelValue - prevMouseState.ScrollWheelValue;
            
            if (scroll < 0 && keyboardState.IsKeyDown(Keys.LeftShift))
            {
                particleEngine.TTL += 1;
            }
            else if (scroll > 0 && keyboardState.IsKeyDown(Keys.LeftShift) && particleEngine.TTL > 0)
            {
                particleEngine.TTL -= 1;
            }

            // FADE TOGGLE

            if (keyboardState.IsKeyDown(Keys.F) && prevKeyboardState != keyboardState && !fadeOut)
            {
                particleEngine.FadeOut = true;
                fadeOut = true;
            }
            else if (keyboardState.IsKeyDown(Keys.F) && prevKeyboardState != keyboardState && fadeOut)
            {
                particleEngine.FadeOut = false;
                fadeOut = false;
            }

            // SIZE SHIFTER MAX

            if (scroll < 0 && keyboardState.IsKeyDown(Keys.M) && particleEngine.MinSize <= particleEngine.MaxSize)
            {
                particleEngine.MaxSize += 1;
            }
            else if (scroll > 0 && keyboardState.IsKeyDown(Keys.M) && particleEngine.MaxSize > 0 && particleEngine.MinSize < particleEngine.MaxSize)
            {
                particleEngine.MaxSize -= 1;
            }

            // SIZE SHIFTER MIN


            if (scroll < 0 && keyboardState.IsKeyDown(Keys.N) && particleEngine.MinSize < particleEngine.MaxSize)
            {
                particleEngine.MinSize += 1;
            }
            else if (scroll > 0 && keyboardState.IsKeyDown(Keys.N) && particleEngine.MinSize > 0 && particleEngine.MinSize < particleEngine.MaxSize)
            {
                particleEngine.MinSize -= 1;
            }

            // Angular Velocity Shifter

            if (scroll < 0 && keyboardState.IsKeyDown(Keys.LeftControl) && particleEngine.AngularVelocity < particleEngine.AngularVelocity)
            {
                particleEngine.AngularVelocity += 1;
            }
            else if (scroll > 0 && keyboardState.IsKeyDown(Keys.LeftControl) && particleEngine.AngularVelocity > 0)
            {
                particleEngine.AngularVelocity -= 1;
            }
            
            
            // GROW TOGGLE

            if (keyboardState.IsKeyDown(Keys.G) && prevKeyboardState != keyboardState && !doGrow)
            {
                particleEngine.Grow = true;
                doGrow = true;

                doShrink = false;
            }
            else if (keyboardState.IsKeyDown(Keys.G) && prevKeyboardState != keyboardState && doGrow)
            {
                particleEngine.Grow = false;
                doGrow = false;
            }

            if (doGrow)
                particleEngine.Grow = false;


            // SHRINK TOGGLE

            if (keyboardState.IsKeyDown(Keys.S) && prevKeyboardState != keyboardState && !doShrink)
            {
                particleEngine.Shrink = true;
                doShrink = true;

                doGrow = false;
            }
            else if (keyboardState.IsKeyDown(Keys.S) && prevKeyboardState != keyboardState && doShrink)
            {
                particleEngine.Shrink = false;
                doShrink = false;
            }

            // Rect Toggle

            if (keyboardState.IsKeyDown(Keys.P) && prevKeyboardState != keyboardState && !doRect)
            {
                particleEngine.DoRect = true;
                doRect = true;
            }
            else if (keyboardState.IsKeyDown(Keys.P) && prevKeyboardState != keyboardState && doRect)
            {
                particleEngine.DoRect = false;
                doRect = false;
            }

            particleEngine.EmitterLocation = mouseState.Position.ToVector2();
            particleEngine.Speed = 1f;
            particleEngine.Angle = 180f;
            particleEngine.SpawnRect = spawnRect;
            particleEngine.Update();

            if (mouseState.LeftButton == ButtonState.Pressed)
                particleEngine.Enabled = true;
            else
                particleEngine.Enabled = false;

           base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            particleEngine.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
