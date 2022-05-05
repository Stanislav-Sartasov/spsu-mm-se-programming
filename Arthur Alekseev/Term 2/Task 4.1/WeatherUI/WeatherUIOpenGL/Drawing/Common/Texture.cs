using OpenTK.Graphics.OpenGL4;
using System.Drawing;
using System.Drawing.Imaging;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;

namespace WeatherUIOpenGL.Drawing.Common
{
	public class Texture
	{
		public int Handle { get; private set; }

		public static Texture LoadFromBitmap(Bitmap bmp)
		{
			int handle = GL.GenTexture();
			GL.ActiveTexture(TextureUnit.Texture0);
			GL.BindTexture(TextureTarget.Texture2D, handle);
			bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
			var data = bmp.LockBits(
				new Rectangle(0, 0, bmp.Width, bmp.Height),
				ImageLockMode.ReadOnly,
				System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			GL.TexImage2D(TextureTarget.Texture2D,
				0,
				PixelInternalFormat.Rgba,
				bmp.Width,
				bmp.Height,
				0,
				PixelFormat.Bgra,
				PixelType.UnsignedByte,
				data.Scan0);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
			GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
			bmp.UnlockBits(data);
			return new Texture(handle);
		}

		public Texture(int glHandle)
		{
			Handle = glHandle;
		}

		public void Use(TextureUnit unit)
		{
			GL.ActiveTexture(unit);
			GL.BindTexture(TextureTarget.Texture2D, Handle);
		}
	}
}