using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace C_WindowsFormAndOpenTK
{
    public interface MyIDrawable
    {
        void MyDraw(Matrix4 _myModel, MyHandleCamera _cam, MyShader _myShader);
        void MyDrawOutline(MyGameObject _myGo, MyHandleCamera _cam);
    }
    
    public abstract class MyObjectOnScene : MyTransform
    {
        public int myId;
        public string myName {  get; set; }
        public bool myIsDestroy { get; protected set; }
        public abstract void MyDraw(MyHandleCamera _cam, MySimpleRectGL _myGlRect);
        public abstract void MyDrawOutline(MyHandleCamera _cam);
        public abstract void MyUpdate();
        public abstract void MyInitialize();
        public abstract void MyDestroy();
    }
}
