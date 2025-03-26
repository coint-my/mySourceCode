using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace C_WindowsFormAndOpenTK
{
    public struct Vertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 TexCoords;
    }

    public class MyMesh
    {
        public readonly int indicesCount;
        public List<MyTestTexture> textures;
        public readonly int VAO;

        public MyMesh(Span<Vertex> vertices, Span<int> indices, List<MyTestTexture> textures)
        {
            this.textures = textures;
            indicesCount = indices.Length;

            // Setup Mesh
            VAO = GL.GenVertexArray();
            int VBO = GL.GenBuffer();
            int EBO = GL.GenBuffer();

            GL.BindVertexArray(VAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * (sizeof(float) * 8), 
                ref MemoryMarshal.GetReference(vertices), BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int),
                ref MemoryMarshal.GetReference(indices), BufferUsageHint.StaticDraw);

            // Vertex positions
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false,
                Unsafe.SizeOf<Vertex>(), Marshal.OffsetOf<Vertex>(nameof(Vertex.Position)));
            // Vertex normals
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false,
                Unsafe.SizeOf<Vertex>(), Marshal.OffsetOf<Vertex>(nameof(Vertex.Normal)));
            // Vertex texture coords
            GL.EnableVertexAttribArray(2);
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 
                Unsafe.SizeOf<Vertex>(), Marshal.OffsetOf<Vertex>(nameof(Vertex.TexCoords)));

            GL.BindVertexArray(0);
        }

        public void Draw(MyShader shader)
        {
            int diffuseNr = 1;
            int specularNr = 1;
            int normalNr = 1;
            int heightNr = 1;

            for (int i = 0; i < textures.Count; i++)
            {
                GL.ActiveTexture(TextureUnit.Texture0 + i); 

                string number = "0";
                string name = textures[i].type;
                if (name == "texture_diffuse")
                    number = diffuseNr++.ToString();
                else if (name == "texture_specular")
                    number = specularNr++.ToString();
                else if (name == "texture_normal")
                    number = normalNr++.ToString(); 
                else if (name == "texture_height")
                    number = heightNr++.ToString(); 
                                                     
                GL.Uniform1(GL.GetUniformLocation(shader.Handle, (name + number)), i);
                
                GL.BindTexture(TextureTarget.Texture2D, textures[i].Handle);
            }

            GL.BindVertexArray(VAO);
            GL.DrawElements(PrimitiveType.Triangles, indicesCount, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);

            GL.ActiveTexture(TextureUnit.Texture0);
        }
    }
}
