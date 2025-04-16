using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Diagnostics;

namespace C_WindowsFormAndOpenTK
{
    public class MyHandleCamera : MyCamera
    {
        private MySimplePolygonColor myPolygonTexture;

        private Vector2 lastPos;

        private float myDeltaTime;

        private long myTime;

        private float mySensitivity, myCameraSpeed;

        public MyHandleCamera(Vector3 _startPos, float _aspectRatio) : base(_startPos, _aspectRatio)
        {
            lastPos = new Vector2(0f, 0f);
            mySensitivity = 0.2f;
            myCameraSpeed = 4.5f;
            myTime = DateTime.Now.Ticks;
            myDeltaTime = 0;
            myIsFly = false;

            MyTexture texture = MyTexture.LoadFromFile("Resources/Textures/myCam.png");
            texture.Use(TextureUnit.Texture0 + MyTexture.myCurrentIndex);
            myPolygonTexture = new MySimplePolygonColor(ref texture, MyTexture.myCurrentIndex);
            myPolygonTexture.MySetScale(new Vector3(0.5f));
        }

        public override void MyDraw(MyHandleCamera _cam)
        {
            base.MyDraw(_cam);
            myPolygonTexture.MyDraw(_cam, new Vector4(0.7f));
            myPolygonTexture.MySetPosition(myModel.ExtractTranslation());
        }

        public void MyMousePress(float _x, float _y)
        {
            if (myIsFly)
                lastPos = new Vector2(_x, _y);
        }

        public void MyMouseMove(float _x, float _y)
        {
            if (myIsFly)
            {
                float xOffset = _x - lastPos.X;
                float yOffset = _y - lastPos.Y;
                lastPos = new Vector2(_x, _y);

                Yaw += xOffset * mySensitivity;
                Pitch -= yOffset * mySensitivity;
            }
        }

        public void MyUpdateCamera()
        {
            long currFrame = DateTime.Now.Ticks;
            long test = currFrame - myTime;
            myDeltaTime = test * 0.0000001f;
            myTime = currFrame;

            if (!myIsFly)
                MyUpdateVectors();
        }

        public void MyDoMovementKeyboard(MyDirection _direction)
        {
            if (myIsFly)
            {
                switch (_direction)
                {
                    case MyDirection.FORWARD:
                        myPosition += Front * myCameraSpeed * myDeltaTime;
                        break;
                    case MyDirection.BACKWARD:
                        myPosition -= Front * myCameraSpeed * myDeltaTime;
                        break;
                    case MyDirection.LEFT:
                        myPosition -= Right * myCameraSpeed * myDeltaTime;
                        break;
                    case MyDirection.RIGHT:
                        myPosition += Right * myCameraSpeed * myDeltaTime;
                        break;
                    case MyDirection.UP:
                        myPosition += Up * myCameraSpeed * myDeltaTime;
                        break;
                    case MyDirection.DOWN:
                        myPosition -= Up * myCameraSpeed * myDeltaTime;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
