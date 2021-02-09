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

        private Texture2D CurrentTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Cam = new Camera(45, 800 / 400, 0.1f, 100f, new Vector3(0, 0, 10), new Vector3(0, 0, 0));
            Player1 = new Player(1f, Cam);
            Textures = new TextureHandler(GraphicsDevice, "Minecraft Texture Atlas", "Cube");
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Textures.LoadTextures(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            Player1.Movement();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            CurrentTexture = Textures.GetTexture(200, 200);
            Textures.DrawCube(world, Cam.GetView(), Cam.GetProjection(), CurrentTexture);
            base.Draw(gameTime);
        }
    }
}
