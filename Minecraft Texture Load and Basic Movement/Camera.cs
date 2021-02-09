using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        public void Move(float xChange, float yChange, float zChange)
        {
            Position.X += xChange;
            Position.Y += yChange;
            Position.Z += zChange;

            FocusPoint.X += xChange;
            FocusPoint.Y += yChange;
            FocusPoint.Z += zChange;

            View = Matrix.CreateLookAt(Position, FocusPoint, Vector3.UnitY);
        }

    }
}
