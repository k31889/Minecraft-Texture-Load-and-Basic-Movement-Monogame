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

        private TextureHandler Textures;
        private Camera_Controller Cam;
        private FPS_Counter fps;

        private SpriteFont font;

        private RasterizerState rasterizerState;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;


            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here        
            fps = new FPS_Counter();
            Cam = new Camera_Controller(45, _graphics.PreferredBackBufferWidth / _graphics.PreferredBackBufferHeight, 0.1f, 100f, new Vector3(0, 0, 10), new Vector3(0, 0, 0), 10f, 0.1f, _graphics);
            Cam.Initialize();
            Textures = new TextureHandler(GraphicsDevice, "Minecraft Texture Atlas", "Cube");

            rasterizerState = new RasterizerState();

            _graphics.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            this.TargetElapsedTime = TimeSpan.FromSeconds(1d / 240d);
            //cap or uncap framerate
            this.IsFixedTimeStep = true;
            _graphics.GraphicsDevice.BlendState = BlendState.AlphaBlend;
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
            Cam.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            for(int i = -3; i < 2; i++)
            {
                for (int j = -3; j < 2; j++)
                {
                    _graphics.GraphicsDevice.DepthStencilState = DepthStencilState.Default;                                   //Gives render depth so that objects behind others aren't rendered on top
                    rasterizerState.CullMode = CullMode.CullClockwiseFace;
                    Textures.DrawCube(Matrix.CreateTranslation(i * 2, -4, j * 2), Cam.GetView(), Cam.GetProjection(), 304, 240);
                }
            }
            _spriteBatch.Begin();
            fps.DrawFps(_spriteBatch, font, new Vector2(0f, 0f), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
