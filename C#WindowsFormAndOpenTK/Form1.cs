using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
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
        private MyShader _shader;
        private MyTexture _texture;
        private MyTexture _texture2;
        private MyHandleCamera myCamera;

        MyModel myModel;
        MyShader myShaderOutline;
        MyModel myPrefabSphere;
        MyShader myShaderLight;
        MyModel myPrefabCube;
        List<MyObjectOnScene> myListObjects;
        MyTransform myBufferTransform;

        private Timer myTimer = null;
        private float myAngle;
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
            myCamera.MyMousePress(e.X, e.Y);
        }

        private void GlControl_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                myCamera.MyMouseMove(e.X, e.Y);                
            }
        }

        private void MyUpdate(object sender, EventArgs e)
        {
            myCamera.MyUpdateCamera();

            MyKeyDown();

            MyRender();
        }

        private void MyRender()
        {
            GL.ClearColor(0.2f, 0.2f, 0.2f, 0.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _texture.Use(TextureUnit.Texture0);
            _texture2.Use(TextureUnit.Texture1);
            _shader.Use();

            var model = Matrix4.Identity;
            myAngle += 0.1f;
            model = model * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(myAngle));
            model = model * Matrix4.CreateScale(1.1f);
            model = model * Matrix4.CreateTranslation(VecPos);
            _shader.SetMatrix4("model", model);
            _shader.SetMatrix4("view", myCamera.MyGetCamera.GetViewMatrix());
            _shader.SetMatrix4("projection", myCamera.MyGetCamera.GetProjectionMatrix());

            float greenValue = ((float)Math.Sin(MathHelper.DegreesToRadians(myAngle * 20)) / 2) + 0.6f;
            int vertexColorLocation = GL.GetUniformLocation(_shader.Handle, "ourColor");
            GL.Uniform4(vertexColorLocation, 0.0f, greenValue, 0.0f, 1.0f);

            GL.BindVertexArray(_vertexArrayObject);

            GL.Enable(EnableCap.DepthTest);
            //GL.StencilFunc(StencilFunction.Always, 1, 0xFF);
            //GL.StencilMask(0xFF);

            myShaderOutline.Use();
            GL.Uniform1(GL.GetUniformLocation(myShaderOutline.Handle, "outLine"), 1.05f);
            myShaderOutline.SetMatrix4("model", model);
            myShaderOutline.SetMatrix4("view", myCamera.MyGetCamera.GetViewMatrix());
            myShaderOutline.SetMatrix4("projection", myCamera.MyGetCamera.GetProjectionMatrix());
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);

            //GL.StencilFunc(StencilFunction.Notequal, 1, 0xFF);
            //GL.StencilMask(0x00);
            GL.Disable(EnableCap.DepthTest);

            _shader.Use();
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);

            //GL.StencilMask(0xFF);
            //GL.StencilFunc(StencilFunction.Always, 0, 0xFF);
            GL.Enable(EnableCap.DepthTest);

            //myModel.myTransform.myRotation = new Vector3(0, myAngle, 0);
            //myModel.Draw(myCamera.MyGetCamera.GetViewMatrix(), myCamera.MyGetCamera.GetProjectionMatrix());

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

            MyObjectOnScene testDepth = null;

            for (int i = 0; i < myListObjects.Count; i++)
            {
                MyObjectOnScene temp = (MyObjectOnScene)listBoxGameObjects.SelectedItem;
                if (temp == myListObjects[i] && temp.myId == myListObjects[i].myId)
                {
                    testDepth = temp;
                }
                else
                    myListObjects[i].MyDraw(myCamera.MyGetCamera.GetViewMatrix(),
                        myCamera.MyGetCamera.GetProjectionMatrix());
            }

            if (testDepth != null)
            {
                GL.Disable(EnableCap.DepthTest);
                testDepth.MyDrawOutline(myCamera);
                GL.Enable(EnableCap.DepthTest);
                testDepth.MyDraw(myCamera.MyGetCamera.GetViewMatrix(),
                    myCamera.MyGetCamera.GetProjectionMatrix());
            }

            glControl.SwapBuffers();
        }

        private void MyKeyDown()
        {
            if (myKeyPress[87])
            {
                myCamera.MyDoMovementKeyboard(MyDirection.FORWARD);
            }
            if (myKeyPress[83])
            {
                myCamera.MyDoMovementKeyboard(MyDirection.BACKWARD);
            }
            if (myKeyPress[65])
            {
                myCamera.MyDoMovementKeyboard(MyDirection.LEFT);
            }
            if (myKeyPress[68])
            {
                myCamera.MyDoMovementKeyboard(MyDirection.RIGHT);
            }
            if (myKeyPress[32])
            {
                myCamera.MyDoMovementKeyboard(MyDirection.UP);
            }
            if (myKeyPress[17])
            {
                myCamera.MyDoMovementKeyboard(MyDirection.DOWN);
            }
        }

        private void MyUpdateNumericUpDown()
        {
            numericPositionX.Value = (decimal)myBufferTransform.myPosition.X;
            numericPositionY.Value = (decimal)myBufferTransform.myPosition.Y;
            numericPositionZ.Value = (decimal)myBufferTransform.myPosition.Z;

            numericRotationX.Value = (decimal)myBufferTransform.myRotation.X;
            numericRotationY.Value = (decimal)myBufferTransform.myRotation.Y;
            numericRotationZ.Value = (decimal)myBufferTransform.myRotation.Z;

            numericScaleX.Value = (decimal)myBufferTransform.myScale.X;
            numericScaleY.Value = (decimal)myBufferTransform.myScale.Y;
            numericScaleZ.Value = (decimal)myBufferTransform.myScale.Z;
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

            myCamera = new MyHandleCamera(Vector3.UnitZ * 3, glControl.Width / (float)glControl.Height);

            myShaderOutline = new MyShader("Resources/Shaders/shaderOutline.vert", 
                                    "Resources/Shaders/shaderOutline.frag");
            myShaderLight = new MyShader("Resources/Shaders/shaderModel.vert",
                "Resources/Shaders/shaderLighting.frag");
            myModel = new MyModel("Resources/Models/rock/rock.obj", myShaderLight, myShaderOutline);
            myPrefabSphere = new MyModel("Resources/Models/sphere/sphere1.FBX", myShaderLight, myShaderOutline);
            myPrefabCube = new MyModel("Resources/Models/cub/cub.FBX", myShaderLight, myShaderOutline);

            myBufferTransform = new MyTransform();
            myListObjects = new List<MyObjectOnScene>();
            MyGameObject go = new MyGameObject();
            MyGameObject go2 = new MyGameObject();
            MyGameObject go3 = new MyGameObject();
            MyGameObject go4 = new MyGameObject();
            go.MyAddComponent(myModel);
            go2.myTransform.myPosition = new Vector3(0, 2, 0);
            go2.MyAddComponent(myModel);
            go3.MyAddComponent(myPrefabSphere);
            go4.MyAddComponent(myPrefabCube);
            myListObjects.Add(go);
            myListObjects.Add(go2);
            myListObjects.Add(go3);
            myListObjects.Add(go4);
            for (int i = 0; i < myListObjects.Count; i++)
            {
                myListObjects[i].MyInitialize();
                listBoxGameObjects.Items.Add(myListObjects[i]);
            }
        }

        private void GlControl_Paint(object sender, PaintEventArgs e)
        {
            MyRender();
        }

        private void GlControl_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, glControl.Width, glControl.Height);

            if (myCamera != null)
                myCamera.MyGetCamera.AspectRatio = glControl.Width / (float)glControl.Height;
        }

        private void listBoxGameObjects_SelectedValueChanged(object sender, EventArgs e)
        {
            if (((MyGameObject)listBoxGameObjects.SelectedItem) != null)
            {
                MyGameObject myGameObject = ((MyGameObject)listBoxGameObjects.SelectedItem);
                myBufferTransform = myGameObject.myTransform;
                MyUpdateNumericUpDown();

                MyCheckParameterModel(myGameObject);
            }
        }

        private void MyCheckParameterModel(MyGameObject _myGameObject)
        {
            if (flowLayoutPanelMyParameters.Controls.Count > 0)
                flowLayoutPanelMyParameters.Controls.Clear();

            MyModel model = ((MyModel)_myGameObject.MyGetComponents[0]);
            GroupBox gBox = MyCreateGroupBox(_myGameObject.myName, "Model " + model.MyGetDirectory);
            FlowLayoutPanel flow = MyCreateFlowLayoutPanel();
            gBox.Controls.Add(flow);
            CheckBox checkBoxVisible = MyCreateCheckBox("IsVisible", _myGameObject, 
                _myGameObject.myIsVisible, CheckBox_IsVisible);
            CheckBox checkBoxWireframe = MyCreateCheckBox("IsWireframe", _myGameObject,
                _myGameObject.myIsWireframe, CheckBox_IsWireframe);
            Button button = new Button();
            flow.Controls.Add(checkBoxVisible);
            flow.Controls.Add(checkBoxWireframe);
            flow.Controls.Add(button);
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
            CheckBox checkBox = ((CheckBox)sender);
            ((MyGameObject)checkBox.Tag).myIsWireframe = checkBox.Checked;
        }

        private void CheckBox_IsVisible(object sender, EventArgs e)
        {
            CheckBox checkBox = ((CheckBox)sender);
            ((MyGameObject)checkBox.Tag).myIsVisible = checkBox.Checked;
        }

        private GroupBox MyCreateGroupBox(string _nameGameObject, string _nameComponent)
        {
            GroupBox groupBox = new GroupBox();
            groupBox.Text = "(" + _nameGameObject + ") Model " + 
                _nameComponent.Substring(_nameComponent.LastIndexOf('/') + 1);
            groupBox.MinimumSize = new Size(220, 50);
            groupBox.AutoSize = true;

            flowLayoutPanelMyParameters.Controls.Add(groupBox);
            return groupBox;
        }

        private FlowLayoutPanel MyCreateFlowLayoutPanel()
        {
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.AutoSize = true;
            flowLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel.Location = new Point(1, 40);
            flowLayoutPanel.BorderStyle = BorderStyle.FixedSingle;
            return flowLayoutPanel;
        }

        private void numericUpDownX_ValueChanged(object sender, EventArgs e)
        {
            Vector3 v3 = myBufferTransform.myPosition;
            v3.X = (float)((NumericUpDown)sender).Value;
            myBufferTransform.myPosition = v3;
        }

        private void numericUpDownY_ValueChanged(object sender, EventArgs e)
        {
            Vector3 v3 = myBufferTransform.myPosition;
            v3.Y = (float)((NumericUpDown)sender).Value;
            myBufferTransform.myPosition = v3;
        }

        private void numericUpDownZ_ValueChanged(object sender, EventArgs e)
        {
            Vector3 v3 = myBufferTransform.myPosition;
            v3.Z = (float)((NumericUpDown)sender).Value;
            myBufferTransform.myPosition = v3;
        }

        private void numericRotationX_ValueChanged(object sender, EventArgs e)
        {
            Vector3 v3 = myBufferTransform.myRotation;
            v3.X = (float)((NumericUpDown)sender).Value;
            myBufferTransform.myRotation = v3;
        }

        private void numericRotationY_ValueChanged(object sender, EventArgs e)
        {
            Vector3 v3 = myBufferTransform.myRotation;
            v3.Y = (float)((NumericUpDown)sender).Value;
            myBufferTransform.myRotation = v3;
        }

        private void numericRotationZ_ValueChanged(object sender, EventArgs e)
        {
            Vector3 v3 = myBufferTransform.myRotation;
            v3.Z = (float)((NumericUpDown)sender).Value;
            myBufferTransform.myRotation = v3;
        }

        private void numericScaleX_ValueChanged(object sender, EventArgs e)
        {
            Vector3 v3 = myBufferTransform.myScale;
            v3.X = (float)((NumericUpDown)sender).Value;
            myBufferTransform.myScale = v3;
        }

        private void numericScaleY_ValueChanged(object sender, EventArgs e)
        {
            Vector3 v3 = myBufferTransform.myScale;
            v3.Y = (float)((NumericUpDown)sender).Value;
            myBufferTransform.myScale = v3;
        }

        private void numericScaleZ_ValueChanged(object sender, EventArgs e)
        {
            Vector3 v3 = myBufferTransform.myScale;
            v3.Z = (float)((NumericUpDown)sender).Value;
            myBufferTransform.myScale = v3;
        }
    }
}
