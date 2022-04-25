using OpenTK.Graphics.OpenGL4;

namespace WeatherUIOpenGL.Drawing.Common
{
	public class Sprite : IRenderable
	{
		// Buffers
		private int _vertexArrayObject;
		private int _elementBufferObject;
		private int _vertexBufferObject;

		// on screen coordinates
		private float[] _vertices { get; set; }
		public static readonly uint[] _indices =
		{
			0, 1, 3,
			1, 2, 3
		};

		public readonly Shader shader;
		public readonly Texture texture;

		public Sprite(Bounds bounds, Shader shader, Texture? texture)
		{
			_vertices = new float[20];
			this.shader = shader;
			this.texture = texture;
			ReshapeWithCoords(-bounds[0], bounds[1], -bounds[2], bounds[3]);
		}
		public void ReshapeWithCoords(float top_x, float top_y, float bottom_x, float bottom_y)
		{
			_vertices = new float[]
			{
			-top_x, top_y, 0f, 1.0f, 1.0f, // top right
            -top_x, bottom_y, 0f, 1.0f, 0.0f, // bottom right
            -bottom_x, bottom_y, 0f, 0.0f, 0.0f, // bottom left
            -bottom_x, top_y, 0f, 0.0f, 1.0f // top left
            };
		}

		public void Render()
		{
			GL.BindVertexArray(_vertexArrayObject);

			GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
			GL.BufferData(BufferTarget.ArrayBuffer, 20 * sizeof(float), _vertices,
				BufferUsageHint.DynamicDraw);

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
			GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices,
				BufferUsageHint.StaticDraw);

			if (texture != null)
				texture.Use(TextureUnit.Texture0);

			shader.Use();

			GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
		}

		public void Load()
		{
			_vertexArrayObject = GL.GenVertexArray();
			GL.BindVertexArray(_vertexArrayObject);

			_vertexBufferObject = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
			GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices,
				BufferUsageHint.DynamicDraw);

			_elementBufferObject = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
			GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices,
				BufferUsageHint.StaticDraw);

			shader.Use();
			var vertexLocation = shader.GetAttribLocation("aPosition");
			GL.EnableVertexAttribArray(vertexLocation);
			GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

			var texCoordLocation = shader.GetAttribLocation("aTexCoord");
			GL.EnableVertexAttribArray(texCoordLocation);
			GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float),
				3 * sizeof(float));
			if (texture != null)
				texture.Use(TextureUnit.Texture0);

		}
	}
}