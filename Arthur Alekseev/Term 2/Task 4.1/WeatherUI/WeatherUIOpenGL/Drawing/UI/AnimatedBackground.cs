using WeatherUIOpenGL.Drawing.Common;
using OpenTK.Graphics.OpenGL4;

namespace WeatherUIOpenGL.Drawing.UI
{
	public class AnimatedBackground : IRenderable
	{
		private Sprite sprite;

		public AnimatedBackground(string fragmentShaderPath)
		{
			var fshader = File.ReadAllText(fragmentShaderPath);
			var vshader = File.ReadAllText("Files/shaders/basic.vert");

			sprite = new Sprite(new Bounds(1, 1, -1, -1), new Shader(vshader, fshader), null);
		}

		public void Load()
		{
			sprite.Load();
		}

		public void Render()
		{
			sprite.Render();
		}

		public void Update(App app, float t)
		{
			sprite.Shader.Use();

			var loc = sprite.Shader.GetUniformLocation("iResolution");
			var locTime = sprite.Shader.GetUniformLocation("iTime");

			GL.Uniform2(loc, app.Size);
			GL.Uniform1(locTime, t);
		}
	}
}
