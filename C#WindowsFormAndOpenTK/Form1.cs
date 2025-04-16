using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using HeyRed.Mime;
using Microsoft.VisualBasic.FileIO;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace C_WindowsFormAndOpenTK
{
    public partial class Form1 : Form
    {
        bool[] myKeyPress = new bool[256];
        private readonly float[] _vertices =
        {
            // Position         Texture coordinates
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };
        private readonly uint[] _indices =
        {
            0, 1, 3,
            1, 2, 3 
        };
        private int _vertexBufferObject;
        private int _vertexArrayObject;
        private int _elementBufferObject;
        private long myTimeDoubleClick = DateTime.Now.Ticks;
        private string myPathDirectory;
        private MyShader _shader;
        private MyTexture _texture;
        private MyTexture _texture2;
        //private MyTexture myTextureWhite_8_8;
        private MyHandleCamera myCameraCurrent;
        private MyHandleCamera myCameraFly;
        //private MyTestCamera myTestCamera;
        MyObjectOnScene testDepth = null;
        //private MySimplePolygonColor myTestPolygon;

        MyModel myModel;
        MyShader myShaderOutline;
        MyModel myPrefabSphere;
        MyModel myPrefabCube;
        MyModel myPrefabPlane;
        MyShader myShaderLight;
        List<MyObjectOnScene> myListObjects;
        //MyTransform myBufferTransform;

        private Timer myTimer = null;
        //private float myAngle;
        private GLControl glControl;

        private Vector3 VecPos = new Vector3(0, 0, -2);


        public Form1()
        {
            InitializeComponent();
            InitGLControl();
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
            for (int ind = 0; ind < myListObjects.Count; ind++)
            {
                myListObjects[ind].MyUpdate();
            }

            myCameraCurrent.MyUpdateCamera();

            MyKeyDown();

            MyRender();
        }

        private void MyRender()
        {
            GL.ClearColor(0.2f, 0.2f, 0.2f, 0.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _texture.Use(TextureUnit.Texture0);
            _texture2.Use(TextureUnit.Texture1);
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

            myShaderLight.SetVector3("viewPos", VecPos);

            // Here we specify to the shaders what textures they should refer to when we want to get the positions.
            myShaderLight.SetInt("material.diffuse", 0);
            myShaderLight.SetInt("material.specular", 1);
            myShaderLight.SetVector3("material.specular", new Vector3(0.4f, 0.4f, 0.4f));
            myShaderLight.SetFloat("material.shininess", 32.0f);
            
            myShaderLight.SetVector3("light.direction", new Vector3(-0.2f, -1.0f, -0.3f));
            myShaderLight.SetVector3("light.ambient", new Vector3(0.06f));
            myShaderLight.SetVector3("light.diffuse", new Vector3(0.5f));
            myShaderLight.SetVector3("light.specular", new Vector3(0.0f));

            for (int i = 0; i < myListObjects.Count; i++)
            {
                if (testDepth != null && testDepth == myListObjects[i])
                {
                    testDepth = myListObjects[i];
                }
                else
                    myListObjects[i].MyDraw(myCameraCurrent);
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
            MyUpdateNumericUpDown();
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

        private void MyUpdateNumericUpDown()
        {
            if (testDepth != null)
            {
                numericPositionX.Value = (decimal)testDepth.myPosition.X;
                numericPositionY.Value = (decimal)testDepth.myPosition.Y;
                numericPositionZ.Value = (decimal)testDepth.myPosition.Z;

                numericRotationX.Value = (decimal)testDepth.myRotation.X;
                numericRotationY.Value = (decimal)testDepth.myRotation.Y;
                numericRotationZ.Value = (decimal)testDepth.myRotation.Z;

                numericScaleX.Value = (decimal)testDepth.myScale.X;
                numericScaleY.Value = (decimal)testDepth.myScale.Y;
                numericScaleZ.Value = (decimal)testDepth.myScale.Z;
            }
        }

        private void GlControl_Load(object sender, EventArgs e)
        {
            GL.Enable(EnableCap.DepthTest);
            //GL.Enable(EnableCap.CullFace);
            //GL.CullFace(CullFaceMode.Front);
            //GL.FrontFace(FrontFaceDirection.Cw);

            GL.ClearColor(System.Drawing.Color.MidnightBlue);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), 
                _vertices, BufferUsageHint.StaticDraw);

            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint),
                _indices, BufferUsageHint.StaticDraw);

            _shader = new MyShader("Resources/Shaders/shader.vert", "Resources/Shaders/shader.frag");
            _shader.Use();

            var vertexLocation = _shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, 
                false, 5 * sizeof(float), 0);

            var texCoordLocation = _shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float,
                false, 5 * sizeof(float), 3 * sizeof(float));

            _texture = MyTexture.LoadFromFile("Resources/Textures/box1.jpg");
            _texture.Use(TextureUnit.Texture0);

            _texture2 = MyTexture.LoadFromFile("Resources/Textures/stone.jpg");
            _texture2.Use(TextureUnit.Texture1);

            _shader.SetInt("texture0", 0);
            _shader.SetInt("texture1", 1);

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

            myListObjects = new List<MyObjectOnScene>();

            MyAddTreeViewGameObject(MyInstantiateInScene(new MyHandleCamera(Vector3.Zero, MyGetAspectRatio())));
            MyAddTreeViewGameObject(MyInstantiateInScene(new MyGameObject(), myModel));
            MyAddTreeViewGameObject(MyInstantiateInScene(new MyGameObject(), myPrefabSphere));
            MyAddTreeViewGameObject(MyInstantiateInScene(new MyGameObject(), myPrefabCube));

            MyInitializeExplorer("Resources");
        }

        private float MyGetAspectRatio() => glControl.Width / (float)glControl.Height;

        private void MyInitializeExplorer(string _nameDir)
        {
            myPathDirectory = _nameDir;
            DirectoryInfo dirInfo = new DirectoryInfo(myPathDirectory);
            if (dirInfo.Exists)
            {
                groupBoxExplorer.Text = myPathDirectory;
                flowLayoutExplorer.Controls.Clear();

                if (myPathDirectory != "Resources")
                    flowLayoutExplorer.Controls.Add(MyCreateFolder("...", true));

                foreach (var item in dirInfo.EnumerateDirectories())
                {
                    flowLayoutExplorer.Controls.Add(MyCreateFolder(item.Name));
                }

                foreach (var item in dirInfo.EnumerateFiles())
                {
                    string typeName = MimeTypesMap.GetMimeType(item.FullName);
                    string type = typeName.Substring(0, typeName.LastIndexOf('/'));
                    Debug.WriteLine("name" + item.Name + " type = " + typeName);

                    if (type == "image")
                        flowLayoutExplorer.Controls.Add(MyCreateFile(item.Name, Properties.Resources.fileImage));
                    else
                        flowLayoutExplorer.Controls.Add(MyCreateFile(item.Name, Properties.Resources.file));
                }
            }
            else
                Debug.WriteLine("Wrong Directory = " + myPathDirectory);
        }

        private Button MyCreateFile(string _name, Bitmap _image)
        {
            Button button = new Button();
            button.Text = _name;
            button.ImageAlign = ContentAlignment.TopCenter;
            button.TextAlign = ContentAlignment.BottomCenter;
            button.TextImageRelation = TextImageRelation.ImageAboveText;
            button.AutoEllipsis = true;
            button.UseCompatibleTextRendering = true;
            button.Image = _image;
            button.Margin = new Padding();
            button.Size = new Size(60, 60);
            button.Tag = false;

            return button;
        }

        private Button MyCreateFolder(string _name, bool _isLocked = false)
        {
            Button button = new Button();
            button.Text = _name;
            button.ImageAlign = ContentAlignment.TopCenter;
            button.TextAlign = ContentAlignment.BottomCenter;
            button.TextImageRelation = TextImageRelation.ImageAboveText;
            button.AutoEllipsis = true;
            button.UseCompatibleTextRendering = true;
            button.Image = Properties.Resources.folder;
            button.Margin = new Padding();
            button.Size = new Size(60, 60);
            button.Tag = _isLocked;
            button.Click += ButtonFolder_Click;

            return button;
        }

        private void createFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryInfo directory = Directory.CreateDirectory(myPathDirectory + "//newFolder");
            if (directory.Exists)
            {
                Debug.WriteLine("create folder " + directory.FullName + " local dir = " + myPathDirectory);
                MyInitializeExplorer(myPathDirectory);
            }
        }

        private void deleteToolStrip_Click(object sender, EventArgs e)
        {
            Button btn = MyFindSelectFolder();
            DirectoryInfo dInfo = new DirectoryInfo(myPathDirectory + "//" + btn.Text);

            try
            {
                Directory.Delete(myPathDirectory + "//" + btn.Text, true);
                MyInitializeExplorer(myPathDirectory);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete wrong " + ex.Message, "Exeption", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void rename_Click(object sender, EventArgs e)
        {
            Button btn = MyFindSelectFolder();

            using(MyRenameFolder rename = new MyRenameFolder(btn.Text))
            {
                if (rename.ShowDialog(this) == DialogResult.OK)
                {
                    FileSystem.RenameDirectory(myPathDirectory + "//" + btn.Text, rename.MyTextRename);
                    
                    btn.Text = rename.MyTextRename;
                }
            }
        }

        private void contextMenuStripExplorer_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ToolStripItem itemRename = contextMenuStripExplorer.Items.Find("Rename", false)[0];
            itemRename.Enabled = false;
            ToolStripItem itemDelete = contextMenuStripExplorer.Items.Find("deleteToolStrip", false)[0];
            itemDelete.Enabled = false;

            Button btn = MyFindSelectFolder();
            if (btn != null && (bool)btn.Tag == false)
            {
                DirectoryInfo dInfo = new DirectoryInfo(myPathDirectory + "//" + btn.Text);
                if (dInfo.Attributes == FileAttributes.Directory || dInfo.Attributes == FileAttributes.Archive)
                {
                    itemRename = contextMenuStripExplorer.Items.Find("Rename", false)[0];
                    itemRename.Enabled = true;
                    itemDelete = contextMenuStripExplorer.Items.Find("deleteToolStrip", false)[0];
                    itemDelete.Enabled = true;
                }
            }
        }

        private Button MyFindSelectFolder()
        {
            for (int i = 0; i < flowLayoutExplorer.Controls.Count; i++)
            {
                if (((Button)flowLayoutExplorer.Controls[i]).Focused)
                    return ((Button)flowLayoutExplorer.Controls[i]);
            }

            return null;
        }

        private void ButtonFolder_Click(object sender, EventArgs e)
        {
            long t = (DateTime.Now.Ticks - myTimeDoubleClick) / 1000000;

            if(t <= 2)
            {
                Console.WriteLine("time " + t);

                Button button = (Button)sender;

                string nameFolder = button.Text;

                if (button.Text == "...")
                    nameFolder = myPathDirectory.Substring(0, myPathDirectory.LastIndexOf('/') - 1);
                else
                    nameFolder = myPathDirectory + "//" + nameFolder;

                MyInitializeExplorer(nameFolder);
            }

            myTimeDoubleClick = DateTime.Now.Ticks;
        }

        private void MyAddTreeViewGameObject(MyObjectOnScene _object)
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

        private MyGameObject MyInstantiateInScene(MyGameObject _gameObject, MyComponent _component)
        {
            MyGameObject _go = _gameObject;
            _go.MyAddComponent(_component);
            myListObjects.Add(_go);
            return _go;
        }
        private MyGameObject MyInstantiateInScene(MyGameObject _gameObject)
        {
            MyGameObject _go = _gameObject;
            myListObjects.Add(_go);
            return _go;
        }

        private void MyDestroy(MyGameObject _go)
        {
            _go.MyDestroy();

            for (int i = myListObjects.Count - 1; i >= 0; i--)
            {
                if (myListObjects[i].myIsDestroy)
                    myListObjects.RemoveAt(i);
            }
        }

        private void GlControl_Paint(object sender, PaintEventArgs e)
        {
            MyRender();
        }

        private void GlControl_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, glControl.Width, glControl.Height);

            if (myCameraCurrent != null)
            {
                myCameraCurrent.AspectRatio = MyGetAspectRatio();
                myCameraFly.AspectRatio = MyGetAspectRatio();

                for (int i = 0; i < myListObjects.Count; i++)
                    if (myListObjects[i] is MyHandleCamera)
                        ((MyHandleCamera)myListObjects[i]).AspectRatio = MyGetAspectRatio();
            }
        }

        private void treeViewGameObjects_MouseDown(object sender, MouseEventArgs e)
        {
            Point point = e.Location;

            if(treeViewGameObjects.GetNodeAt(point) == null)
            {
                flowLayoutPanelMyParameters.Controls.Clear();
                treeViewGameObjects.SelectedNode = null;
                testDepth = null;
                myCameraCurrent = myCameraFly;
            }

            if (e.Button == MouseButtons.Left)
            {
                //TreeNode node = treeViewGameObjects.GetNodeAt(point);
                //MyGameObject gameObject = (MyGameObject)node.Tag;

                //Debug.WriteLine(gameObject.myName + ".parent = " + gameObject.myParent + "|" +
                //    gameObject.myName + ".child count = " + gameObject.myChild.Count);
            }
        }

        private void treeViewGameObjects_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.IsSelected)
            {
                MyGameObject myGameObject = e.Node.Tag as MyGameObject;
                testDepth = myGameObject;

                MyUpdateNumericUpDown();
                MyCheckParameterModel(myGameObject);
            }
        }

        private void MyCheckParameterModel(MyGameObject _myGameObject)
        {
            if (flowLayoutPanelMyParameters.Controls.Count > 0)
                flowLayoutPanelMyParameters.Controls.Clear();

            MyHandleCamera myCam = _myGameObject as MyHandleCamera;
            if (myCam != null)
                myCameraCurrent = myCam;
            else myCameraCurrent = myCameraFly;

            if (_myGameObject.MyGetComponent<MyModel>() != null)
            {
                MyModel model = _myGameObject.MyGetComponent<MyModel>();
                GroupBox gBox = MyCreateGroupBox(_myGameObject.myName, "Model " + model.MyGetDirectory);
                MyCreatePivotVector(_myGameObject);
                FlowLayoutPanel flow = MyCreateFlowLayoutPanel(FlowDirection.TopDown);
                gBox.Controls.Add(flow);
                CheckBox checkBoxVisible = MyCreateCheckBox("IsVisible", _myGameObject,
                    _myGameObject.myIsVisible, CheckBox_IsVisible);
                CheckBox checkBoxWireframe = MyCreateCheckBox("IsWireframe", _myGameObject,
                    _myGameObject.myIsWireframe, CheckBox_IsWireframe);
                flow.Controls.Add(checkBoxVisible);
                flow.Controls.Add(checkBoxWireframe);

                MyShowTextureParameter(model);
            }
        }

        private void MyShowTextureParameter(MyModel _model)
        {
            GroupBox gBox = MyCreateGroupBox("Texture", "Model " + _model.MyGetDirectory);
            FlowLayoutPanel flow = MyCreateFlowLayoutPanel(FlowDirection.TopDown);
            gBox.Controls.Add(flow);
            System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox();
            textBox.Size = new Size(220, 70);
            textBox.Multiline = true;
            textBox.Text = _model.MyGetTexture != null ? _model.MyGetTexture.path : "None";
            flow.Controls.Add(textBox);
            flow.Controls.Add(MyAddLabelAndVector2("Tex Coords", _model, MyEventU_ValueChanged,
                MyEventV_ValueChanged));
        }

        private void MyEventV_ValueChanged(object _sender, EventArgs _e)
        {
            NumericUpDown nud = (NumericUpDown)_sender;
            MyModel model = (MyModel)nud.Tag;
            Vector3 newUV = new Vector3(model.myTexCoord.X, (float)nud.Value, 0);
            model.myTexCoord = newUV;
        }

        private void MyEventU_ValueChanged(object _sender, EventArgs _e)
        {
            NumericUpDown nud = (NumericUpDown)_sender;
            MyModel model = (MyModel)nud.Tag;
            Vector3 newUV = new Vector3((float)nud.Value, model.myTexCoord.Y, 0);
            model.myTexCoord = newUV;
        }

        private Panel MyAddLabelAndVector2(string _label, MyModel _model, EventHandler _U, EventHandler _V)
        {
            Label name = new Label();
            name.AutoSize = true;
            name.Text = _label;

            NumericUpDown numericV = new NumericUpDown();
            numericV.Size = new Size(38, 20);
            numericV.Increment = 0.1m;
            numericV.DecimalPlaces = 1;
            numericV.Maximum = 100;
            numericV.Minimum = 0.1m;
            numericV.Value = (decimal)_model.myTexCoord.Y;
            numericV.Tag = _model;
            numericV.ValueChanged += _V;
            NumericUpDown numericU = new NumericUpDown();
            numericU.Size = new Size(38, 20);
            numericU.Increment = 0.1m;
            numericU.DecimalPlaces = 1;
            numericU.Maximum = 100;
            numericU.Minimum = 0.1m;
            numericU.Value = (decimal)_model.myTexCoord.X;
            numericU.Tag = _model;
            numericU.ValueChanged += _U;
            Label labelU = new Label();
            labelU.AutoSize = true;
            labelU.Text = "U";
            Label labelV = new Label();
            labelV.AutoSize = true;
            labelV.Text = "V";

            FlowLayoutPanel flow = MyCreateFlowLayoutPanel(FlowDirection.LeftToRight);
            flow.Size = new Size(220, 70);
            flow.Controls.Add(name);
            flow.Controls.Add(labelU);
            flow.Controls.Add(numericU);
            flow.Controls.Add(labelV);
            flow.Controls.Add(numericV);
            return flow;
        }

        private GroupBox MyCreatePivotVector(MyGameObject _gameObject)
        {
            GroupBox gBox = MyCreateGroupBox("Pivot ", "");
            FlowLayoutPanel flow = MyCreateFlowLayoutPanel(FlowDirection.LeftToRight);
            flow.AutoSize = false;
            flow.Size = new Size(240, 70);
            gBox.Controls.Add(flow);
            Label labelX = new Label();
            labelX.AutoSize = true;
            labelX.Text = "X";
            Label labelY = new Label();
            labelY.AutoSize = true;
            labelY.Text = "Y";
            Label labelZ = new Label();
            labelZ.AutoSize = true;
            labelZ.Text = "Z";
            NumericUpDown nx = new NumericUpDown();
            nx.Size = new Size(50, 20);
            nx.Increment = 0.1m;
            nx.DecimalPlaces = 2;
            nx.Maximum = 1000;
            nx.Minimum = -1000;
            nx.Value = (decimal)_gameObject.myPivot.X;
            nx.Tag = _gameObject;
            nx.ValueChanged += Nx_ValueChanged;
            NumericUpDown ny = new NumericUpDown();
            ny.Size = new Size(50, 20);
            ny.Increment = 0.1m;
            ny.DecimalPlaces = 2;
            ny.Maximum = 1000;
            ny.Minimum = -1000;
            ny.Value = (decimal)_gameObject.myPivot.Y;
            ny.Tag = _gameObject;
            ny.ValueChanged += Ny_ValueChanged;
            NumericUpDown nz = new NumericUpDown();
            nz.Size = new Size(50, 20);
            nz.Increment = 0.1m;
            nz.DecimalPlaces = 2;
            nz.Maximum = 1000;
            nz.Minimum = -1000;
            nz.Value = (decimal)_gameObject.myPivot.Z;
            nz.Tag = _gameObject;
            nz.ValueChanged += Nz_ValueChanged;
            CheckBox checkBoxShowPivot = new CheckBox();
            checkBoxShowPivot.AutoSize = true;
            checkBoxShowPivot.Text = "Show Pivot";
            checkBoxShowPivot.Checked = _gameObject.MyIsShowPivot;
            checkBoxShowPivot.Tag = _gameObject;
            checkBoxShowPivot.CheckedChanged += CheckBoxShowPivot_CheckedChanged;
            flow.Controls.Add(labelX);
            flow.Controls.Add(nx);
            flow.Controls.Add(labelY);
            flow.Controls.Add(ny);
            flow.Controls.Add(labelZ);
            flow.Controls.Add(nz);
            flow.Controls.Add(checkBoxShowPivot);

            return gBox;
        }

        private void CheckBoxShowPivot_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            MyGameObject gameObject = (MyGameObject)box.Tag;
            gameObject.MyIsShowPivot = box.Checked;
        }

        private void Nx_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            MyGameObject gameObject = (MyGameObject)nud.Tag;
            Vector3 tempPivot = gameObject.myPivot;
            Vector3 newPivot = new Vector3((float)nud.Value, tempPivot.Y, tempPivot.Z);
            gameObject.myPivot = newPivot;
        }
        private void Ny_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            MyGameObject gameObject = (MyGameObject)nud.Tag;
            Vector3 tempPivot = gameObject.myPivot;
            Vector3 newPivot = new Vector3(tempPivot.X, (float)nud.Value, tempPivot.Z);
            gameObject.myPivot = newPivot;
        }
        private void Nz_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            MyGameObject gameObject = (MyGameObject)nud.Tag;
            Vector3 tempPivot = gameObject.myPivot;
            Vector3 newPivot = new Vector3(tempPivot.X, tempPivot.Y, (float)nud.Value);
            gameObject.myPivot = newPivot;
        }

        private CheckBox MyCreateCheckBox(string _text, MyGameObject _myGameObject, bool _myIsWireframe,
            EventHandler _eventMethod)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.Text = _text;
            checkBox.Checked = _myIsWireframe;
            checkBox.CheckedChanged += _eventMethod;
            checkBox.Tag = _myGameObject;
            return checkBox;
        }

        private void CheckBox_IsWireframe(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            ((MyGameObject)checkBox.Tag).myIsWireframe = checkBox.Checked;
        }

        private void CheckBox_IsVisible(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            MyGameObject go = (MyGameObject)checkBox.Tag;
            go.MySetVisible(checkBox.Checked);
        }

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

        private void numericUpDownX_ValueChanged(object sender, EventArgs e)
        {
            if (testDepth != null)
            {
                Vector3 v3 = testDepth.myPosition;
                v3.X = (float)((NumericUpDown)sender).Value;
                testDepth.myPosition = v3;
            }
        }

        private void numericUpDownY_ValueChanged(object sender, EventArgs e)
        {
            if (testDepth != null)
            {
                Vector3 v3 = testDepth.myPosition;
                v3.Y = (float)((NumericUpDown)sender).Value;
                testDepth.myPosition = v3;
            }
        }

        private void numericUpDownZ_ValueChanged(object sender, EventArgs e)
        {
            if (testDepth != null)
            {
                Vector3 v3 = testDepth.myPosition;
                v3.Z = (float)((NumericUpDown)sender).Value;
                testDepth.myPosition = v3;
            }
        }

        private void numericRotationX_ValueChanged(object sender, EventArgs e)
        {
            if (testDepth != null)
            {
                Vector3 v3 = testDepth.myRotation;
                v3.X = (float)((NumericUpDown)sender).Value;
                testDepth.myRotation = v3;
            }
        }

        private void numericRotationY_ValueChanged(object sender, EventArgs e)
        {
            if (testDepth != null)
            {
                Vector3 v3 = testDepth.myRotation;
                v3.Y = (float)((NumericUpDown)sender).Value;
                testDepth.myRotation = v3;
            }
        }

        private void numericRotationZ_ValueChanged(object sender, EventArgs e)
        {
            if (testDepth != null)
            {
                Vector3 v3 = testDepth.myRotation;
                v3.Z = (float)((NumericUpDown)sender).Value;
                testDepth.myRotation = v3;
            }
        }

        private void numericScaleX_ValueChanged(object sender, EventArgs e)
        {
            if (testDepth != null)
            {
                Vector3 v3 = testDepth.myScale;
                v3.X = (float)((NumericUpDown)sender).Value;
                testDepth.myScale = v3;
            }
        }

        private void numericScaleY_ValueChanged(object sender, EventArgs e)
        {
            if (testDepth != null)
            {
                Vector3 v3 = testDepth.myScale;
                v3.Y = (float)((NumericUpDown)sender).Value;
                testDepth.myScale = v3;
            }
        }

        private void numericScaleZ_ValueChanged(object sender, EventArgs e)
        {
            if (testDepth != null)
            {
                Vector3 v3 = testDepth.myScale;
                v3.Z = (float)((NumericUpDown)sender).Value;
                testDepth.myScale = v3;
            }
        }

        private void treeViewGameObjects_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        private void treeViewGameObjects_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void treeViewGameObjects_DragOver(object sender, DragEventArgs e)
        {
            Point targetPoint = treeViewGameObjects.PointToClient(new Point(e.X, e.Y));
            treeViewGameObjects.SelectedNode = treeViewGameObjects.GetNodeAt(targetPoint);
        }

        private void treeViewGameObjects_DragDrop(object sender, DragEventArgs e)
        {
            Point targetPoint = treeViewGameObjects.PointToClient(new Point(e.X, e.Y));
            TreeNode targetNode = treeViewGameObjects.GetNodeAt(targetPoint);

            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            if (targetNode == null)
            {
                draggedNode.Remove();
                MyGameObject goDrag = (MyGameObject)draggedNode.Tag;

                if(goDrag.myParent != null)
                    goDrag.myParent.MyRemoveChild(goDrag);

                goDrag.myParent = null;

                treeViewGameObjects.Nodes.Add((TreeNode)draggedNode.Clone());
                return;
            }

            if (!draggedNode.Equals(targetNode) && !MyContainsNode(draggedNode, targetNode))
            {
                Debug.WriteLine("drop parent");
                if (e.Effect == DragDropEffects.Move)
                {
                    draggedNode.Remove();

                    MyGameObject goDrag = (MyGameObject)draggedNode.Tag;
                    MyGameObject goTarget = (MyGameObject)targetNode.Tag;

                    if (goDrag.myParent != null)
                        goDrag.myParent.MyRemoveChild(goDrag);

                    goDrag.myParent = goTarget;
                    goTarget.MyAddChild(goDrag);

                    targetNode.Nodes.Add(draggedNode);
                }

                targetNode.Expand();
            }
        }

        private bool MyContainsNode(TreeNode node1, TreeNode node2)
        {
            if (node2.Parent == null) 
                return false;
            if (node2.Parent.Equals(node1)) 
                return true;

            return MyContainsNode(node1, node2.Parent);
        }

        private void contextMenuStripHierarhy_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (treeViewGameObjects.SelectedNode != null)
            {
                contextMenuStripHierarhy.Items["Delete"].Enabled = true;
                contextMenuStripHierarhy.Items["renameToolStripMenuItem"].Enabled = true;
            }
            else
            {
                contextMenuStripHierarhy.Items["Delete"].Enabled = false;
                contextMenuStripHierarhy.Items["renameToolStripMenuItem"].Enabled = false;
            }
        }

        private void contextMenuStripHierarhy_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Debug.WriteLine("empty " + e.ClickedItem);
            
            if (e.ClickedItem.ToString() == "Delete")
            {
                MyGameObject goDelete = (MyGameObject)treeViewGameObjects.SelectedNode.Tag;
                MyDestroy(goDelete);
                treeViewGameObjects.SelectedNode.Remove();
                Debug.WriteLine(e.ClickedItem.ToString() + " = " + goDelete.myName + " listObject.Count = " +
                    myListObjects.Count);

                flowLayoutPanelMyParameters.Controls.Clear();
                treeViewGameObjects.SelectedNode = null;
                testDepth = null;
            }
        }

        private void CreateGameObjectEmpty_Click(object sender, EventArgs e)
        {
            MyGameObject gameObject = new MyGameObject("Empty obj");
            MyAddTreeViewGameObject(MyInstantiateInScene(gameObject));
        }

        private void cubeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyGameObject gameObject = new MyGameObject("Cube");
            MyAddTreeViewGameObject(MyInstantiateInScene(gameObject, myPrefabCube));
        }

        private void sphereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyGameObject gameObject = new MyGameObject("Sphere");
            MyAddTreeViewGameObject(MyInstantiateInScene(gameObject, myPrefabSphere));
        }

        private void planeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyGameObject gameObject = new MyGameObject("Plane");
            MyAddTreeViewGameObject(MyInstantiateInScene(gameObject, myPrefabPlane));
        }

        private void cameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyGameObject gameObject = new MyHandleCamera(Vector3.Zero, MyGetAspectRatio());
            MyAddTreeViewGameObject(MyInstantiateInScene(gameObject));
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeViewGameObjects.SelectedNode.BeginEdit();
        }

        private void treeViewGameObjects_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null && e.Label.Length > 0)
                ((MyGameObject)treeViewGameObjects.SelectedNode.Tag).myName = e.Label;
            else
                e.CancelEdit = true;
        }
    }
}
