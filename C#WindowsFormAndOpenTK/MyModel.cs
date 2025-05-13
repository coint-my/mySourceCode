using Assimp;
using AssimpMesh = Assimp.Mesh;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;
using System.Xml.Serialization;

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
    
    public class MyModel : MyComponent, IDisposable, MyIDrawable
    {
        private List<MyMesh> meshes;
        private string directory;

        public string myShaderPathVert;
        public string myShaderPathFrag;
        //[XmlIgnore]
        //public MyShader myShader;
        private MyShader myShaderOutline;
        public string myPrefab;
        //public Vector3 myTexCoord { get; set; }
        public bool MyIsVisible { get; set; }
        public string MyGetDirectory { get { return directory; } }
        [XmlIgnore]
        public Material MyMaterial { get; set; }

        //[XmlIgnore]
        //public MyTestTexture MyGetTexture 
        //{ 
        //    get 
        //    {
        //        if (meshes[0].textures.Count > 0)
        //            return meshes[0].textures[0];
        //        return null;
        //    }
        //    set { meshes[0].textures[0] = value; }
        //}

        public MyModel() 
        {
            
        }

        public MyModel(string path)
        {

            myPrefab = path;
            loadModel(path);
            //myTexCoord = Vector3.One;
        }

        public MyModel(string path, MyShader _myShader, MyShader _myShaderOutline) : base()
        {
            myPrefab = path;
            loadModel(path);
            myShaderPathVert = _myShader.MyGetPathVert;
            myShaderPathFrag = _myShader.MyGetPathFrag;
            //myShader = _myShader;
            myShaderOutline = _myShaderOutline;
            //myTexCoord = Vector3.One;
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

        public void MyDraw(Matrix4 _myModel, MyHandleCamera _cam, MyShader _myShader)
        {
            _myShader.Use();

            _myShader.SetVector3("myUV", _myShader.myTexCoord);

            _myShader.SetMatrix4("model", _myModel);
            _myShader.SetMatrix4("view", _cam.GetViewMatrix());
            _myShader.SetMatrix4("projection", _cam.GetProjectionMatrix());

            foreach (MyMesh mesh in meshes)
            {
                mesh.Draw(_myShader);
            }
        }

        public void MyDrawOutline(MyGameObject _myGo, MyHandleCamera _cam)
        {
            myShaderOutline.Use();
            float len = Vector3.Distance(_myGo.myPosition, _cam.myPosition) * 0.001f;
            //GL.Uniform3(GL.GetUniformLocation(myShaderOutline.Handle, "outLine"), scale);

            Matrix4 myNewScaleModel;
            Vector3 newScale = new Vector3(1.005f + len, 1.005f + len, 1.005f + len);
            _myGo.MyTransformUpdate(out myNewScaleModel, _myGo.myScale * newScale, 
                _myGo.myRotation, _myGo.myPosition, _myGo.myPivot);

            myShaderOutline.SetMatrix4("model", myNewScaleModel);
            myShaderOutline.SetMatrix4("view", _cam.GetViewMatrix());
            myShaderOutline.SetMatrix4("projection", _cam.GetProjectionMatrix());

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
            //List<MyTestTexture> textures = new List<MyTestTexture>();

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

            MyMaterial = scene.Materials[mesh.MaterialIndex];

            //List<MyTestTexture> diffuseMaps = loadMaterialTextures(MyMaterial, TextureType.Diffuse, "texture_diffuse");
            //textures.AddRange(diffuseMaps);
            //// 2. specular maps
            //List<MyTestTexture> specularMaps = loadMaterialTextures(MyMaterial, TextureType.Specular, "texture_specular");
            //textures.AddRange(specularMaps);
            //// 3. normal maps
            //List<MyTestTexture> normalMaps = loadMaterialTextures(MyMaterial, TextureType.Height, "texture_normal");
            //textures.AddRange(normalMaps);
            //// 4. height maps
            //List<MyTestTexture> heightMaps = loadMaterialTextures(MyMaterial, TextureType.Ambient, "texture_height");
            //textures.AddRange(heightMaps);

            CenterModelPivot(vertices);

            //if(textures.Count == 0)
            //{
            //    textures.Add(MyTestTexture.LoadFromFile("Resources/Textures/myWhite_8_8.jpg"));
            //}

            return new MyMesh(vertices.ToArray(), indices.ToArray()/*, textures*/);
        }

        public void CenterModelPivot(List<Vertex> _listVert)
        {
            Vector3 min = _listVert[0].Position;
            Vector3 max = _listVert[0].Position;

            foreach (var vertex in _listVert)
            {
                min = Vector3.ComponentMin(min, vertex.Position);
                max = Vector3.ComponentMax(max, vertex.Position);
            }

            Vector3 center = (min + max) / 2;

            for (int i = 0; i < _listVert.Count; i++)
            {
                Vertex vert = _listVert[i];
                vert.Position -= center;
                _listVert[i] = vert;
            }
        }

        public List<MyTestTexture> MyGetMaterialsTextures()
        {
            List<MyTestTexture> textures = new List<MyTestTexture>();

            List<MyTestTexture> diffuseMaps = loadMaterialTextures(MyMaterial, TextureType.Diffuse, "texture_diffuse");
            textures.AddRange(diffuseMaps);
            // 2. specular maps
            List<MyTestTexture> specularMaps = loadMaterialTextures(MyMaterial, TextureType.Specular, "texture_specular");
            textures.AddRange(specularMaps);
            // 3. normal maps
            List<MyTestTexture> normalMaps = loadMaterialTextures(MyMaterial, TextureType.Height, "texture_normal");
            textures.AddRange(normalMaps);
            // 4. height maps
            List<MyTestTexture> heightMaps = loadMaterialTextures(MyMaterial, TextureType.Ambient, "texture_height");
            textures.AddRange(heightMaps);

            if (textures.Count == 0)
            {
                textures.Add(MyTestTexture.LoadFromFile("Resources\\Textures//myWhite_8_8.jpg"));
            }

            return textures;
        }

        private List<MyTestTexture> loadMaterialTextures(Material mat, TextureType type, string typeName)
        {
            List<MyTestTexture> textures = new List<MyTestTexture>();
            List<MyTestTexture> textures_loaded = new List<MyTestTexture>();

            for (int i = 0; i < mat.GetMaterialTextureCount(type); i++)
            {
                TextureSlot str;
                mat.GetMaterialTexture(type, i, out str);
                string newDIrectory = directory.Replace("/", "\\");
                string filename = newDIrectory + "//" + str.FilePath;//Path.Combine(directory, str.FilePath);
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
            meshes.Clear();
        }
    }
}
