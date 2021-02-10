using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Minecraft_Texture_Load_and_Basic_Movement
{
    //This method of texture importing works by exporting a model with unwrapped UVs and an applied texture from Blender (current version 2.9) as an FBX file
    class TextureHandler
    {
        GraphicsDevice graphicsDevice;

        //The width of each tile in your texture atlas (Minecraft has 16x16 by default)
        private int TextureWidth = 16;
        private int TextureHeight = 16;

        private Texture2D TextureAtlas;
        private String AtlasName;

        private Model Cube;
        private string ModelName;

        private Texture2D BlockTexture;

        //Constructor which is passed the current graphics device, the file name of the TextureAtlas and the file name of the model
        public TextureHandler(GraphicsDevice device, string aName, string mName)
        {
            AtlasName = aName;
            ModelName = mName;
            graphicsDevice = device;
        }

        //Loads Content from Content library, passed the current Content Manager
        public void LoadTextures(ContentManager Content)
        {
            TextureAtlas = Content.Load<Texture2D>(AtlasName);
            Cube = Content.Load<Model>(ModelName);
        }

        //Converts Texture Atlas to single tile texture
        private Texture2D GetTexture(int TexturePosX, int TexturePosY)
        {
            Rectangle sourceRectangle = new Rectangle(TexturePosX, TexturePosY, TextureWidth, TextureHeight);            //Gets and stores a single texture from the texture library
            Texture2D newTexture = new Texture2D(graphicsDevice, sourceRectangle.Width, sourceRectangle.Height);         //Create new Texture2D with correct width and Height
            Color[] data = new Color[sourceRectangle.Width * sourceRectangle.Height];                                    //Creates an array to store colour data with the same dimensions as the texture
            TextureAtlas.GetData(0, sourceRectangle, data, 0, data.Length);                                              //Gets colour data from the Texture Atlas
            newTexture.SetData(data);                                                                                    //Sets new colour data to the new texture

            BlockTexture = newTexture;
            return BlockTexture;
        }

        public void DrawCube(Matrix world, Matrix view, Matrix projection, int TexturePosX, int TexturePosY)
        {
            graphicsDevice.SamplerStates[0] = SamplerState.PointWrap;                                                   //Removes issues where linear interpolation make pixel textures blurry

            foreach (ModelMesh mesh in Cube.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                    effect.Texture = GetTexture(TexturePosX, TexturePosY);
                }
                mesh.Draw();
            }
        }

    }
}
