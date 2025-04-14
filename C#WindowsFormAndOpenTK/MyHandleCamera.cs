using OpenTK;
using System;

namespace C_WindowsFormAndOpenTK
{
    public class MyHandleCamera : MyCamera
    {
        //public MyCamera MyGetCamera { get { return myCamera; } }

        //private MyCamera myCamera;

        private Vector2 lastPos;

        private float myDeltaTime;

        private long myTime;

        private float mySensitivity, myCameraSpeed;

        public MyHandleCamera(Vector3 _startPos, float _aspectRatio) : base(_startPos, _aspectRatio)
        {
            //myCamera = new MyCamera(_startPos, _aspectRatio);
            lastPos = new Vector2(0f, 0f);
            mySensitivity = 0.2f;
            myCameraSpeed = 4.5f;
            myTime = DateTime.Now.Ticks;
            myDeltaTime = 0;
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

            /*myCamera.*/Yaw += xOffset * mySensitivity;
            /*myCamera.*/Pitch -= yOffset * mySensitivity;
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
                    /*myCamera.*/myPosition += /*myCamera.*/Front * myCameraSpeed * myDeltaTime;
                    break;
                case MyDirection.BACKWARD:
                    /*myCamera.*/myPosition -= /*myCamera.*/Front * myCameraSpeed * myDeltaTime;
                    break;
                case MyDirection.LEFT:
                    /*myCamera.*/myPosition -= /*myCamera.*/Right * myCameraSpeed * myDeltaTime;
                    break;
                case MyDirection.RIGHT:
                    myPosition += /*myCamera.*/Right * myCameraSpeed * myDeltaTime;
                    break;
                case MyDirection.UP:
                    myPosition += /*myCamera.*/Up * myCameraSpeed * myDeltaTime;
                    break;
                case MyDirection.DOWN:
                    myPosition -= /*myCamera.*/Up * myCameraSpeed * myDeltaTime;
                    break;
                default:
                    break;
            }
        }
    }
}
