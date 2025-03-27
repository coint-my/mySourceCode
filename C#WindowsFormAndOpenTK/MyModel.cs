using Assimp;
using AssimpMesh = Assimp.Mesh;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using OpenTK.Graphics.OpenGL;

namespace C_WindowsFormAndOpenTK
{
    public static class Extensions
    {
        public static Vector3 ConvertAssimpVector3(this Vector3D AssimpVector)
        {
            // Reinterpret the assimp vector into an OpenTK vector.
            return Unsafe.As<Vector3D, Vector3>(ref AssimpVector);
        }

        public static Matrix4 ConvertAssimpMatrix4(this Matrix4x4 AssimpMatrix)
        {
            // Take the column-major assimp matrix and convert it to a row-major OpenTK matrix.
            return Matrix4.Transpose(Unsafe.As<Matrix4x4, Matrix4>(ref AssimpMatrix));
        }
    }

    public class MyTransform
    {
        public Vector3 myPosition { get; set; }
        public Vector3 myRotation { get; set; }
        public Vector3 myScale { get; set; }

        public MyTransform() { myPosition = new Vector3(); myRotation = new Vector3(); myScale = Vector3.One; }
    }

    public class MyModel : MyComponent, IDisposable, MyIDrawable
    {
        private List<MyMesh> meshes;
        private string directory;
        private List<MyTestTexture> textures_loaded;
        private Matrix4 myModel;
        private MyShader myShader;
        private MyShader myShaderOutline;
        private MyTransform myTransform;

        public bool myIsVisible { get; set; }
        public string MyGetDirectory { get { return directory; } }

        public MyModel(string path)
        {
            textures_loaded = new List<MyTestTexture>();

            loadModel(path);
            myModel = Matrix4.Identity;
            myTransform = new MyTransform();
        }

        public MyModel(string path, MyShader _myShader, MyShader _myShaderOutline) : base()
        {
            textures_loaded = new List<MyTestTexture>();

            loadModel(path);
            myShader = _myShader;
            myShaderOutline = _myShaderOutline;
            myTransform = new MyTransform();
            myModel = Matrix4.Identity;
        }

        private void MyTransformUpdate() 
        {
            myModel = Matrix4.Identity;
            myModel = myModel * Matrix4.CreateScale(myTransform.myScale);
            myModel = myModel * Matrix4.CreateFromQuaternion(
                OpenTK.Quaternion.FromEulerAngles(myTransform.myRotation));
            myModel = myModel * Matrix4.CreateTranslation(myTransform.myPosition);
        }

        public void loadModel(string path)
        {
            // Create a new importer
            AssimpContext importer = new AssimpContext();

            LogStream logstream = new LogStream((string msg, string userData) =>
            {
                System.Diagnostics.Debug.WriteLine(msg);
            });
            logstream.Attach();

            Scene scene = importer.ImportFile(path, PostProcessSteps.Triangulate);

            if (scene == null || scene.SceneFlags.HasFlag(SceneFlags.Incomplete) || scene.RootNode == null)
            {
                Console.WriteLine("Unable to load model from: " + path);
                return;
            }

            meshes = new List<MyMesh>();

            directory = path.Substring(0, path.LastIndexOf('/'));

            ProcessNode(scene.RootNode, scene);

            importer.Dispose();
        }

        public void MyDraw(Matrix4 _view, Matrix4 _projection)
        {
            myShader.Use();

            MyTransformUpdate();
            
            myShader.SetMatrix4("model", myModel);
            myShader.SetMatrix4("view", _view);
            myShader.SetMatrix4("projection", _projection);

            foreach (MyMesh mesh in meshes)
            {
                mesh.Draw(myShader);
            }
        }

        public void MyDrawOutline(MyHandleCamera _cam)
        {
            myShaderOutline.Use();
            float len = Vector3.Distance(myTransform.myPosition, _cam.MyGetCamera.Position) * 0.001f;
            //GL.Uniform3(GL.GetUniformLocation(myShaderOutline.Handle, "outLine"), scale);

            MyTransformUpdate();
            Matrix4 myNewScaleModel = Matrix4.Identity;
            myNewScaleModel = myNewScaleModel * Matrix4.CreateScale(myTransform.myScale *
                new Vector3(1.005f + len, 1.005f + len, 1.005f + len));
            myNewScaleModel = myNewScaleModel * Matrix4.CreateFromQuaternion(
                OpenTK.Quaternion.FromEulerAngles(myTransform.myRotation));
            myNewScaleModel = myNewScaleModel * Matrix4.CreateTranslation(myTransform.myPosition);

            myShaderOutline.SetMatrix4("model", myNewScaleModel);
            myShaderOutline.SetMatrix4("view", _cam.MyGetCamera.GetViewMatrix());
            myShaderOutline.SetMatrix4("projection", _cam.MyGetCamera.GetProjectionMatrix());

            foreach (MyMesh mesh in meshes)
            {
                mesh.Draw(myShaderOutline);
            }
        }

        private void ProcessNode(Node node, Scene scene)
        {
            for (int i = 0; i < node.MeshCount; i++)
            {
                AssimpMesh mesh = scene.Meshes[node.MeshIndices[i]];
                meshes.Add(ProcessMesh(mesh, scene));
            }

            for (int i = 0; i < node.ChildCount; i++)
            {
                ProcessNode(node.Children[i], scene);
            }
        }

        private MyMesh ProcessMesh(AssimpMesh mesh, Scene scene)
        {
            List<Vertex> vertices = new List<Vertex>();
            List<int> indices = new List<int>();
            List<MyTestTexture> textures = new List<MyTestTexture>();

            for (int i = 0; i < mesh.VertexCount; i++)
            {
                Vertex vertex = new Vertex();

                vertex.Position = mesh.Vertices[i].ConvertAssimpVector3();

                if (mesh.HasNormals)
                {
                    vertex.Normal = mesh.Normals[i].ConvertAssimpVector3();
                }

                if (mesh.HasTextureCoords(0))
                {
                    Vector2 vec;
                    vec.X = mesh.TextureCoordinateChannels[0][i].X;
                    vec.Y = mesh.TextureCoordinateChannels[0][i].Y;
                    vertex.TexCoords = vec;

                }
                else vertex.TexCoords = new Vector2(0.0f, 0.0f);

                vertices.Add(vertex);
            }

            for (int i = 0; i < mesh.FaceCount; i++)
            {
                Face face = mesh.Faces[i];
                for (int j = 0; j < face.IndexCount; j++)
                    indices.Add(face.Indices[j]);
            }

            Material material = scene.Materials[mesh.MaterialIndex];

            List<MyTestTexture> diffuseMaps = loadMaterialTextures(material, TextureType.Diffuse, "texture_diffuse");
            textures.AddRange(diffuseMaps);
            // 2. specular maps
            List<MyTestTexture> specularMaps = loadMaterialTextures(material, TextureType.Specular, "texture_specular");
            textures.AddRange(specularMaps);
            // 3. normal maps
            List<MyTestTexture> normalMaps = loadMaterialTextures(material, TextureType.Height, "texture_normal");
            textures.AddRange(normalMaps);
            // 4. height maps
            List<MyTestTexture> heightMaps = loadMaterialTextures(material, TextureType.Ambient, "texture_height");
            textures.AddRange(heightMaps);

            return new MyMesh(vertices.ToArray(), indices.ToArray(), textures);
        }

        private List<MyTestTexture> loadMaterialTextures(Material mat, TextureType type, string typeName)
        {
            List<MyTestTexture> textures = new List<MyTestTexture>();

            for (int i = 0; i < mat.GetMaterialTextureCount(type); i++)
            {
                TextureSlot str;
                mat.GetMaterialTexture(type, i, out str);
                string filename = Path.Combine(directory, str.FilePath);
                bool skip = false;
                for (int j = 0; j < textures_loaded.Count; j++)
                {
                    if (textures_loaded[j].path.CompareTo(filename) == 0)
                    {
                        textures.Add(textures_loaded[j]);
                        skip = true;
                        break;
                    }
                }
                if (!skip)
                {
                    MyTestTexture texture = MyTestTexture.LoadFromFile(filename, typeName);
                    textures.Add(texture);
                    textures_loaded.Add(texture);
                }
            }
            return textures;
        }

        public void Dispose()
        {
            textures_loaded.Clear();
            meshes.Clear();
        }

        public void MySetTransform(MyTransform _tr)
        {
            myTransform = _tr;
        }
    }
}
