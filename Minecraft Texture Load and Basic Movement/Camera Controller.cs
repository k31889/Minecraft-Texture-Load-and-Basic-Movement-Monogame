using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Minecraft_Texture_Load_and_Basic_Movement
{
    class Camera_Controller
    {
        private Matrix View;
        private Matrix Projection;

        private Vector3 cameraPosition;
        private Vector3 cameraDirection;
        private Vector3 cameraUp;

        GraphicsDeviceManager graphics;

        private float movementSpeed;
        private float Sensitivity;

        MouseState prevMouseState;

        public Matrix GetView()
        {
            return View;
        }
        public Matrix GetProjection()
        {
            return Projection;
        }

        public Camera_Controller(float FOV, float aspectRatio, float nearPlaneDistance, float farPlaneDistance, Vector3 StartPos, Vector3 StartFocus, float MoveSpeed, float Sens, GraphicsDeviceManager graphicsDeviceManager)
        {
            cameraPosition = StartPos;
            cameraDirection = StartFocus - StartPos;
            cameraDirection.Normalize();
            cameraUp = Vector3.UnitY;
            CreateLookAt();

            graphics = graphicsDeviceManager;

            movementSpeed = MoveSpeed;
            Sensitivity = Sens;

            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, graphics.PreferredBackBufferWidth / graphics.PreferredBackBufferHeight, 1, 100);
        }
        
        public void Initialize()
        {
            Mouse.SetPosition(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);

            prevMouseState = Mouse.GetState();
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                cameraPosition += cameraDirection * movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                cameraPosition -= cameraDirection * movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                cameraPosition += Vector3.Cross(cameraUp, cameraDirection) * movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                cameraPosition -= Vector3.Cross(cameraUp, cameraDirection) * movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }             
          

            // Rotation on X axis
            cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(cameraUp, (-MathHelper.PiOver4 / 100) * (Mouse.GetState().X - prevMouseState.X) * Sensitivity));

            // Rotation on Y axis
            cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(Vector3.Cross(cameraUp, cameraDirection), (MathHelper.PiOver4 / 100) * (Mouse.GetState().Y - prevMouseState.Y) * Sensitivity));

            //reset Y axis movement so player only moves horizontally
            cameraPosition.Y = 0;

            // Reset prevMouseState and Mouse Position
            Initialize();

            CreateLookAt();
        }

        private void CreateLookAt()
        {
            View = Matrix.CreateLookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
        }
    }
}
