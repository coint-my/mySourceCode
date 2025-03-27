using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;

namespace C_WindowsFormAndOpenTK
{
    public class MyGameObject : MyObjectOnScene
    {
        public static int myCounter = 0;

        private List<MyComponent> myComponents;

        public List<MyComponent> MyGetComponents { get { return myComponents; } }

        public bool myIsVisible { get; set; }

        public bool myIsWireframe { get; set; }

        public MyGameObject()
        {
            myCounter++;
            myId++;
            myName = "GameObject_" + myCounter;
            myTransform = new MyTransform();
            myComponents = new List<MyComponent>();
            myIsVisible = true;
            myIsWireframe = false;
        }

        public override string ToString()
        {
            return myName;
        }

        public void MyAddComponent(MyComponent _component)
        {
            myComponents.Add(_component);
        }

        public override void MyDestroy()
        {
            myComponents.Clear();
        }

        public override void MyDraw(Matrix4 _view, Matrix4 _projection)
        {
            for (int i = 0; i < myComponents.Count; i++)
            {
                MyIDrawable tmpObj = myComponents[i] as MyIDrawable;
                if (tmpObj != null && myIsVisible)
                {
                    if (myIsWireframe)
                        GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                    else
                        GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                    
                    tmpObj.MySetTransform(myTransform);
                    tmpObj.MyDraw(_view, _projection);
                }
            }
        }
        public override void MyDrawOutline(MyHandleCamera _cam)
        {
            for (int i = 0; i < myComponents.Count; i++)
            {
                MyIDrawable tmpObj = myComponents[i] as MyIDrawable;

                if (tmpObj != null && myIsVisible)
                {
                    if(myIsWireframe)
                        GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                    else
                        GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

                    tmpObj.MySetTransform(myTransform);
                    tmpObj.MyDrawOutline(_cam);
                }
            }
        }

        public override void MyInitialize()
        {
            
        }

        public override void MyUpdate()
        {
            
        }
    }
}
