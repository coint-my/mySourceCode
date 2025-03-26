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
        void MySetTransform(MyTransform _tr);
        void MyDraw(Matrix4 _view, Matrix4 _projection);
        void MyDrawOutline(MyHandleCamera _cam);
    }

    public abstract class MyObjectOnScene
    {
        public int myId;
        public string myName {  get; set; }
        public MyTransform myTransform {  get; set; }
        public abstract void MyDraw(Matrix4 _view, Matrix4 _projection);
        public abstract void MyDrawOutline(MyHandleCamera _cam);
        public abstract void MyUpdate();
        public abstract void MyInitialize();
        public abstract void MyDestroy();
    }
}
