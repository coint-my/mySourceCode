using System;
using System.Diagnostics;
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

        //MyModel myModel;
        //MyShader myShader;

        private Timer myTimer = null;
        private float myAngle;
        private GLControl glControl;

        private Vector3 VecPos = new Vector3(0, 0, -2);


        public Form1()
        {
            InitializeComponent();
            //InitGLControl();
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

            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);

            //myModel.myTransform.myRotation = new Vector3(myAngle, 0, 0);
            //myModel.Draw(myCamera.MyGetCamera.GetViewMatrix(), myCamera.MyGetCamera.GetProjectionMatrix());

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
            numericUpDownX.Value = (decimal)VecPos.X;
            numericUpDownY.Value = (decimal)VecPos.Y;
            numericUpDownZ.Value = (decimal)VecPos.Z;
        }

        private void GlControl_Load(object sender, EventArgs e)
        {
            GL.Enable(EnableCap.DepthTest);
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

            //myShader = new MyShader("Resources/Shaders/shaderModel.vert", "Resources/Shaders/shaderModel.frag");
            //myModel = new MyModel("Resources/Models/rock/rock.obj", myShader);

            MyUpdateNumericUpDown();
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

        private void numericUpDownX_ValueChanged(object sender, EventArgs e)
        {
            float valX = (float)((NumericUpDown)sender).Value;
            VecPos.X = valX;
        }

        private void numericUpDownY_ValueChanged(object sender, EventArgs e)
        {
            float valY = (float)((NumericUpDown)sender).Value;
            VecPos.Y = valY;
        }

        private void numericUpDownZ_ValueChanged(object sender, EventArgs e)
        {
            float valZ = (float)((NumericUpDown)sender).Value;
            VecPos.Z = valZ;
        }
    }
}
