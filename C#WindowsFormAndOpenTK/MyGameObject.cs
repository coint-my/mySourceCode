using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace C_WindowsFormAndOpenTK
{
    public class MyTransform
    {
        private List<MyTransform> myListChildren;
        public Vector3 myPosition;
        public Vector3 myRotation { get; set; }
        public Vector3 myScale { get; set; }
        //[XmlIgnore]
        public MyTransform myParent { get; set; }
        //[XmlArray("MyTransform")]
        //[XmlArrayItem("MyTransform")]
        public List<MyTransform> myChild { get { return myListChildren; } }
        public Vector3 myPivot { get; set; }

        public MyTransform()
        {
            myPosition = new Vector3(); myRotation = new Vector3(); myScale = Vector3.One;
            myParent = null;
            myPivot = new Vector3();
            myListChildren = new List<MyTransform>();
        }

        public void MyAddChild(MyTransform _child)
        {
            myListChildren.Add(_child);
        }

        public void MyRemoveChild(int _index)
        {
            myListChildren.RemoveAt(_index);
        }

        public void MyRemoveChild(MyTransform _child)
        {
            myListChildren.Remove(_child);
        }
    }
    
    public class MyGameObject : MyObjectOnScene, IDisposable, ICloneable
    {
        public static int myCounter = 0;

        protected Matrix4 myModel;
        private Vector3 myPositionPivot;
        public Matrix4 MyGetModel {  get { return myModel; } }
        private MySimplePolygonColor myPolygonColorPivot;

        public List<MyComponent> myComponents;

        public bool MyIsShowPivot { get; set; }
        public bool myIsVisible { get; set; }
        public bool myIsWireframe { get; set; }
        public int myHesh { get; set; }

        public MyShader myShader;
        private MyShader myShaderWireframe;

        public MyGameObject()
        {
            myShader = new MyShader("Resources/Shaders/shaderModel.vert", "Resources/Shaders/shaderLighting.frag");
            myShaderWireframe = new MyShader("Resources/Shaders/shader.vert", "Resources/Shaders/shader.frag");

            myShader.SetVector3("viewPos", Vector3.One);
            myShader.SetInt("material.diffuse", 0);
            myShader.SetInt("material.specular", 1);
            myShader.SetVector3("material.specular", new Vector3(0.4f, 0.4f, 0.4f));
            myShader.SetFloat("material.shininess", 32.0f);

            myShader.SetVector3("light.direction", new Vector3(-0.2f, -1.0f, -0.3f));
            myShader.SetVector3("light.ambient", new Vector3(0.06f));
            myShader.SetVector3("light.diffuse", new Vector3(0.5f));
            myShader.SetVector3("light.specular", new Vector3(0.0f));

            myId = myCounter;
            myHesh = GetHashCode();
            myName = "GameObject_" + myCounter;
            myPolygonColorPivot = new MySimplePolygonColor();
            myModel = Matrix4.Identity;
            myComponents = new List<MyComponent>();
            myIsVisible = true;
            myIsWireframe = false;
            MyIsShowPivot = false;
            myIsDestroy = false;
        }
        public MyGameObject(string _name) : this()
        {
            myName = _name + "_" + myCounter;
        }

        public MyGameObject(MyGameObject myCopyObject) : this()
        {
            myName = "copy" + myCounter + "_" + myCopyObject.myName;
            myModel = myCopyObject.myModel;
            myComponents = myCopyObject.myComponents;
            myIsVisible = myCopyObject.myIsVisible;
            myIsWireframe = myCopyObject.myIsWireframe;
            MyIsShowPivot = myCopyObject.MyIsShowPivot;
            myIsDestroy = myCopyObject.myIsDestroy;

            myPosition = myCopyObject.myPosition;
            myRotation = myCopyObject.myRotation;
            myScale = myCopyObject.myScale;
        }

        public object Clone()
        {
            MyGameObject obj = (MyGameObject)MemberwiseClone(); // встроенный метод поверхностного копирования
            obj.myName = "copy" + myCounter + "_" + myName;
            obj.myId = myCounter;
            MyIncrementID();
            return obj;
        }

        public void Dispose()
        {
            myShader.Dispose();
            myShader = null;
            myShaderWireframe.Dispose();
            myShaderWireframe = null;
            //if(myComponents.Count > 0 && myComponents[0] is MyModel) (myComponents[0] as MyModel).Dispose();
            myComponents.Clear();
        }

        public static void MyIncrementID()
        {
            myCounter++;
        }

        public void MySetName(string _name)
        {
            myName = _name + "_" + myCounter;
        }

        public MyComponent MyGetComponent<T>()
        {
            MyComponent component = null;
            for (int i = 0; i < myComponents.Count; i++)
            {
                if (myComponents[i] is T)
                    component = myComponents[i];
            }
            return component;
        }

        public void MySetVisible(bool _visible)
        {
            myIsVisible = _visible;

            if (myChild.Count > 0)
            {
                for (int i = 0; i < myChild.Count; i++)
                {
                    ((MyGameObject)myChild[i]).MySetVisible(_visible);
                }
            }
        }

        public override string ToString()
        {
            return myName;
        }

        public void MyAddComponent(MyComponent _component)
        {
            if(_component is MyModel)
            {
                if (myShader.myTextures.Count == 0)
                    myShader.myTextures = ((MyModel)_component).MyGetMaterialsTextures();

                myComponents.Add(_component);
            }
            else
            {
                myComponents.Add(_component);                
            }

        }

        public override void MyDestroy()
        {
            myIsDestroy = true;

            if (myChild.Count > 0)
            {
                for (int i = 0; i < myChild.Count; i++)
                {
                    ((MyGameObject)myChild[i]).MyDestroy();
                }
            }
        }

        public void MyGetAllChildGameObjectRecursion(ref List<MyGameObject> _childOut, MyGameObject _child)
        {
            for (int i = 0; i < _child.myChild.Count; i++)
            {
                _childOut.Add(_child.myChild[i] as MyGameObject);
                MyGetAllChildGameObjectRecursion(ref _childOut, _child.myChild[i] as MyGameObject);
            }
        }

        public List<MyGameObject> MyGetCopyGameObject()
        {
            List<MyGameObject> listFindObject = new List<MyGameObject>();
            listFindObject.Add(this);

            MyGetAllChildGameObjectRecursion(ref listFindObject, this);

            List<MyGameObject> listCopyGameObject = new List<MyGameObject>();

            for (int i = 0; i < listFindObject.Count; i++)
            {
                //listCopyGameObject.Add(new MyGameObject(listFindObject[i]));
                listCopyGameObject.Add((MyGameObject) listFindObject[i].Clone());
            }

            MySetChildrenNewGameObject(ref listCopyGameObject);
            MySetParentNewGameObject(ref listCopyGameObject);
            
            return listCopyGameObject;
        }

        private void MySetParentNewGameObject(ref List<MyGameObject> _listGO)
        {
            for (int go = 0; go < _listGO.Count; go++)
            {
                if (_listGO[go].myParent != null)
                {
                    for (int par = 0; par < _listGO.Count; par++)
                    {
                        if (((MyGameObject)_listGO[go].myParent).myHesh == _listGO[par].myHesh)
                        {
                            _listGO[go].myParent = _listGO[par];
                            break;
                        }
                    }
                }
            }
        }

        private void MySetChildrenNewGameObject(ref List<MyGameObject> _listGO)
        {
            for (int go = 0; go < _listGO.Count; go++)
                if (_listGO[go].myChild.Count > 0)
                    for (int child = 0; child < _listGO[go].myChild.Count; child++)
                        for (int i = 0; i < _listGO.Count; i++)
                            if (((MyGameObject)_listGO[go].myChild[child]).myHesh == _listGO[i].myHesh)
                            {
                                _listGO[go].myChild[child] = _listGO[i];
                                break;
                            }
        }

        public override void MyDraw(MyHandleCamera _cam, MySimpleRectGL _glRect)
        {
            MyTransformUpdate();

            for (int i = 0; i < myComponents.Count; i++)
            {
                MyIDrawable tmpObj = myComponents[i] as MyIDrawable;
                if (tmpObj != null && myIsVisible)
                {
                    if (myIsWireframe)
                    {
                        GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                        tmpObj.MyDraw(myModel, _cam, myShaderWireframe);
                    }
                    else
                    {
                        GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                        tmpObj.MyDraw(myModel, _cam, myShader);
                    }
                }
            }

            if (MyIsShowPivot)
            {
                GL.Disable(EnableCap.DepthTest);

                myPolygonColorPivot.MySetPosition(
                    myParent != null ? myPositionPivot : myPivot + myPosition);

                myPolygonColorPivot.MyDraw(_cam, new Vector4(0.9f, 0.9f, 0.0f, 1.0f), _glRect);
                GL.Enable(EnableCap.DepthTest);
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

                    tmpObj.MyDrawOutline(this, _cam);
                }
            }
        }

        public void MyTransformUpdate(out Matrix4 _myModel, Vector3 _myScale, Vector3 _myRotation,
            Vector3 _myPosition, Vector3 _myPivot)
        {
            if (myParent == null)
            {
                _myModel = Matrix4.Identity;
                _myModel = _myModel * Matrix4.CreateScale(_myScale);
                _myModel = _myModel * Matrix4.CreateTranslation(-_myPivot);
                _myModel = _myModel * Matrix4.CreateFromQuaternion(
                    OpenTK.Quaternion.FromEulerAngles(_myRotation));
                _myModel = _myModel * Matrix4.CreateTranslation(_myPivot);
                _myModel = _myModel * Matrix4.CreateTranslation(_myPosition);
            }
            else
            {
                MyGameObject parentObj = (MyGameObject)myParent;
                Matrix4 parentModel = parentObj.MyGetModel;
                _myModel = Matrix4.Identity;
                _myModel = _myModel * Matrix4.CreateScale(_myScale);
                _myModel = _myModel * Matrix4.CreateTranslation(-_myPivot);
                _myModel = _myModel * Matrix4.CreateFromQuaternion(
                   OpenTK.Quaternion.FromEulerAngles(_myRotation));
                _myModel = _myModel * Matrix4.CreateTranslation(_myPivot);
                _myModel = _myModel * Matrix4.CreateTranslation(_myPosition);

                _myModel *= Matrix4.CreateFromQuaternion(parentModel.ExtractRotation());
                _myModel *= Matrix4.CreateTranslation(parentModel.ExtractTranslation());
            }
        }

        public void MyTransformUpdate()
        {
            if (myParent == null)
            {
                myModel = Matrix4.Identity;
                myModel = myModel * Matrix4.CreateScale(myScale);
                myModel = myModel * Matrix4.CreateTranslation(-myPivot);
                myModel = myModel * Matrix4.CreateFromQuaternion(
                   OpenTK.Quaternion.FromEulerAngles(myRotation));
                myModel = myModel * Matrix4.CreateTranslation(myPivot);
                myModel = myModel * Matrix4.CreateTranslation(myPosition);
            }
            else
            {
                MyGameObject parentObj = (MyGameObject)myParent;
                Matrix4 parentModel = parentObj.MyGetModel;
                myModel = Matrix4.Identity;
                myModel = myModel * Matrix4.CreateScale(myScale);
                myModel = myModel * Matrix4.CreateTranslation(-myPivot);
                myModel = myModel * Matrix4.CreateFromQuaternion(
                   OpenTK.Quaternion.FromEulerAngles(myRotation));
                myModel = myModel * Matrix4.CreateTranslation(myPivot);
                myModel = myModel * Matrix4.CreateTranslation(myPosition);

                myModel *= Matrix4.CreateFromQuaternion(parentModel.ExtractRotation());
                myModel *= Matrix4.CreateTranslation(parentModel.ExtractTranslation());

                Matrix4 test = Matrix4.Identity;
                test *= Matrix4.CreateTranslation(myPosition);
                test *= Matrix4.CreateTranslation(myPivot);
                
                test *= Matrix4.CreateFromQuaternion(parentModel.ExtractRotation());
                test *= Matrix4.CreateTranslation(parentModel.ExtractTranslation());

                myPositionPivot = test.ExtractTranslation();
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
