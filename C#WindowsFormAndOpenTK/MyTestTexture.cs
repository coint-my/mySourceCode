using OpenTK.Graphics.OpenGL4;
using StbImageSharp;
using System.IO;

namespace C_WindowsFormAndOpenTK
{
    public class MyTestTexture
    {
        public readonly int Handle;
        public string type;
        public string path;
        public string myName;

        public static MyTestTexture LoadFromFile(string filename, string type = "texture_diffuse")
        {
            int handle = GL.GenTexture();

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, handle);
            StbImage.stbi_set_flip_vertically_on_load(1);

            using (Stream stream = File.OpenRead(filename))
            {
                ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                    image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
            }

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (int)TextureMagFilter.Linear);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS,
                (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT,
                (int)TextureWrapMode.Repeat);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return new MyTestTexture(handle, filename, type);
        }

        public MyTestTexture(int _glHandle, string _path, string _type)
        {
            Handle = _glHandle;
            path = _path;
            type = _type;
            myName = Path.GetFileNameWithoutExtension(_path);
        }

        public void Use(TextureUnit unit)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }
    }
}
