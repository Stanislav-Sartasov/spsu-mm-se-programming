using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using WeatherUIOpenGL.Drawing.Common;

namespace WeatherUIOpenGL.Drawing.UI
{
	public class RectangleBackground : IRenderable
	{
        // Renderables
        private Sprite sprite;
        
        // Data passed to shader
        public Vector4 Color;
        private Vector2 innerResolution;

        public RectangleBackground(Bounds bounds)
		{
            var shaderTV = File.ReadAllText("Files/shaders/basic.vert");
            var shaderTF = File.ReadAllText("Files/shaders/rectangle.frag");

            Shader shader = new Shader(shaderTV, shaderTF);
            sprite = new Sprite(bounds, shader, null);

            Color = new Vector4(1, 0.5f, 0.7f, 0.8f);
        }

		public void Render()
		{
            sprite.shader.Use();

            var locationResolution = sprite.shader.GetUniformLocation("innerResolution");
            var locationColor = sprite.shader.GetUniformLocation("color");

            GL.Uniform2(locationResolution, innerResolution);
            GL.Uniform4(locationColor, Color);

			sprite.Render();
		}

		public void Load()
		{
            sprite.Load();
		}

        public void UpdateInnerResolution(Vector2 newSize)
		{
            innerResolution = newSize;
        }
	}
}
