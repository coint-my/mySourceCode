using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using HeyRed.Mime;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace C_WindowsFormAndOpenTK
{
    public partial class FormMain : Form
    {
        bool[] myKeyPress = new bool[256];
        //private readonly float[] _vertices =
        //{
        //    // Position         Texture coordinates
        //     0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
        //     0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
        //    -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
        //    -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        //};
        //private readonly uint[] _indices =
        //{
        //    0, 1, 3,
        //    1, 2, 3 
        //};
        //private int _vertexBufferObject;
        //private int _vertexArrayObject;
        //private int _elementBufferObject;
        //public string myPathDirectory;
        //private MyShader _shader;
        //private MyTexture _texture;
        //private MyTexture _texture2;
        //private MyTexture myTextureWhite_8_8;
        public MyHandleCamera myCameraFly;
        public MyHandleCamera myCameraCurrent;
        public MyObjectOnScene testDepth = null;
        //private MyTestCamera myTestCamera;
        //private MySimplePolygonColor myTestPolygon;

        MyModel myModel;
        public MyShader myShaderOutline;
        public MyModel myPrefabSphere;
        public MyModel myPrefabCube;
        public MyModel myPrefabPlane;
        public MyShader myShaderLight;

        MyEditor myEditor;
        MyScene myCurrentScene;
        public static Dictionary<string, MyTestTexture> myDictionaryTextures;
        //List<MyObjectOnScene> myListObjects;
        //MyTransform myBufferTransform;

        private Timer myTimer = null;
        //private float myAngle;
        private GLControl glControl;

        private Vector3 VecPosLight = new Vector3(0, 0, -2);

        public FormMain()
        {
            myEditor = new MyEditor(this);
            InitializeComponent();
            InitGLControl();
            myCurrentScene = new MyScene();
        }

        private void InitGLControl()
        {
            glControl = new GLControl(new GraphicsMode(32, 24, 0, 4));
            glControl.Dock = DockStyle.Fill;

            glControl.Load += GlControl_Load;
            glControl.Paint += GlControl_Paint;
            glControl.Resize += GlControl_Resize;
            glControl.KeyDown += GlControl_KeyDown;
            glControl.KeyUp += GlControl_KeyUp;
            glControl.MouseMove += GlControl_MouseMove;
            glControl.MouseDown += GlControl_MouseDown;

            panelOpenTK.Controls.Add(glControl);

            // Redraw the screen every 1/20 of a second.
            myTimer = new Timer();
            myTimer.Tick += MyUpdate;
            myTimer.Interval = 30;   // 1000 ms per sec / 50 ms per frame = 20 FPS
            myTimer.Start();
        }

        private void GlControl_KeyUp(object sender, KeyEventArgs e)
        {
            myKeyPress[e.KeyValue] = false;
        }

        private void GlControl_KeyDown(object sender, KeyEventArgs e)
        {
            myKeyPress[e.KeyValue] = true;
        }

        private void GlControl_MouseDown(object sender, MouseEventArgs e)
        {
            myCameraCurrent.MyMousePress(e.X, e.Y);
        }

        private void GlControl_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                myCameraCurrent.MyMouseMove(e.X, e.Y);                
            }
        }

        private void MyUpdate(object sender, EventArgs e)
        {
            for (int ind = 0; ind < myCurrentScene.myListObjects.Count; ind++)
            {
                myCurrentScene.myListObjects[ind].MyUpdate();
            }

            myCameraCurrent.MyUpdateCamera();

            MyKeyDown();

            MyRender();
        }

        private void MyRender()
        {
            GL.ClearColor(0.2f, 0.2f, 0.2f, 0.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //_texture.Use(TextureUnit.Texture0);
            //_texture2.Use(TextureUnit.Texture1);
            //_shader.Use();

            //var model = Matrix4.Identity;
            //myAngle += 0.1f;
            //model = model * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(myAngle));
            //model = model * Matrix4.CreateScale(1.1f);
            //model = model * Matrix4.CreateTranslation(VecPos);
            //_shader.SetMatrix4("model", model);
            //_shader.SetMatrix4("view", myCamera.MyGetCamera.GetViewMatrix());
            //_shader.SetMatrix4("projection", myCamera.MyGetCamera.GetProjectionMatrix());

            //float greenValue = ((float)Math.Sin(MathHelper.DegreesToRadians(myAngle * 20)) / 2) + 0.6f;
            //int vertexColorLocation = GL.GetUniformLocation(_shader.Handle, "ourColor");
            //GL.Uniform4(vertexColorLocation, 0.0f, greenValue, 0.0f, 1.0f);

            //GL.BindVertexArray(_vertexArrayObject);

            //GL.Enable(EnableCap.DepthTest);
            //GL.StencilFunc(StencilFunction.Always, 1, 0xFF);
            //GL.StencilMask(0xFF);

            //myShaderOutline.Use();
            //GL.Uniform1(GL.GetUniformLocation(myShaderOutline.Handle, "outLine"), 1.05f);
            //myShaderOutline.SetMatrix4("model", model);
            //myShaderOutline.SetMatrix4("view", myCamera.MyGetCamera.GetViewMatrix());
            //myShaderOutline.SetMatrix4("projection", myCamera.MyGetCamera.GetProjectionMatrix());
            //GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);

            //GL.StencilFunc(StencilFunction.Notequal, 1, 0xFF);
            //GL.StencilMask(0x00);
            //GL.Disable(EnableCap.DepthTest);

            //_shader.Use();
            //GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);

            //GL.StencilMask(0xFF);
            //GL.StencilFunc(StencilFunction.Always, 0, 0xFF);
            GL.Enable(EnableCap.DepthTest);

            myShaderLight.SetVector3("viewPos", VecPosLight);

            // Here we specify to the shaders what textures they should refer to when we want to get the positions.
            myShaderLight.SetInt("material.diffuse", 0);
            myShaderLight.SetInt("material.specular", 1);
            myShaderLight.SetVector3("material.specular", new Vector3(0.4f, 0.4f, 0.4f));
            myShaderLight.SetFloat("material.shininess", 32.0f);
            
            myShaderLight.SetVector3("light.direction", new Vector3(-0.2f, -1.0f, -0.3f));
            myShaderLight.SetVector3("light.ambient", new Vector3(0.06f));
            myShaderLight.SetVector3("light.diffuse", new Vector3(0.5f));
            myShaderLight.SetVector3("light.specular", new Vector3(0.0f));

            for (int i = 0; i < myCurrentScene.myListObjects.Count; i++)
            {
                if (testDepth != null && testDepth == myCurrentScene.myListObjects[i])
                {
                    testDepth = myCurrentScene.myListObjects[i];
                }
                else
                    myCurrentScene.myListObjects[i].MyDraw(myCameraCurrent);
            }

            if (testDepth != null)
            {
                GL.Disable(EnableCap.DepthTest);
                testDepth.MyDrawOutline(myCameraCurrent);
                GL.Enable(EnableCap.DepthTest);
                testDepth.MyDraw(myCameraCurrent);
            }

            glControl.SwapBuffers();
        }

        private void MyKeyDown()
        {
            myEditor.MyUpdateNumericUpDown();
            if (myKeyPress[87])
            {
                myCameraCurrent.MyDoMovementKeyboard(MyDirection.FORWARD);
            }
            if (myKeyPress[83])
            {
                myCameraCurrent.MyDoMovementKeyboard(MyDirection.BACKWARD);
            }
            if (myKeyPress[65])
            {
                myCameraCurrent.MyDoMovementKeyboard(MyDirection.LEFT);
            }
            if (myKeyPress[68])
            {
                myCameraCurrent.MyDoMovementKeyboard(MyDirection.RIGHT);
            }
            if (myKeyPress[32])
            {
                myCameraCurrent.MyDoMovementKeyboard(MyDirection.UP);
            }
            if (myKeyPress[17])
            {
                myCameraCurrent.MyDoMovementKeyboard(MyDirection.DOWN);
            }
        }

        //public void MyUpdateNumericUpDown()
        //{
        //    if (testDepth != null)
        //    {
        //        numericPositionX.Value = (decimal)testDepth.myPosition.X;
        //        numericPositionY.Value = (decimal)testDepth.myPosition.Y;
        //        numericPositionZ.Value = (decimal)testDepth.myPosition.Z;

        //        numericRotationX.Value = (decimal)testDepth.myRotation.X;
        //        numericRotationY.Value = (decimal)testDepth.myRotation.Y;
        //        numericRotationZ.Value = (decimal)testDepth.myRotation.Z;

        //        numericScaleX.Value = (decimal)testDepth.myScale.X;
        //        numericScaleY.Value = (decimal)testDepth.myScale.Y;
        //        numericScaleZ.Value = (decimal)testDepth.myScale.Z;
        //    }
        //}

        private void GlControl_Load(object sender, EventArgs e)
        {
            GL.Enable(EnableCap.DepthTest);
            //GL.Enable(EnableCap.CullFace);
            //GL.CullFace(CullFaceMode.Front);
            //GL.FrontFace(FrontFaceDirection.Cw);

            GL.ClearColor(System.Drawing.Color.MidnightBlue);

            //_vertexArrayObject = GL.GenVertexArray();
            //GL.BindVertexArray(_vertexArrayObject);

            //_vertexBufferObject = GL.GenBuffer();
            //GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            //GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), 
            //    _vertices, BufferUsageHint.StaticDraw);

            //_elementBufferObject = GL.GenBuffer();
            //GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            //GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint),
            //    _indices, BufferUsageHint.StaticDraw);

            //_shader = new MyShader("Resources/Shaders/shader.vert", "Resources/Shaders/shader.frag");
            //_shader.Use();

            //var vertexLocation = _shader.GetAttribLocation("aPosition");
            //GL.EnableVertexAttribArray(vertexLocation);
            //GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, 
            //    false, 5 * sizeof(float), 0);

            //var texCoordLocation = _shader.GetAttribLocation("aTexCoord");
            //GL.EnableVertexAttribArray(texCoordLocation);
            //GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float,
            //    false, 5 * sizeof(float), 3 * sizeof(float));

            //_texture = MyTexture.LoadFromFile("Resources/Textures/box1.jpg");
            //_texture.Use(TextureUnit.Texture0);

            //_texture2 = MyTexture.LoadFromFile("Resources/Textures/stone.jpg");
            //_texture2.Use(TextureUnit.Texture1);

            //_shader.SetInt("texture0", 0);
            //_shader.SetInt("texture1", 1);

            myCameraFly = new MyHandleCamera(Vector3.UnitZ * 3, MyGetAspectRatio());
            myCameraCurrent = myCameraFly;
            myCameraCurrent.myIsFly = true;

            //myTextureWhite_8_8 = MyTexture.LoadFromFile("Resources/Textures/myWhite_8_8.jpg");
            //myTextureWhite_8_8.Use(TextureUnit.Texture2);
            //myTestPolygon = new MySimplePolygonColor(ref myTextureWhite_8_8, 2);
            //myTestPolygon = new MySimplePolygonColor();

            myShaderOutline = new MyShader("Resources/Shaders/shaderOutline.vert", 
                                    "Resources/Shaders/shaderOutline.frag");
            myShaderLight = new MyShader("Resources/Shaders/shaderModel.vert",
                "Resources/Shaders/shaderLighting.frag");
            myModel = new MyModel("Resources/Models/rock/rock.obj", myShaderLight, myShaderOutline);
            myPrefabSphere = new MyModel("Resources/Models/sphere/sphere1.FBX", myShaderLight, myShaderOutline);
            myPrefabCube = new MyModel("Resources/Models/cub/cub.FBX", myShaderLight, myShaderOutline);
            myPrefabPlane = new MyModel("Resources/Models/plane/plane.FBX", myShaderLight, myShaderOutline);

            //myCurrentScene.myListObjects = new List<MyObjectOnScene>();

            MyAddTreeViewGameObject(MyInstantiateInScene(new MyHandleCamera(Vector3.Zero, MyGetAspectRatio())));
            MyAddTreeViewGameObject(MyInstantiateInScene(new MyGameObject(), myModel));
            MyAddTreeViewGameObject(MyInstantiateInScene(new MyGameObject(), myPrefabSphere));
            MyAddTreeViewGameObject(MyInstantiateInScene(new MyGameObject(), myPrefabCube));

            myEditor.MyInitializeExplorer("Resources");
            MyInitializeTextures();
            MyEventEditor();
        }

        private void MyEventEditor()
        {
            listView1.AfterLabelEdit += new LabelEditEventHandler(myEditor.listView1_AfterLabelEdit);
            listView1.BeforeLabelEdit += new LabelEditEventHandler(myEditor.listView1_BeforeLabelEdit);
            listView1.ItemDrag += new ItemDragEventHandler(myEditor.listView1_ItemDrag);
            listView1.DoubleClick += new EventHandler(myEditor.listView1_DoubleClick);

            contextMenuStripExplorer.Opening += new CancelEventHandler(myEditor.contextMenuStripExplorer_Opening);

            createFolderToolStripMenuItem.Click += new EventHandler(myEditor.createFolderToolStripMenuItem_Click);

            rename.Click += new EventHandler(myEditor.rename_Click);

            deleteToolStrip.Click += new EventHandler(myEditor.deleteToolStrip_Click);

            numericScaleX.ValueChanged += new EventHandler(myEditor.numericScaleX_ValueChanged);
            numericScaleZ.ValueChanged += new EventHandler(myEditor.numericScaleZ_ValueChanged);
            numericScaleY.ValueChanged += new EventHandler(myEditor.numericScaleY_ValueChanged);

            numericRotationX.ValueChanged += new EventHandler(myEditor.numericRotationX_ValueChanged);
            numericRotationZ.ValueChanged += new EventHandler(myEditor.numericRotationZ_ValueChanged);
            numericRotationY.ValueChanged += new EventHandler(myEditor.numericRotationY_ValueChanged);

            numericPositionX.ValueChanged += new EventHandler(myEditor.numericUpDownX_ValueChanged);
            numericPositionZ.ValueChanged += new EventHandler(myEditor.numericUpDownZ_ValueChanged);
            numericPositionY.ValueChanged += new EventHandler(myEditor.numericUpDownY_ValueChanged);

            treeViewGameObjects.AfterLabelEdit += new NodeLabelEditEventHandler(myEditor.treeViewGameObjects_AfterLabelEdit);
            treeViewGameObjects.ItemDrag += new ItemDragEventHandler(myEditor.treeViewGameObjects_ItemDrag);
            treeViewGameObjects.AfterSelect += new TreeViewEventHandler(myEditor.treeViewGameObjects_AfterSelect);
            treeViewGameObjects.DragDrop += new DragEventHandler(myEditor.treeViewGameObjects_DragDrop);
            treeViewGameObjects.DragEnter += new DragEventHandler(myEditor.treeViewGameObjects_DragEnter);
            treeViewGameObjects.DragOver += new DragEventHandler(myEditor.treeViewGameObjects_DragOver);
            treeViewGameObjects.MouseDown += new MouseEventHandler(myEditor.treeViewGameObjects_MouseDown);

            contextMenuStripHierarhy.Opening += new CancelEventHandler(myEditor.contextMenuStripHierarhy_Opening);
            contextMenuStripHierarhy.ItemClicked += new ToolStripItemClickedEventHandler(
                myEditor.contextMenuStripHierarhy_ItemClicked);

            renameToolStripMenuItem.Click += new System.EventHandler(myEditor.renameToolStripMenuItem_Click);

            CreateGameObjectEmpty.Click += new System.EventHandler(myEditor.CreateGameObjectEmpty_Click);

            cubeToolStripMenuItem.Click += new System.EventHandler(myEditor.cubeToolStripMenuItem_Click);

            sphereToolStripMenuItem.Click += new System.EventHandler(myEditor.sphereToolStripMenuItem_Click);

            planeToolStripMenuItem.Click += new System.EventHandler(myEditor.planeToolStripMenuItem_Click);

            cameraToolStripMenuItem.Click += new System.EventHandler(myEditor.cameraToolStripMenuItem_Click);
        }

        public float MyGetAspectRatio() => glControl.Width / (float)glControl.Height;

        private void MyInitializeTextures()
        {
            DirectoryInfo baseDirectory = new DirectoryInfo("Resources");
            myDictionaryTextures = new Dictionary<string, MyTestTexture>();

            foreach(var dir in baseDirectory.GetDirectories("*", System.IO.SearchOption.AllDirectories))
            {
                foreach (var files in dir.GetFiles())
                {
                    string typeName = MimeTypesMap.GetMimeType(files.Name);
                    string type = typeName.Substring(0, typeName.LastIndexOf('/'));

                    if (type == "image")
                    {
                        string nameDirectory = dir.FullName.Substring(dir.FullName.LastIndexOf("Resources"));
                        MyTestTexture texture = MyTestTexture.LoadFromFile(nameDirectory + "//" + files.Name);
                        myDictionaryTextures.Add(texture.path, texture);
                    }
                }
            }
        }

        //public void MyInitializeExplorer(string _nameDir)
        //{
        //    myPathDirectory = _nameDir;
        //    DirectoryInfo dirInfo = new DirectoryInfo(myPathDirectory);
        //    if (dirInfo.Exists)
        //    {
        //        groupBoxExplorer.Text = myPathDirectory;
        //        listView1.Items.Clear();
        //        ImageList listImg = new ImageList();
        //        listImg.ImageSize = new Size(32, 32);
        //        listImg.Images.Add("folder", Properties.Resources.folder);
        //        listImg.Images.Add("file", Properties.Resources.file);
        //        listImg.Images.Add("fileImage", Properties.Resources.fileImage);
        //        listView1.LargeImageList = listImg;

        //        if (myPathDirectory != "Resources")
        //        {
        //            listView1.Items.Add("...", "folder");
        //        }

        //        foreach (var item in dirInfo.EnumerateDirectories())
        //        {
        //            listView1.Items.Add(item.Name, "folder");
        //        }

        //        foreach (var item in dirInfo.EnumerateFiles())
        //        {
        //            string typeName = MimeTypesMap.GetMimeType(item.FullName);
        //            string type = typeName.Substring(0, typeName.LastIndexOf('/'));

        //            if (type == "image")
        //            {
        //                listView1.Items.Add(item.Name, "fileImage");
        //            }
        //            else
        //            {
        //                listView1.Items.Add(item.Name, "file");
        //            }
        //        }
        //    }
        //    else
        //        Debug.WriteLine("Wrong Directory = " + myPathDirectory);
        //}

        //public bool MyIsEqualDirectory(string _name)
        //{
        //    DirectoryInfo directoryCheck = new DirectoryInfo(myPathDirectory);

        //    foreach (var item in directoryCheck.GetDirectories())
        //    {
        //        if(item.Name.ToLower() == _name.ToLower())
        //            return true;
        //    }

        //    return false;
        //}

        //private void createFolderToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string newName = "newFolder";
        //        for (int i = 1; i < 10000; i++)
        //        {
        //            string tempName = newName + i;
        //            if (!MyIsEqualDirectory(tempName))
        //            {
        //                newName = tempName;
        //                break;
        //            }
        //        }
        //        DirectoryInfo directory = Directory.CreateDirectory(myPathDirectory + "//" + newName);

        //        ListViewItem item = listView1.Items.Add(newName, "folder");
        //        item.BeginEdit();
        //    }
        //    catch(Exception ex) { Debug.WriteLine(ex); }
        //}

        //private void deleteToolStrip_Click(object sender, EventArgs e)
        //{
        //    foreach (var item in listView1.SelectedItems)
        //    {
        //        DirectoryInfo dInfo = new DirectoryInfo(myPathDirectory + "//" + ((ListViewItem)item).Text);
        //        try
        //        {
        //            Directory.Delete(myPathDirectory + "//" + ((ListViewItem)item).Text, true);
        //            MyInitializeExplorer(myPathDirectory);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Delete wrong " + ex.Message, "Exeption", MessageBoxButtons.OK,
        //                MessageBoxIcon.Error);
        //        }
        //    }
        //}

        //private void rename_Click(object sender, EventArgs e)
        //{
        //    listView1.SelectedItems[0].BeginEdit();
        //}

        //private void contextMenuStripExplorer_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    ToolStripItem itemRename = contextMenuStripExplorer.Items.Find("Rename", false)[0];
        //    itemRename.Enabled = false;
        //    ToolStripItem itemDelete = contextMenuStripExplorer.Items.Find("deleteToolStrip", false)[0];
        //    itemDelete.Enabled = false;

        //    int count = listView1.SelectedIndices.Count;

        //    if (count == 1)
        //    {
        //        string path = listView1.SelectedItems[0].Text;
        //        DirectoryInfo dInfo = new DirectoryInfo(myPathDirectory + "//" + path);
        //        if (path == "...") return;
        //        if (dInfo.Attributes == FileAttributes.Directory || dInfo.Attributes == FileAttributes.Archive)
        //        {
        //            itemRename = contextMenuStripExplorer.Items.Find("Rename", false)[0];
        //            itemRename.Enabled = true;
        //            itemDelete = contextMenuStripExplorer.Items.Find("deleteToolStrip", false)[0];
        //            itemDelete.Enabled = true;
        //        }
        //    }
        //    else if (count > 1)
        //    {               
        //        foreach (var item in listView1.SelectedItems)
        //        {
        //            DirectoryInfo dInfo = new DirectoryInfo(myPathDirectory + "//" + ((ListViewItem)item).Text);
        //            if (((ListViewItem)item).Text == "...") return;
        //            if (dInfo.Attributes == FileAttributes.Directory || dInfo.Attributes == FileAttributes.Archive)
        //                continue;
        //            else
        //                return;
        //        }

        //        itemDelete = contextMenuStripExplorer.Items.Find("deleteToolStrip", false)[0];
        //        itemDelete.Enabled = true;
        //    }
        //}

        public void MyAddTreeViewGameObject(MyObjectOnScene _object)
        {
            if (treeViewGameObjects.SelectedNode == null)
            {
                _object.MyInitialize();
                int index = treeViewGameObjects.Nodes.Add(new TreeNode(_object.myName));
                treeViewGameObjects.Nodes[index].Tag = _object;
            }
            else
            {
                _object.MyInitialize();
                MyGameObject target = (MyGameObject)treeViewGameObjects.SelectedNode.Tag;
                target.MyAddChild(_object);
                _object.myParent = target;
                TreeNode node = new TreeNode(_object.myName);
                node.Tag = _object;
                treeViewGameObjects.SelectedNode.Nodes.Add(node);
            }
        }

        public MyGameObject MyInstantiateInScene(MyGameObject _gameObject, MyComponent _component)
        {
            MyGameObject.MyIncrementID();
            MyGameObject _go = _gameObject;
            _go.MyAddComponent(_component);
            myCurrentScene.myListObjects.Add(_go);
            return _go;
        }
        public MyGameObject MyInstantiateInScene(MyGameObject _gameObject)
        {
            MyGameObject.MyIncrementID();
            MyGameObject _go = _gameObject;
            myCurrentScene.myListObjects.Add(_go);
            return _go;
        }

        public void MyDestroy(MyGameObject _go)
        {
            _go.MyDestroy();

            for (int i = myCurrentScene.myListObjects.Count - 1; i >= 0; i--)
            {
                if (myCurrentScene.myListObjects[i].myIsDestroy)
                    myCurrentScene.myListObjects.RemoveAt(i);
            }
        }
        
        public void GlControl_Paint(object sender, PaintEventArgs e)
        {
            MyRender();
        }
        
        public void GlControl_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, glControl.Width, glControl.Height);

            if (myCameraCurrent != null)
            {
                myCameraCurrent.AspectRatio = MyGetAspectRatio();
                myCameraFly.AspectRatio = MyGetAspectRatio();

                for (int i = 0; i < myCurrentScene.myListObjects.Count; i++)
                    if (myCurrentScene.myListObjects[i] is MyHandleCamera)
                        ((MyHandleCamera)myCurrentScene.myListObjects[i]).AspectRatio = MyGetAspectRatio();
            }
        }

        #region myDepricate
        //public void treeViewGameObjects_MouseDown(object sender, MouseEventArgs e)
        //{
        //    Point point = e.Location;

        //    if(treeViewGameObjects.GetNodeAt(point) == null)
        //    {
        //        flowLayoutPanelMyParameters.Controls.Clear();
        //        treeViewGameObjects.SelectedNode = null;
        //        testDepth = null;
        //        myCameraCurrent = myCameraFly;
        //    }
        //}

        //public void treeViewGameObjects_AfterSelect(object sender, TreeViewEventArgs e)
        //{
        //    if (e.Node.IsSelected)
        //    {
        //        MyGameObject myGameObject = e.Node.Tag as MyGameObject;
        //        testDepth = myGameObject;

        //        MyUpdateNumericUpDown();
        //        myEditor.MyCheckParameterModel(myGameObject);
        //    }
        //}

        //public void MyCheckParameterModel(MyGameObject _myGameObject)
        //{
        //    if (flowLayoutPanelMyParameters.Controls.Count > 0)
        //        flowLayoutPanelMyParameters.Controls.Clear();

        //    MyHandleCamera myCam = _myGameObject as MyHandleCamera;
        //    if (myCam != null)
        //        myCameraCurrent = myCam;
        //    else myCameraCurrent = myCameraFly;

        //    if (_myGameObject.MyGetComponent<MyModel>() != null)
        //    {
        //        MyModel model = _myGameObject.MyGetComponent<MyModel>();
        //        GroupBox gBox = MyCreateGroupBox(_myGameObject.myName, "Model " + model.MyGetDirectory);
        //        myEditor.MyCreatePivotVector(_myGameObject);
        //        FlowLayoutPanel flow = MyCreateFlowLayoutPanel(FlowDirection.TopDown);
        //        gBox.Controls.Add(flow);
        //        CheckBox checkBoxVisible = myEditor.MyCreateCheckBox("IsVisible", _myGameObject,
        //            _myGameObject.myIsVisible, myEditor.CheckBox_IsVisible);
        //        CheckBox checkBoxWireframe = myEditor.MyCreateCheckBox("IsWireframe", _myGameObject,
        //            _myGameObject.myIsWireframe, myEditor.CheckBox_IsWireframe);
        //        flow.Controls.Add(checkBoxVisible);
        //        flow.Controls.Add(checkBoxWireframe);

        //        myEditor.MyShowTextureParameter(model);
        //    }
        //}

        //public void MyShowTextureParameter(MyModel _model)
        //{
        //    GroupBox gBox = MyCreateGroupBox("Texture", "Model " + _model.MyGetDirectory);
        //    FlowLayoutPanel flow = MyCreateFlowLayoutPanel(FlowDirection.TopDown);
        //    gBox.Controls.Add(flow);
        //    Button buttonTexture = new Button();
        //    buttonTexture.Size = new Size(220, 30);
        //    buttonTexture.Text = _model.MyGetTexture != null ? _model.MyGetTexture.myName : "None";
        //    buttonTexture.AllowDrop = true;
        //    buttonTexture.Tag = _model;
        //    buttonTexture.DragEnter += myEditor.ButtonTextureMyParameters_DragEnter;
        //    buttonTexture.DragDrop += myEditor.ButtonTextureMyParameters_DragDrop;
        //    flow.Controls.Add(buttonTexture);
        //    flow.Controls.Add(myEditor.MyAddLabelAndVector2("Tex Coords", _model, myEditor.MyEventU_ValueChanged,
        //        myEditor.MyEventV_ValueChanged));
        //}

        //private void MyEventV_ValueChanged(object _sender, EventArgs _e)
        //{
        //    NumericUpDown nud = (NumericUpDown)_sender;
        //    MyModel model = (MyModel)nud.Tag;
        //    Vector3 newUV = new Vector3(model.myTexCoord.X, (float)nud.Value, 0);
        //    model.myTexCoord = newUV;
        //}

        //private void MyEventU_ValueChanged(object _sender, EventArgs _e)
        //{
        //    NumericUpDown nud = (NumericUpDown)_sender;
        //    MyModel model = (MyModel)nud.Tag;
        //    Vector3 newUV = new Vector3((float)nud.Value, model.myTexCoord.Y, 0);
        //    model.myTexCoord = newUV;
        //}

        //private Panel MyAddLabelAndVector2(string _label, MyModel _model, EventHandler _U, EventHandler _V)
        //{
        //    Label name = new Label();
        //    name.AutoSize = true;
        //    name.Text = _label;

        //    NumericUpDown numericV = new NumericUpDown();
        //    numericV.Size = new Size(38, 20);
        //    numericV.Increment = 0.1m;
        //    numericV.DecimalPlaces = 1;
        //    numericV.Maximum = 100;
        //    numericV.Minimum = 0.1m;
        //    numericV.Value = (decimal)_model.myTexCoord.Y;
        //    numericV.Tag = _model;
        //    numericV.ValueChanged += _V;
        //    NumericUpDown numericU = new NumericUpDown();
        //    numericU.Size = new Size(38, 20);
        //    numericU.Increment = 0.1m;
        //    numericU.DecimalPlaces = 1;
        //    numericU.Maximum = 100;
        //    numericU.Minimum = 0.1m;
        //    numericU.Value = (decimal)_model.myTexCoord.X;
        //    numericU.Tag = _model;
        //    numericU.ValueChanged += _U;
        //    Label labelU = new Label();
        //    labelU.AutoSize = true;
        //    labelU.Text = "U";
        //    Label labelV = new Label();
        //    labelV.AutoSize = true;
        //    labelV.Text = "V";

        //    FlowLayoutPanel flow = MyCreateFlowLayoutPanel(FlowDirection.LeftToRight);
        //    flow.Size = new Size(220, 70);
        //    flow.Controls.Add(name);
        //    flow.Controls.Add(labelU);
        //    flow.Controls.Add(numericU);
        //    flow.Controls.Add(labelV);
        //    flow.Controls.Add(numericV);
        //    return flow;
        //}

        //private GroupBox MyCreatePivotVector(MyGameObject _gameObject)
        //{
        //    GroupBox gBox = MyCreateGroupBox("Pivot ", "");
        //    FlowLayoutPanel flow = MyCreateFlowLayoutPanel(FlowDirection.LeftToRight);
        //    flow.AutoSize = false;
        //    flow.Size = new Size(240, 70);
        //    gBox.Controls.Add(flow);
        //    Label labelX = new Label();
        //    labelX.AutoSize = true;
        //    labelX.Text = "X";
        //    Label labelY = new Label();
        //    labelY.AutoSize = true;
        //    labelY.Text = "Y";
        //    Label labelZ = new Label();
        //    labelZ.AutoSize = true;
        //    labelZ.Text = "Z";
        //    NumericUpDown nx = new NumericUpDown();
        //    nx.Size = new Size(50, 20);
        //    nx.Increment = 0.1m;
        //    nx.DecimalPlaces = 2;
        //    nx.Maximum = 1000;
        //    nx.Minimum = -1000;
        //    nx.Value = (decimal)_gameObject.myPivot.X;
        //    nx.Tag = _gameObject;
        //    nx.ValueChanged += myEditor.Nx_ValueChanged;
        //    NumericUpDown ny = new NumericUpDown();
        //    ny.Size = new Size(50, 20);
        //    ny.Increment = 0.1m;
        //    ny.DecimalPlaces = 2;
        //    ny.Maximum = 1000;
        //    ny.Minimum = -1000;
        //    ny.Value = (decimal)_gameObject.myPivot.Y;
        //    ny.Tag = _gameObject;
        //    ny.ValueChanged += myEditor.Ny_ValueChanged;
        //    NumericUpDown nz = new NumericUpDown();
        //    nz.Size = new Size(50, 20);
        //    nz.Increment = 0.1m;
        //    nz.DecimalPlaces = 2;
        //    nz.Maximum = 1000;
        //    nz.Minimum = -1000;
        //    nz.Value = (decimal)_gameObject.myPivot.Z;
        //    nz.Tag = _gameObject;
        //    nz.ValueChanged += myEditor.Nz_ValueChanged;
        //    CheckBox checkBoxShowPivot = new CheckBox();
        //    checkBoxShowPivot.AutoSize = true;
        //    checkBoxShowPivot.Text = "Show Pivot";
        //    checkBoxShowPivot.Checked = _gameObject.MyIsShowPivot;
        //    checkBoxShowPivot.Tag = _gameObject;
        //    checkBoxShowPivot.CheckedChanged += myEditor.CheckBoxShowPivot_CheckedChanged;
        //    flow.Controls.Add(labelX);
        //    flow.Controls.Add(nx);
        //    flow.Controls.Add(labelY);
        //    flow.Controls.Add(ny);
        //    flow.Controls.Add(labelZ);
        //    flow.Controls.Add(nz);
        //    flow.Controls.Add(checkBoxShowPivot);

        //    return gBox;
        //}

        //private void CheckBoxShowPivot_CheckedChanged(object sender, EventArgs e)
        //{
        //    CheckBox box = (CheckBox)sender;
        //    MyGameObject gameObject = (MyGameObject)box.Tag;
        //    gameObject.MyIsShowPivot = box.Checked;
        //}

        //private void Nx_ValueChanged(object sender, EventArgs e)
        //{
        //    NumericUpDown nud = (NumericUpDown)sender;
        //    MyGameObject gameObject = (MyGameObject)nud.Tag;
        //    Vector3 tempPivot = gameObject.myPivot;
        //    Vector3 newPivot = new Vector3((float)nud.Value, tempPivot.Y, tempPivot.Z);
        //    gameObject.myPivot = newPivot;
        //}
        //private void Ny_ValueChanged(object sender, EventArgs e)
        //{
        //    NumericUpDown nud = (NumericUpDown)sender;
        //    MyGameObject gameObject = (MyGameObject)nud.Tag;
        //    Vector3 tempPivot = gameObject.myPivot;
        //    Vector3 newPivot = new Vector3(tempPivot.X, (float)nud.Value, tempPivot.Z);
        //    gameObject.myPivot = newPivot;
        //}
        //private void Nz_ValueChanged(object sender, EventArgs e)
        //{
        //    NumericUpDown nud = (NumericUpDown)sender;
        //    MyGameObject gameObject = (MyGameObject)nud.Tag;
        //    Vector3 tempPivot = gameObject.myPivot;
        //    Vector3 newPivot = new Vector3(tempPivot.X, tempPivot.Y, (float)nud.Value);
        //    gameObject.myPivot = newPivot;
        //}

        //private CheckBox MyCreateCheckBox(string _text, MyGameObject _myGameObject, bool _myIsWireframe,
        //    EventHandler _eventMethod)
        //{
        //    CheckBox checkBox = new CheckBox();
        //    checkBox.Text = _text;
        //    checkBox.Checked = _myIsWireframe;
        //    checkBox.CheckedChanged += _eventMethod;
        //    checkBox.Tag = _myGameObject;
        //    return checkBox;
        //}

        //private void CheckBox_IsWireframe(object sender, EventArgs e)
        //{
        //    CheckBox checkBox = (CheckBox)sender;
        //    ((MyGameObject)checkBox.Tag).myIsWireframe = checkBox.Checked;
        //}

        //private void CheckBox_IsVisible(object sender, EventArgs e)
        //{
        //    CheckBox checkBox = (CheckBox)sender;
        //    MyGameObject go = (MyGameObject)checkBox.Tag;
        //    go.MySetVisible(checkBox.Checked);
        //}

        private GroupBox MyCreateGroupBox(string _nameGameObject, string _nameComponent)
        {
            GroupBox groupBox = new GroupBox();
            groupBox.Text = "(" + _nameGameObject + ") " +
                _nameComponent.Substring(_nameComponent.LastIndexOf('/') + 1);
            groupBox.MinimumSize = new Size(220, 50);
            groupBox.AutoSize = true;

            flowLayoutPanelMyParameters.Controls.Add(groupBox);
            return groupBox;
        }

        private FlowLayoutPanel MyCreateFlowLayoutPanel(FlowDirection _direcion)
        {
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.AutoSize = true;
            flowLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel.FlowDirection = _direcion;
            flowLayoutPanel.Location = new Point(1, 30);
            flowLayoutPanel.BorderStyle = BorderStyle.FixedSingle;
            return flowLayoutPanel;
        }

        //private void numericUpDownX_ValueChanged(object sender, EventArgs e)
        //{
        //    if (testDepth != null)
        //    {
        //        Vector3 v3 = testDepth.myPosition;
        //        v3.X = (float)((NumericUpDown)sender).Value;
        //        testDepth.myPosition = v3;
        //    }
        //}

        //private void numericUpDownY_ValueChanged(object sender, EventArgs e)
        //{
        //    if (testDepth != null)
        //    {
        //        Vector3 v3 = testDepth.myPosition;
        //        v3.Y = (float)((NumericUpDown)sender).Value;
        //        testDepth.myPosition = v3;
        //    }
        //}

        //private void numericUpDownZ_ValueChanged(object sender, EventArgs e)
        //{
        //    if (testDepth != null)
        //    {
        //        Vector3 v3 = testDepth.myPosition;
        //        v3.Z = (float)((NumericUpDown)sender).Value;
        //        testDepth.myPosition = v3;
        //    }
        //}

        //private void numericRotationX_ValueChanged(object sender, EventArgs e)
        //{
        //    if (testDepth != null)
        //    {
        //        Vector3 v3 = testDepth.myRotation;
        //        v3.X = (float)((NumericUpDown)sender).Value;
        //        testDepth.myRotation = v3;
        //    }
        //}

        //private void numericRotationY_ValueChanged(object sender, EventArgs e)
        //{
        //    if (testDepth != null)
        //    {
        //        Vector3 v3 = testDepth.myRotation;
        //        v3.Y = (float)((NumericUpDown)sender).Value;
        //        testDepth.myRotation = v3;
        //    }
        //}

        //private void numericRotationZ_ValueChanged(object sender, EventArgs e)
        //{
        //    if (testDepth != null)
        //    {
        //        Vector3 v3 = testDepth.myRotation;
        //        v3.Z = (float)((NumericUpDown)sender).Value;
        //        testDepth.myRotation = v3;
        //    }
        //}

        //private void numericScaleX_ValueChanged(object sender, EventArgs e)
        //{
        //    if (testDepth != null)
        //    {
        //        Vector3 v3 = testDepth.myScale;
        //        v3.X = (float)((NumericUpDown)sender).Value;
        //        testDepth.myScale = v3;
        //    }
        //}

        //private void numericScaleY_ValueChanged(object sender, EventArgs e)
        //{
        //    if (testDepth != null)
        //    {
        //        Vector3 v3 = testDepth.myScale;
        //        v3.Y = (float)((NumericUpDown)sender).Value;
        //        testDepth.myScale = v3;
        //    }
        //}

        //private void numericScaleZ_ValueChanged(object sender, EventArgs e)
        //{
        //    if (testDepth != null)
        //    {
        //        Vector3 v3 = testDepth.myScale;
        //        v3.Z = (float)((NumericUpDown)sender).Value;
        //        testDepth.myScale = v3;
        //    }
        //}

        //private void treeViewGameObjects_ItemDrag(object sender, ItemDragEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        DoDragDrop(e.Item, DragDropEffects.Move);
        //    }
        //}

        //private void treeViewGameObjects_DragEnter(object sender, DragEventArgs e)
        //{
        //    e.Effect = e.AllowedEffect;
        //}

        //private void treeViewGameObjects_DragOver(object sender, DragEventArgs e)
        //{
        //    Point targetPoint = treeViewGameObjects.PointToClient(new Point(e.X, e.Y));
        //    treeViewGameObjects.SelectedNode = treeViewGameObjects.GetNodeAt(targetPoint);
        //}

        //private void treeViewGameObjects_DragDrop(object sender, DragEventArgs e)
        //{
        //    Point targetPoint = treeViewGameObjects.PointToClient(new Point(e.X, e.Y));
        //    TreeNode targetNode = treeViewGameObjects.GetNodeAt(targetPoint);

        //    TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

        //    if (targetNode == null)
        //    {
        //        draggedNode.Remove();
        //        MyGameObject goDrag = (MyGameObject)draggedNode.Tag;

        //        if(goDrag.myParent != null)
        //            goDrag.myParent.MyRemoveChild(goDrag);

        //        goDrag.myParent = null;

        //        treeViewGameObjects.Nodes.Add((TreeNode)draggedNode.Clone());
        //        return;
        //    }

        //    if (!draggedNode.Equals(targetNode) && !MyContainsNode(draggedNode, targetNode))
        //    {
        //        Debug.WriteLine("drop parent");
        //        if (e.Effect == DragDropEffects.Move)
        //        {
        //            draggedNode.Remove();

        //            MyGameObject goDrag = (MyGameObject)draggedNode.Tag;
        //            MyGameObject goTarget = (MyGameObject)targetNode.Tag;

        //            if (goDrag.myParent != null)
        //                goDrag.myParent.MyRemoveChild(goDrag);

        //            goDrag.myParent = goTarget;
        //            goTarget.MyAddChild(goDrag);

        //            targetNode.Nodes.Add(draggedNode);
        //        }

        //        targetNode.Expand();
        //    }
        //}

        //private bool MyContainsNode(TreeNode node1, TreeNode node2)
        //{
        //    if (node2.Parent == null) 
        //        return false;
        //    if (node2.Parent.Equals(node1)) 
        //        return true;

        //    return MyContainsNode(node1, node2.Parent);
        //}

        //private void contextMenuStripHierarhy_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    if (treeViewGameObjects.SelectedNode != null)
        //    {
        //        contextMenuStripHierarhy.Items["Delete"].Enabled = true;
        //        contextMenuStripHierarhy.Items["renameToolStripMenuItem"].Enabled = true;
        //    }
        //    else
        //    {
        //        contextMenuStripHierarhy.Items["Delete"].Enabled = false;
        //        contextMenuStripHierarhy.Items["renameToolStripMenuItem"].Enabled = false;
        //    }
        //}

        //private void contextMenuStripHierarhy_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        //{
        //    Debug.WriteLine("empty " + e.ClickedItem);

        //    if (e.ClickedItem.ToString() == "Delete")
        //    {
        //        MyGameObject goDelete = (MyGameObject)treeViewGameObjects.SelectedNode.Tag;
        //        MyDestroy(goDelete);
        //        treeViewGameObjects.SelectedNode.Remove();
        //        Debug.WriteLine(e.ClickedItem.ToString() + " = " + goDelete.myName + " listObject.Count = " +
        //            myListObjects.Count);

        //        flowLayoutPanelMyParameters.Controls.Clear();
        //        treeViewGameObjects.SelectedNode = null;
        //        testDepth = null;
        //    }
        //}

        //private void CreateGameObjectEmpty_Click(object sender, EventArgs e)
        //{
        //    MyGameObject gameObject = new MyGameObject("Empty obj");
        //    MyAddTreeViewGameObject(MyInstantiateInScene(gameObject));
        //}

        //private void cubeToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    MyGameObject gameObject = new MyGameObject("Cube");
        //    MyAddTreeViewGameObject(MyInstantiateInScene(gameObject, myPrefabCube));
        //}

        //private void sphereToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    MyGameObject gameObject = new MyGameObject("Sphere");
        //    MyAddTreeViewGameObject(MyInstantiateInScene(gameObject, myPrefabSphere));
        //}

        //private void planeToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    MyGameObject gameObject = new MyGameObject("Plane");
        //    MyAddTreeViewGameObject(MyInstantiateInScene(gameObject, myPrefabPlane));
        //}

        //private void cameraToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    MyGameObject gameObject = new MyHandleCamera(Vector3.Zero, MyGetAspectRatio());
        //    MyAddTreeViewGameObject(MyInstantiateInScene(gameObject));
        //}

        //private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    treeViewGameObjects.SelectedNode.BeginEdit();
        //}

        //private void treeViewGameObjects_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        //{
        //    if (e.Label != null && e.Label.Length > 0)
        //        ((MyGameObject)treeViewGameObjects.SelectedNode.Tag).myName = e.Label;
        //    else
        //        e.CancelEdit = true;
        //}

        //private void ButtonTextureMyParameters_DragEnter(object sender, DragEventArgs e)
        //{
        //    if ((ListViewItem)e.Data.GetData(typeof(ListViewItem)) != null)
        //    {
        //        string nameData = ((ListViewItem)e.Data.GetData(typeof(ListViewItem))).Text;
        //        string typeName = MimeTypesMap.GetMimeType(nameData);
        //        string type = typeName.Substring(0, typeName.LastIndexOf('/'));

        //        if(type == "image")
        //        {
        //            Control currControl = sender as Control;
        //            currControl.Select();
        //            e.Effect = e.AllowedEffect;
        //        }
        //    }
        //}

        //private void ButtonTextureMyParameters_DragDrop(object sender, DragEventArgs e)
        //{
        //    string fileNameTexture = myPathDirectory + "//" + 
        //        (e.Data.GetData(typeof(ListViewItem)) as ListViewItem).Text;
        //    Debug.WriteLine("full name = " + fileNameTexture);

        //    MyTestTexture texture = myDictionaryTextures[fileNameTexture];
        //    MyModel model = ((Button)sender).Tag as MyModel;
        //    Button boxTexture = sender as Button;
        //    string nameTexture = System.IO.Path.GetFileNameWithoutExtension((e.Data.GetData(typeof(ListViewItem))
        //        as ListViewItem).Text);
        //    boxTexture.Text = nameTexture;
        //    model.MyGetTexture = texture;
        //}

        //private void listView1_DoubleClick(object sender, EventArgs e)
        //{
        //    string nameFolder = listView1.SelectedItems[0].Text;

        //    if (listView1.SelectedItems[0].Text == "...")
        //        nameFolder = myPathDirectory.Substring(0, myPathDirectory.LastIndexOf('/') - 1);
        //    else
        //        nameFolder = myPathDirectory + "//" + nameFolder;

        //    MyInitializeExplorer(nameFolder);
        //}

        //private void listView1_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        //{
        //    bool isNotPossible = true;
        //    string path = listView1.SelectedItems[0].Text;
        //    DirectoryInfo dInfo = new DirectoryInfo(myPathDirectory + "//" + path);
        //    if (dInfo.Attributes == FileAttributes.Directory || dInfo.Attributes == FileAttributes.Archive)
        //        isNotPossible = false;

        //    e.CancelEdit = isNotPossible;
        //}

        //private void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        //{
        //    if (e.Label != null && !MyIsEqualDirectory(e.Label))
        //        FileSystem.RenameDirectory(myPathDirectory + "//" + listView1.SelectedItems[0].Text,
        //            e.Label);
        //    else
        //        e.CancelEdit = true;
        //}

        //private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        //{
        //    DoDragDrop(e.Item, DragDropEffects.Move);
        //}
        #endregion

        private void saveSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myCurrentScene.MySaveScene();
        }

        private void loadSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myCurrentScene.MyLoadScene(myCurrentScene, this);
        }
    }
}
