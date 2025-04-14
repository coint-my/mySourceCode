using OpenTK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_WindowsFormAndOpenTK
{
    public class MyTestCamera : MyGameObject
    {
        private Vector3 _front = -Vector3.UnitZ;

        private Vector3 _up = Vector3.UnitY;

        private Vector3 _right = Vector3.UnitX;

        private float _fov = MathHelper.DegreesToRadians(60);

        public float MyAspectRatio { get; set; }

        public MyTestCamera() : base("Camera")
        {
            MyAspectRatio = 1;
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(myPosition, myPosition + _front, _up);
        }

        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(_fov, MyAspectRatio, 0.01f, 1000f);
        }

        public override void MyUpdate()
        {
            base.MyUpdate();
            //_front.X = (float)Math.Cos(_pitch) * (float)Math.Cos(_yaw);
            //_front.Y = (float)Math.Sin(_pitch);
            //_front.Z = (float)Math.Cos(_pitch) * (float)Math.Sin(_yaw);
            
            _front.X = (float)Math.Cos(myRotation.Y) * (float)Math.Cos(myRotation.X);
            _front.Y = (float)Math.Sin(myRotation.Y);
            _front.Z = (float)Math.Cos(myRotation.Y) * (float)Math.Sin(myRotation.X);

            _front = Vector3.Normalize(_front);

            _right = Vector3.Normalize(Vector3.Cross(_front, Vector3.UnitY));
            _up = Vector3.Normalize(Vector3.Cross(_right, _front));
        }
    }
}
