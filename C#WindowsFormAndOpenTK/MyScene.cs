using System;
using System.Collections.Generic;
using OpenTK;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace C_WindowsFormAndOpenTK
{
    public struct MySaveGameObject
    {
        public string myName;
        public string myObj;
        public MyShader myShader;
        //public int myID;

        public Vector3 myPosition;
        public Vector3 myRotation;
        public Vector3 myScale;
        public Vector3 myPivot;

        public int myParent;
        public List<int> myChild;

        public bool MyIsShowPivot;
        public bool myIsVisible;
        public bool myIsWireframe;

        public List<MyComponent> myComponents;
    }

    public class MyScene : IDisposable
    {
        public string MyNameScene;

        public List<MyObjectOnScene> myListObjects;

        public List<MySaveGameObject> mySaveObjects;

        public MyScene() 
        {
            myListObjects = new List<MyObjectOnScene>();
            mySaveObjects = new List<MySaveGameObject>();
            MyNameScene = "newScene";
        }

        public void Dispose()
        {
            myListObjects.Clear();
            mySaveObjects.Clear();
            MyNameScene = "None";
        }

        private void MyInitializeSaves()
        {
            mySaveObjects.Clear();

            foreach (var obj in myListObjects)
            {
                MySaveGameObject saveObj = new MySaveGameObject();
                saveObj.myName = obj.myName;

                if (((MyGameObject)obj).myShader.myTextures.Count > 0)
                     saveObj.myShader = ((MyGameObject)obj).myShader;
                saveObj.myObj = obj.GetType().FullName;
                //saveObj.myID = obj.myId;
                saveObj.myPosition = obj.myPosition;
                saveObj.myRotation = obj.myRotation;
                saveObj.myScale = obj.myScale;
                saveObj.myPivot = obj.myPivot;
                saveObj.myParent = (MyGameObject)obj.myParent != null ? ((MyGameObject)obj.myParent).myId : -1;
                saveObj.myChild = new List<int>();
                foreach (var child in obj.myChild)
                {
                    saveObj.myChild.Add(((MyGameObject)child).myId);
                }
                saveObj.MyIsShowPivot = ((MyGameObject)obj).MyIsShowPivot;
                saveObj.myIsVisible = ((MyGameObject)obj).myIsVisible;
                saveObj.myIsWireframe = ((MyGameObject)obj).myIsWireframe;
                saveObj.myComponents = ((MyGameObject)obj).myComponents;

                mySaveObjects.Add(saveObj);
            }
        }

        private bool MyLoadGameObject(FormMain _formMain)
        {
            try
            {
                myListObjects.Clear();
                _formMain.treeViewGameObjects.Nodes.Clear();
                MyGameObject.myCounter = 0;

                foreach (var obj in mySaveObjects)
                {
                    MyGameObject go = Activator.CreateInstance(Type.GetType(obj.myObj)) as MyGameObject;
                    go.myPosition = obj.myPosition;
                    go.myRotation = obj.myRotation;
                    go.myScale = obj.myScale;
                    go.myPivot = obj.myPivot;
                    go.myName = obj.myName;
                    //go.myId = obj.myID;
                    go.MyIsShowPivot = obj.MyIsShowPivot;
                    go.myIsVisible = obj.myIsVisible;
                    go.myIsWireframe= obj.myIsWireframe;

                    if(obj.myComponents.Count > 0)
                    {
                        if (obj.myComponents[0] is MyModel)
                        {
                            MyModel mod = (MyModel)obj.myComponents[0];

                            MyModel model = FormMain.myDictionaryPrefabs[mod.myPrefab];

                            go.myShader.myTexCoord = obj.myShader.myTexCoord;
                            try
                            {
                                go.myShader.myTextures.Add(
                                    FormMain.myDictionaryTextures[obj.myShader.myTextures[0].path]);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("Exeption textures in list not found = " + obj.myShader +
                                    " " + ex.Message);
                            }

                            _formMain.MyAddTreeViewGameObject(_formMain.MyInstantiateInScene(go, model));
                            continue;
                        }
                    }

                    _formMain.MyAddTreeViewGameObject(_formMain.MyInstantiateInScene(go));
                }

                MySetGameObjectParent();

                MySetGameObjectChildren();

                return true;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
                return false;
            }
        }

        private void MySetGameObjectChildren()
        {
            for (int ind = 0; ind < mySaveObjects.Count; ind++)
                if (mySaveObjects[ind].myChild.Count > 0)
                {
                    MyGameObject parentGameObject = myListObjects[ind] as MyGameObject;

                    for (int child = 0; child < mySaveObjects[ind].myChild.Count; child++)
                        for (int i = 0; i < myListObjects.Count; i++)
                        {
                            int childID = mySaveObjects[ind].myChild[child];

                            if (childID == myListObjects[i].myId)
                                parentGameObject.MyAddChild(myListObjects[i]);
                        }
                }
        }

        private void MySetGameObjectParent()
        {
            for (int ind = 0; ind < mySaveObjects.Count; ind++)
                if (mySaveObjects[ind].myParent >= 0)
                    for (int i = 0; i < myListObjects.Count; i++)
                        if(mySaveObjects[ind].myParent == myListObjects[i].myId)
                            myListObjects[ind].myParent = myListObjects[i];
        }

        private void MySetChildTreeViewGameObject(FormMain _main)
        {
            foreach (var parents in myListObjects)
            {
                if(parents.myChild.Count > 0)
                {
                    TreeNode nodeParent = MyTreeViewFindNodeByID(
                        _main.treeViewGameObjects.Nodes, (MyGameObject)parents);

                    foreach (var objectChild in parents.myChild)
                    {
                        TreeNode nodeChild = MyTreeViewFindNodeByID(
                            _main.treeViewGameObjects.Nodes, ((MyGameObject)objectChild));

                        nodeChild.Remove();

                        nodeParent.Nodes.Add(nodeChild);
                    }
                }
            }
        }

        private TreeNode MyTreeViewFindNodeByText(TreeNodeCollection _nodes, string _text)
        {
            foreach (TreeNode node in _nodes)
            {
                if (node.Text == _text)
                    return node;

                // Рекурсивный поиск в подузлах
                TreeNode found = MyTreeViewFindNodeByText(node.Nodes, _text);
                if (found != null)
                    return found;
            }

            return null; // Если не найден
        }

        private TreeNode MyTreeViewFindNodeByID(TreeNodeCollection _nodes, MyGameObject _go)
        {
            foreach (TreeNode node in _nodes)
            {
                if (((MyGameObject)node.Tag).myId == _go.myId)
                    return node;

                // Рекурсивный поиск в подузлах
                TreeNode found = MyTreeViewFindNodeByID(node.Nodes, _go);
                if (found != null)
                    return found;
            }

            return null; // Если не найден
        }

        public void MySaveScene(string _namePath)
        {
            MyInitializeSaves();

            Type[] types = new Type[] { typeof(MyModel), typeof(MyHandleCamera) };
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<MySaveGameObject>), types);

            using (StreamWriter sw = new StreamWriter(_namePath))
            {
                xmlSerializer.Serialize(sw, mySaveObjects);
            }
        }

        public void MyLoadScene(string _namePath, FormMain _formMain)
        {
            MyNameScene = _namePath;
            MyGameObject.myCounter = 0;
            _formMain.groupBoxScene.Text = MyNameScene;

            Type[] types = new Type[] { typeof(MyModel) };
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<MySaveGameObject>), types);

            try
            {
                using (TextReader tr = new StreamReader(_namePath))
                {
                    mySaveObjects = (List<MySaveGameObject>)xmlSerializer.Deserialize(tr);
                }
            }
            catch (Exception ex) { Debug.WriteLine("Exeption = " + ex.Message); }

            if (MyLoadGameObject(_formMain))
            {
                MySetChildTreeViewGameObject(_formMain);
                Debug.WriteLine("load ok");
            }
            else
                Debug.WriteLine("load error");
        }
    }
}
