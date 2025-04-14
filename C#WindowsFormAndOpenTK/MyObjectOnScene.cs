using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_WindowsFormAndOpenTK
{
    public interface MyIDrawable
    {
        void MyDraw(Matrix4 _myModel, MyHandleCamera _cam);
        void MyDrawOutline(MyGameObject _myGo, MyHandleCamera _cam);
    }

    public abstract class MyObjectOnScene : MyTransform
    {
        public int myId;
        public string myName {  get; set; }
        public bool myIsDestroy { get; protected set; }
        public abstract void MyDraw(MyHandleCamera _cam);
        public abstract void MyDrawOutline(MyHandleCamera _cam);
        public abstract void MyUpdate();
        public abstract void MyInitialize();
        public abstract void MyDestroy();
    }
}
