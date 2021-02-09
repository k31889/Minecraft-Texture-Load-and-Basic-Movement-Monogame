using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Minecraft_Texture_Load_and_Basic_Movement
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Matrix world = Matrix.CreateTranslation(new Vector3(0, 0, 0));

        private Player Player1;
        private TextureHandler Textures;
        private Camera Cam;
        private FPS_Counter fps;

        private SpriteFont font;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 400;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here        
            fps = new FPS_Counter();
            Cam = new Camera(45, _graphics.PreferredBackBufferWidth / _graphics.PreferredBackBufferHeight, 0.1f, 100f, new Vector3(0, 0, 10), new Vector3(0, 0, 0));
            Player1 = new Player(10f, Cam);
            Textures = new TextureHandler(GraphicsDevice, "Minecraft Texture Atlas", "Cube");

            this.TargetElapsedTime = TimeSpan.FromSeconds(1d / 240d);
            this.IsFixedTimeStep = true;
            _graphics.SynchronizeWithVerticalRetrace = false;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Textures.LoadTextures(Content);
            font = Content.Load<SpriteFont>("font");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            fps.Update(gameTime);
            Player1.Movement(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            for(int i = 0; i < 3; i++)
            {
                Textures.DrawCube(Matrix.CreateTranslation(i * 10, 0, 0), Cam.GetView(), Cam.GetProjection(), 304, 240);
            }
            _spriteBatch.Begin();
            fps.DrawFps(_spriteBatch, font, new Vector2(0f, 0f), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
