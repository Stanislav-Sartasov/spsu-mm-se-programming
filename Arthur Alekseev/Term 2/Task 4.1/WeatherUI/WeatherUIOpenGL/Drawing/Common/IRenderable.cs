using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherUIOpenGL.Drawing.Common
{
	public interface IRenderable
	{
		/// <summary>
		/// Render object on screen / in FBO
		/// </summary>
		public void Render();
		/// <summary>
		/// Prepare object to render
		/// </summary>
		public void Load();
	}
}
