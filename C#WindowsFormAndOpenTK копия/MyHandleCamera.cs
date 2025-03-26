using OpenTK;
using System;
//using static C_WindowsFormAndOpenTK.MyCamera;

namespace C_WindowsFormAndOpenTK
{
    public class MyHandleCamera
    {
        public MyCamera MyGetCamera { get { return myCamera; } }

        private MyCamera myCamera;

        private Vector2 lastPos;

        private float myLastFrame, myDeltaTime;

        private long myTime;

        private float mySensitivity, myCameraSpeed;

        public MyHandleCamera(Vector3 _startPos, float _aspectRatio)
        {
            myCamera = new MyCamera(_startPos, _aspectRatio);
            lastPos = new Vector2(0f, 0f);
            mySensitivity = 0.2f;
            myCameraSpeed = 2.5f;
            myTime = DateTime.Now.Ticks;
            myDeltaTime = 0;
            myLastFrame = 0;
        }

        public void MyMousePress(float _x, float _y)
        {
            lastPos = new Vector2(_x, _y);
        }

        public void MyMouseMove(float _x, float _y)
        {
            float xOffset = _x - lastPos.X;
            float yOffset = _y - lastPos.Y;
            lastPos = new Vector2(_x, _y);

            myCamera.Yaw += xOffset * mySensitivity;
            myCamera.Pitch -= yOffset * mySensitivity;
        }

        public void MyUpdateCamera()
        {
            long currFrame = DateTime.Now.Ticks;
            long test = currFrame - myTime;
            myDeltaTime = test * 0.0000001f;
            myTime = currFrame;
        }

        public void MyDoMovementKeyboard(MyDirection _direction)
        {
            switch (_direction)
            {
                case MyDirection.FORWARD:
                    myCamera.Position += myCamera.Front * myCameraSpeed * myDeltaTime;
                    break;
                case MyDirection.BACKWARD:
                    myCamera.Position -= myCamera.Front * myCameraSpeed * myDeltaTime;
                    break;
                case MyDirection.LEFT:
                    myCamera.Position -= myCamera.Right * myCameraSpeed * myDeltaTime;
                    break;
                case MyDirection.RIGHT:
                    myCamera.Position += myCamera.Right * myCameraSpeed * myDeltaTime;
                    break;
                case MyDirection.UP:
                    myCamera.Position += myCamera.Up * myCameraSpeed * myDeltaTime;
                    break;
                case MyDirection.DOWN:
                    myCamera.Position -= myCamera.Up * myCameraSpeed * myDeltaTime;
                    break;
                default:
                    break;
            }
        }
    }
}
