using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using OpenTK;
using OpenTK.Mathematics;

namespace WeatherUIOpenGL.Drawing.Common
{
	public class Shader
	{
		public readonly int Handle;
		public Dictionary<string, int> _uniformLocations { get; private set; }
		
		public Shader(string shaderSourceVert, string shaderSourceFrag)
		{
			var vertexShader = GL.CreateShader(ShaderType.VertexShader);
			GL.ShaderSource(vertexShader, shaderSourceVert);
			CompileShader(vertexShader, shaderSourceVert);

			var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
			GL.ShaderSource(fragmentShader, shaderSourceFrag);
			CompileShader(fragmentShader, shaderSourceFrag);

			Handle = GL.CreateProgram();

			GL.AttachShader(Handle, vertexShader);
			GL.AttachShader(Handle, fragmentShader);

			LinkProgram(Handle);

			GL.DetachShader(Handle, vertexShader);
			GL.DetachShader(Handle, fragmentShader);
			GL.DeleteShader(fragmentShader);
			GL.DeleteShader(vertexShader);

			GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

			_uniformLocations = new Dictionary<string, int>();

			for (var i = 0; i < numberOfUniforms; i++)
			{
				var key = GL.GetActiveUniform(Handle, i, out _, out _);
				var location = GL.GetUniformLocation(Handle, key);
				_uniformLocations.Add(key, location);
			}
		}

		private static void CompileShader(int shader, string text)
		{
			GL.CompileShader(shader);

			GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
			if (code != (int)All.True)
			{
				var infoLog = GL.GetShaderInfoLog(shader);
				throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
			}
		}

		public static Shader GenBasicShader()
		{
			return new Shader(File.ReadAllText("Files/shaders/basic.vert"), File.ReadAllText("Files/shaders/basic.frag"));
		}

		internal int GetUniformLocation(string name)
		{
			try
			{
				return _uniformLocations[name];
			}
			catch
			{
				return -1;
			}
		}

		private static void LinkProgram(int program)
		{
			GL.LinkProgram(program);

			GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
			if (code != (int)All.True)
			{
				throw new Exception($"Error occurred whilst linking Program({program})");
			}
		}

		public void Use()
		{
			GL.UseProgram(Handle);
		}

		public int GetAttribLocation(string attribName)
		{
			return GL.GetAttribLocation(Handle, attribName);
		}
	}
}