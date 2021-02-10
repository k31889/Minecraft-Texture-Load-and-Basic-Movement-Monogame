using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Minecraft_Texture_Load_and_Basic_Movement
{
    class Camera
    {
        private Matrix View;
        private Matrix Projection;
        private Vector3 Position;
        private Vector3 FocusPoint;
        public Matrix GetView()
        {
            return View;
        }
        public Matrix GetProjection()
        {
            return Projection;
        }

        public Camera(float FOV, float aspectRatio, float nearPlaneDistance, float farPlaneDistance, Vector3 StartPos, Vector3 StartFocus)
        {
            FocusPoint = StartFocus;
            Position = StartPos;
            View = Matrix.CreateLookAt(StartPos, StartFocus, Vector3.UnitY);
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(FOV), aspectRatio, nearPlaneDistance, farPlaneDistance);
        }

        public void Move(float xChange, float yChange, float zChange, GameTime gameTime)
        {
            Position.X += xChange * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position.Y += yChange * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position.Z += zChange * (float)gameTime.ElapsedGameTime.TotalSeconds;

            FocusPoint.X += xChange * (float)gameTime.ElapsedGameTime.TotalSeconds;
            FocusPoint.Y += yChange * (float)gameTime.ElapsedGameTime.TotalSeconds;
            FocusPoint.Z += zChange * (float)gameTime.ElapsedGameTime.TotalSeconds;

            View = Matrix.CreateLookAt(Position, FocusPoint, Vector3.UnitY);
        }

        public void Rotate(float xChange, float yChange, GameTime gameTime)
        {
            FocusPoint = Vector3.Transform(FocusPoint - Position, Matrix.CreateRotationY(xChange *(float)gameTime.ElapsedGameTime.TotalSeconds)) + Position;
            View = Matrix.CreateLookAt(Position, FocusPoint, Vector3.UnitY);

            //FocusPoint.X += xChange * (float)gameTime.ElapsedGameTime.TotalSeconds; ;
            //FocusPoint.Y += yChange * (float)gameTime.ElapsedGameTime.TotalSeconds; ;
        }
    }
}
