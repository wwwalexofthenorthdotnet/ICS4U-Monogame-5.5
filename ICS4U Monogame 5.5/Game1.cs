using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Resolvers;

namespace ICS4U_Monogame_5._5
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ResolutionManager _resolutionManager;
        MouseState mouseState;
        MouseState prevMouseState;
        KeyboardState keyboardState;
        KeyboardState prevKeyboardState;
        Rectangle window;
        ParticleEngine particleEngine;
        List<Texture2D> particles;

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

            _resolutionManager = new ResolutionManager(1280, 720);

            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            _graphics.IsFullScreen = false;

            _graphics.ApplyChanges();

            window = new Rectangle(0, 0, _resolutionManager.VirtualWidth, _resolutionManager.VirtualHeight);

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

            Vector2 physicalMousePosition = new Vector2(mouseState.X, mouseState.Y);
            Matrix resolutionMatrix = _resolutionManager.GetTransformationMatrix(GraphicsDevice);
            Matrix inverseMatrix = Matrix.Invert(resolutionMatrix);
            Vector2 virtualMousePosition = Vector2.Transform(physicalMousePosition, inverseMatrix);


            particleEngine.SpawnRect = spawnRect;


            // COLOUR CHANGER
            
            if (keyboardState.IsKeyDown(Keys.D1) && prevKeyboardState != keyboardState)
                particleEngine.BaseColor = Color.Blue;
            else if (keyboardState.IsKeyDown(Keys.D2) && prevKeyboardState != keyboardState)
                particleEngine.BaseColor = Color.Red;
            else if (keyboardState.IsKeyDown(Keys.D3) && prevKeyboardState != keyboardState)
                particleEngine.BaseColor = Color.Purple;
            else if (keyboardState.IsKeyDown(Keys.D4) && prevKeyboardState != keyboardState)
                particleEngine.BaseColor = Color.Green;
            else if (keyboardState.IsKeyDown(Keys.D5) && prevKeyboardState != keyboardState)
                particleEngine.BaseColor = Color.Teal;
            else if (keyboardState.IsKeyDown(Keys.D6) && prevKeyboardState != keyboardState)
                particleEngine.BaseColor = Color.HotPink;
            else if (keyboardState.IsKeyDown(Keys.D7) && prevKeyboardState != keyboardState)
                particleEngine.BaseColor = Color.Honeydew;
            else if (keyboardState.IsKeyDown(Keys.D8) && prevKeyboardState != keyboardState)
                particleEngine.BaseColor = Color.Yellow;
            else if (keyboardState.IsKeyDown(Keys.D0) && prevKeyboardState != keyboardState)
                particleEngine.BaseColor = Color.White;

            if (keyboardState.IsKeyDown(Keys.F1) && prevKeyboardState != keyboardState)
                particleEngine.SecondaryColor = Color.Blue;
            else if (keyboardState.IsKeyDown(Keys.F2) && prevKeyboardState != keyboardState)
                particleEngine.SecondaryColor = Color.Red;
            else if (keyboardState.IsKeyDown(Keys.F3) && prevKeyboardState != keyboardState)
                particleEngine.SecondaryColor = Color.Purple;
            else if (keyboardState.IsKeyDown(Keys.F4) && prevKeyboardState != keyboardState)
                particleEngine.SecondaryColor = Color.Green;
            else if (keyboardState.IsKeyDown(Keys.F5) && prevKeyboardState != keyboardState)
                particleEngine.SecondaryColor = Color.Teal;
            else if (keyboardState.IsKeyDown(Keys.F6) && prevKeyboardState != keyboardState)
                particleEngine.SecondaryColor = Color.HotPink;
            else if (keyboardState.IsKeyDown(Keys.F7) && prevKeyboardState != keyboardState)
                particleEngine.SecondaryColor = Color.Honeydew;
            else if (keyboardState.IsKeyDown(Keys.F8) && prevKeyboardState != keyboardState)
                particleEngine.SecondaryColor = Color.Yellow;
            else if (keyboardState.IsKeyDown(Keys.F12) && prevKeyboardState != keyboardState)
                particleEngine.SecondaryColor = Color.White;

            // RANDOM COLOUR TOGGLE

            if (keyboardState.IsKeyDown(Keys.R) && prevKeyboardState != keyboardState && !particleEngine.RandomizeColor)
            {
                particleEngine.RandomizeColor = true;
            }
            else if (keyboardState.IsKeyDown(Keys.R) && prevKeyboardState != keyboardState && particleEngine.RandomizeColor)
            {
                particleEngine.RandomizeColor = false;
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

            if (keyboardState.IsKeyDown(Keys.F) && prevKeyboardState != keyboardState && !particleEngine.FadeOut)
            {
                particleEngine.FadeOut = true;
            }
            else if (keyboardState.IsKeyDown(Keys.F) && prevKeyboardState != keyboardState && particleEngine.FadeOut)
            {
                particleEngine.FadeOut = false;
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

            if (scroll < 0 && keyboardState.IsKeyDown(Keys.LeftControl))
            {
                particleEngine.AngularVelocity += 1;
            }
            else if (scroll > 0 && keyboardState.IsKeyDown(Keys.LeftControl) && particleEngine.AngularVelocity > 0)
            {
                particleEngine.AngularVelocity -= 1;
            }


            // GROW TOGGLE

            if (keyboardState.IsKeyDown(Keys.G) && prevKeyboardState != keyboardState && !particleEngine.Grow)
            {
                particleEngine.Grow = true;

                particleEngine.Shrink = false;
            }
            else if (keyboardState.IsKeyDown(Keys.G) && prevKeyboardState != keyboardState && particleEngine.Grow)
            {
                particleEngine.Grow = false;
            }



            // SHRINK TOGGLE

            if (keyboardState.IsKeyDown(Keys.S) && prevKeyboardState != keyboardState && !particleEngine.Shrink)
            {
                particleEngine.Shrink = true;

                particleEngine.Grow = false;
            }
            else if (keyboardState.IsKeyDown(Keys.S) && prevKeyboardState != keyboardState && particleEngine.Shrink)
            {
                particleEngine.Shrink = false;
            }

            // Rect Toggle

            if (keyboardState.IsKeyDown(Keys.P) && prevKeyboardState != keyboardState && !particleEngine.DoRect)
            {
                particleEngine.DoRect = true;
            }
            else if (keyboardState.IsKeyDown(Keys.P) && prevKeyboardState != keyboardState && particleEngine.DoRect)
            {
                particleEngine.DoRect = false;
            }

            // Spread Angle Shifter

            if (scroll < 0 && keyboardState.IsKeyDown(Keys.LeftAlt))
            {
                particleEngine.SpreadAngle += 10;
            }
            else if (scroll > 0 && keyboardState.IsKeyDown(Keys.LeftAlt))
            {
                particleEngine.SpreadAngle -= 10;
            }

            // Direction Setter

            if (keyboardState.IsKeyDown(Keys.Up) && prevKeyboardState != keyboardState)
                particleEngine.Direction = new Vector2(0, -1);
            else if (keyboardState.IsKeyDown(Keys.Left) && prevKeyboardState != keyboardState)
                particleEngine.Direction = new Vector2(-1, 0);
            else if (keyboardState.IsKeyDown(Keys.Right) && prevKeyboardState != keyboardState)
                particleEngine.Direction = new Vector2(1, 0);
            else if (keyboardState.IsKeyDown(Keys.Down) && prevKeyboardState != keyboardState)
                particleEngine.Direction = new Vector2(0, 1);

            // Direction Toggle

            if (keyboardState.IsKeyDown(Keys.RightControl) && prevKeyboardState != keyboardState && !particleEngine.DoDirection)
            {
                particleEngine.DoDirection = true;
            }
            else if (keyboardState.IsKeyDown(Keys.RightControl) && prevKeyboardState != keyboardState && particleEngine.DoDirection)
            {
                particleEngine.DoDirection = false;
            }

            // Spread Angle Shifter

            if (scroll < 0 && keyboardState.IsKeyDown(Keys.RightAlt))
            {
                particleEngine.Speed += 1;
            }
            else if (scroll > 0 && keyboardState.IsKeyDown(Keys.RightAlt))
            {
                particleEngine.Speed -= 1;
            }

            // Gravity Toggle
            if (keyboardState.IsKeyDown(Keys.Y) && prevKeyboardState != keyboardState && !particleEngine.DoGravity)
            {
                particleEngine.DoGravity = true;
            }
            else if (keyboardState.IsKeyDown(Keys.Y) && prevKeyboardState != keyboardState && particleEngine.DoGravity)
            {
                particleEngine.DoGravity = false;
            }

            // Spread Angle 0

            if (keyboardState.IsKeyDown(Keys.L) && prevKeyboardState != keyboardState)
            {
                particleEngine.SpreadAngle = 0;
            }

            // Total Shifter

            // Spread Angle Shifter

            if (scroll < 0 && keyboardState.IsKeyDown(Keys.T))
            {
                particleEngine.Total += 1;
            }
            else if (scroll > 0 && keyboardState.IsKeyDown(Keys.T) && particleEngine.Total != 0)
            {
                particleEngine.Total -= 1;
            }

            if (keyboardState.IsKeyDown(Keys.NumPad0) && prevKeyboardState != keyboardState)
            {

                spawnRect = new Rectangle(0, 0, _resolutionManager.VirtualWidth, _resolutionManager.VirtualHeight);
                particleEngine.Enabled = false;
                particleEngine.Speed = 1f;

                particleEngine.Total = 10;

                particleEngine.RandomizeColor = false;
                particleEngine.BaseColor = Color.White;
                particleEngine.SecondaryColor = Color.White;
                particleEngine.TTL = 40;
                particleEngine.FadeOut = false;

                particleEngine.DoRect = false;
                particleEngine.Grow = false;
                particleEngine.Shrink = false;
                particleEngine.DoGravity = false;
                particleEngine.DoDirection = false;

                particleEngine.MaxSize = 10;
                particleEngine.MinSize = 5;

                particleEngine.AngularVelocity = 1f;

                particleEngine.SpreadAngle = 30f;

                particleEngine.Speed = 1;

                particleEngine.Direction = new Vector2(0, -1);


                particleEngine.Angle = 0;
            }

            if (keyboardState.IsKeyDown(Keys.NumPad1) && prevKeyboardState != keyboardState)
            {
                spawnRect = new Rectangle(0, 0, _resolutionManager.VirtualWidth, _resolutionManager.VirtualHeight / 4);

                particleEngine.Total = 10;

                particleEngine.DoRect = true;
                particleEngine.FadeOut = true;
                particleEngine.BaseColor = Color.Blue;
                particleEngine.SecondaryColor = Color.Blue;
                particleEngine.DoGravity = true;
                particleEngine.DoDirection = true;
                particleEngine.Direction = new Vector2(0, 1);
                particleEngine.TTL = 250;
                particleEngine.MaxSize = 5;
                particleEngine.MinSize = 1;

                particleEngine.SpreadAngle = 0;
                particleEngine.Speed = 5;


            }

            particleEngine.EmitterLocation = virtualMousePosition;
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
            Matrix resolutionMatrix = _resolutionManager.GetTransformationMatrix(GraphicsDevice);

            // TODO: Add your drawing code here

            _spriteBatch.Begin(transformMatrix: resolutionMatrix, samplerState: SamplerState.PointClamp );
            particleEngine.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
