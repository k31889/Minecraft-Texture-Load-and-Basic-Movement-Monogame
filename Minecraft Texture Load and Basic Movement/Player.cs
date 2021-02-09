using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Minecraft_Texture_Load_and_Basic_Movement
{
    class Player
    {
        private float MovementSpeed;
        private Camera Cam;

        public Player(float mSpeed, Camera camera)
        {
            Cam = camera;
            MovementSpeed = mSpeed;
        }
        public void Movement()
        {
            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Right) || kstate.IsKeyDown(Keys.D))
            {
                Cam.Move(MovementSpeed, 0, 0);
            }
            if (kstate.IsKeyDown(Keys.Left) || kstate.IsKeyDown(Keys.A))
            {
                Cam.Move(-MovementSpeed, 0, 0);
            }
            if (kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.W))
            {
                Cam.Move(0, 0, -MovementSpeed);
            }
            if (kstate.IsKeyDown(Keys.Down) || kstate.IsKeyDown(Keys.S))
            {
                Cam.Move(0, 0, MovementSpeed);
            }
        }
    }
}
