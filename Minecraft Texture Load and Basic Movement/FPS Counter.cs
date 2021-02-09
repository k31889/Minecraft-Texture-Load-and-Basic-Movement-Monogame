using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Minecraft_Texture_Load_and_Basic_Movement
{
    class FPS_Counter
    {

        private double frames = 0;
        private double updates = 0;
        private double elapsed = 0;
        private double last = 0;
        private double now = 0;
        public double msgFrequency = 1.0f;
        public string msg = "";

        /// <summary>
        /// The msgFrequency here is the reporting time to update the message.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            now = gameTime.TotalGameTime.TotalSeconds;
            elapsed = (double)(now - last);
            if (elapsed > msgFrequency)
            {
                msg = " Fps: " + Math.Round((frames / elapsed)).ToString();
                //Console.WriteLine(msg);
                elapsed = 0;
                frames = 0;
                updates = 0;
                last = now;
            }
            updates++;
        }

        public void DrawFps(SpriteBatch spriteBatch, SpriteFont font, Vector2 fpsDisplayPosition, Color fpsTextColor)
        {
            spriteBatch.DrawString(font, msg, fpsDisplayPosition, fpsTextColor);
            frames++;
        }
    }
}
