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
        ParticleEngine particleEngine;
        List<Texture2D> particles;
        bool randomizeColor = false;
        bool fadeOut = false;

        int scroll;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            particles = new List<Texture2D>();

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

            if (scroll < 0 && keyboardState.IsKeyDown(Keys.M))
            {
                particleEngine.MaxSize += 1;
            }
            else if (scroll > 0 && keyboardState.IsKeyDown(Keys.M) && particleEngine.MaxSize > 0)
            {
                particleEngine.MaxSize -= 1;
            }

            // SIZE SHIFTER MIN


            if (scroll < 0 && keyboardState.IsKeyDown(Keys.N) && particleEngine.MinSize < particleEngine.MaxSize)
            {
                particleEngine.MinSize += 1;
            }
            else if (scroll > 0 && keyboardState.IsKeyDown(Keys.N) && particleEngine.MinSize > 0)
            {
                particleEngine.MinSize -= 1;
            }
            particleEngine.EmitterLocation = mouseState.Position.ToVector2();
            particleEngine.Speed = 1f;
            particleEngine.Angle = 180f;
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
