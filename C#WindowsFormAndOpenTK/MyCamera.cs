using OpenTK;
using System;
using System.Diagnostics;

namespace C_WindowsFormAndOpenTK
{
    public enum MyDirection { FORWARD, BACKWARD, LEFT, RIGHT, UP, DOWN }

    public class MyCamera : MyGameObject
    {       
        private Vector3 _front = -Vector3.UnitZ;

        public Vector3 MyGetFront { get { return _front; } }

        public bool myIsFly { get; set; }

        private Vector3 _up = Vector3.UnitY;

        private Vector3 _right = Vector3.UnitX;

        private float _pitch;

        private float _yaw = -MathHelper.PiOver2;

        private float myRoll = 0;

        private float _fov = MathHelper.DegreesToRadians(60);

        private Matrix4 myMatrixView;

        public MyCamera(Vector3 position, float aspectRatio)
        {
            myPosition = position;
            AspectRatio = aspectRatio;
            myName = "Camera";
        }
        // This is simply the aspect ratio of the viewport, used for the projection matrix.
        public float AspectRatio { get; set; }

        public Vector3 Front => _front;

        public Vector3 Up => _up;

        public Vector3 Right => _right;

        // We convert from degrees to radians as soon as the property is set to improve performance.
        public float Pitch
        {
            get => MathHelper.RadiansToDegrees(_pitch);
            set
            {
                var angle = MathHelper.Clamp(value, -89f, 89f);
                _pitch = MathHelper.DegreesToRadians(angle);
                UpdateVectors();
            }
        }

        // We convert from degrees to radians as soon as the property is set to improve performance.
        public float Yaw
        {
            get => MathHelper.RadiansToDegrees(_yaw);
            set
            {
                _yaw = MathHelper.DegreesToRadians(value);
                UpdateVectors();
            }
        }

        public float Fov
        {
            get => MathHelper.RadiansToDegrees(_fov);
            set
            {
                var angle = MathHelper.Clamp(value, 1f, 90f);
                _fov = MathHelper.DegreesToRadians(angle);
            }
        }

        // Get the view matrix using the amazing LookAt function described more in depth on the web tutorials
        public Matrix4 GetViewMatrix()
        {
            if (myIsFly)
                return Matrix4.LookAt(myPosition, myPosition + _front, _up);
            else
                return myMatrixView;
        }

        // Get the projection matrix using the same method we have used up until this point
        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(_fov, AspectRatio, 0.01f, 1000f);
        }

        // This function is going to update the direction vertices using some of the math learned in the web tutorials.
        private void UpdateVectors()
        {
            _front.X = (float)Math.Cos(_pitch) * (float)Math.Cos(_yaw);
            _front.Y = (float)Math.Sin(_pitch);
            _front.Z = (float)Math.Cos(_pitch) * (float)Math.Sin(_yaw);

            _front = Vector3.Normalize(_front);

            _right = Vector3.Normalize(Vector3.Cross(_front, Vector3.UnitY));
            _up = Vector3.Normalize(Vector3.Cross(_right, _front));
        }

        public void MyUpdateVectors()
        {
            // Повороты в радианах
            //float yawRad = MathHelper.DegreesToRadians(myRotation.X);
            //float pitchRad = MathHelper.DegreesToRadians(myRotation.Y);
            //float rollRad = MathHelper.DegreesToRadians(myRotation.Z);


            // Матрица поворота с учетом yaw, pitch и roll
            //Matrix4 rotation = Matrix4.CreateRotationZ(myRotation.Z) *
            //                   Matrix4.CreateRotationX(myRotation.X) *
            //                   Matrix4.CreateRotationY(myRotation.Y);

            //rotation = MyGetModel;
            Vector3 pos = myModel.ExtractTranslation();

            Vector3 forward = Vector3.Transform(Vector3.UnitZ, new Matrix3(myModel));
            Vector3 up = Vector3.Transform(Vector3.UnitY, new Matrix3(myModel));

            myMatrixView = Matrix4.LookAt(pos, pos + forward, up);

            //Debug.WriteLine("pos world x = " + pos.X + " y = " + pos.Y + " z = " + pos.Z);
            //Debug.WriteLine("pos local x = " + myPosition.X + " y = " + myPosition.Y + " z = " + myPosition.Z);
        }
    }
}
