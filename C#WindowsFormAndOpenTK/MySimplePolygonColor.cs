using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.Diagnostics;

namespace C_WindowsFormAndOpenTK
{
    internal class MySimplePolygonColor
    {
        private readonly float[] myVertices =
        {
            // Position         Texture coordinates
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };
        private readonly uint[] myIndices =
        {
            0, 1, 3,
            1, 2, 3
        };

        private int myVertexBufferObject;
        private int myVertexArrayObject;
        private int myElementBufferObject;

        private MyShader mySimpleShader;

        private MyTransform myTransform;

        public MySimplePolygonColor()
        {
            myTransform = new MyTransform();
            myTransform.myPosition = new Vector3(0.0f, 0.0f, 2.0f);
            myTransform.myScale = new Vector3(0.1f);

            myVertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(myVertexArrayObject);

            myVertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, myVertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, myVertices.Length * sizeof(float),
                myVertices, BufferUsageHint.StaticDraw);

            myElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, myElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, myIndices.Length * sizeof(uint),
                myIndices, BufferUsageHint.StaticDraw);

            mySimpleShader = new MyShader("Resources/Shaders/simpleShaderColor.vert",
                "Resources/Shaders/simpleShaderColor.frag");
            mySimpleShader.Use();

            var vertexLocation = mySimpleShader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float,
                false, 5 * sizeof(float), 0);
        }

        public MySimplePolygonColor(ref MyTexture _texure, int _textureData) 
        {
            Debug.WriteLine("texture = " + MyTexture.myCurrentIndex);
            myTransform = new MyTransform();
            myTransform.myPosition = new Vector3(0.0f, 0.0f, 2.0f);
            myTransform.myScale = new Vector3(0.1f);
            //myTransform.myRotation = new Vector3(0.0f, 0.0f, 0.0f);

            myVertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(myVertexArrayObject);

            myVertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, myVertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, myVertices.Length * sizeof(float),
                myVertices, BufferUsageHint.StaticDraw);

            myElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, myElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, myIndices.Length * sizeof(uint),
                myIndices, BufferUsageHint.StaticDraw);

            mySimpleShader = new MyShader("Resources/Shaders/simpleShaderTexture.vert",
                "Resources/Shaders/simpleShaderTexture.frag");
            mySimpleShader.Use();

            var vertexLocation = mySimpleShader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float,
                false, 5 * sizeof(float), 0);

            var texCoordLocation = mySimpleShader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float,
                false, 5 * sizeof(float), 3 * sizeof(float));

            mySimpleShader.SetInt("texture0", _textureData);
        }

        public void MySetScale(Vector3 _scale) => myTransform.myScale = _scale;

        public void MySetPosition(Vector3 _position)
        {
            myTransform.myPosition = _position;
        }

        public void MyDraw(MyHandleCamera _camera, Vector4 _color)
        {
            Matrix4 model = Matrix4.Identity;

            float scale = 0;
            if (_camera.myParent == null)
                scale = (_camera.myPosition - myTransform.myPosition).Length * 0.1f;
            else
                scale = (_camera.MyGetModel.ExtractTranslation() - myTransform.myPosition).Length * 0.1f;

            model = model * Matrix4.CreateScale(myTransform.myScale * scale);
            model = model * Matrix4.CreateFromQuaternion(Quaternion.FromEulerAngles(myTransform.myRotation));
            model = model * Matrix4.CreateTranslation(myTransform.myPosition);

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            mySimpleShader.Use();
            GL.Uniform4(GL.GetUniformLocation(mySimpleShader.Handle, "Color"), _color);
            mySimpleShader.SetMatrix4("view", _camera.GetViewMatrix());
            mySimpleShader.SetMatrix4("projection", _camera.GetProjectionMatrix());
            mySimpleShader.SetMatrix4("model", model);

            GL.BindVertexArray(myVertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, myIndices.Length, DrawElementsType.UnsignedInt, 0);
        }
    }
}
