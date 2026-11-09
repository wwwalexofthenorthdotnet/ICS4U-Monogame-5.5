using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ICS4U_Monogame_5._5
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        MouseState mouseState;
        ParticleEngine particleEngine;
        List<Texture2D> particles;

        int MaxTTL = 60;

        bool test = false;

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

            particleEngine = new ParticleEngine(particles, Vector2.Zero, test);
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

            mouseState = Mouse.GetState();
            particleEngine.EmitterLocation = mouseState.Position.ToVector2();
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
