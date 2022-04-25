using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using WeatherUIOpenGL.Drawing.UI;
using WeatherUIOpenGL.Drawing.Common;
using WeatherUI.Weather;
using System.Drawing;

namespace WeatherUIOpenGL
{
	public class App : GameWindow
	{
		// Buttons 
		private Button refreshButton;
		private Button exitButton;
		private Sprite refreshSprite;
		private Sprite exitSprite;

		// Animations
		private AnimatedBackground bg;
		private float oTime;

		// WeatherData showing
		private WeatherDataLabel tomorrowIoData;
		private WeatherDataLabel openWeatherMapData;

		// Backgrounds are GPU intencive
		// I did not write these
		private string[] animatedBgSources =
		{
			"Files/shaders/bg/abstractcolors.frag",
			"Files/shaders/bg/clouds.frag",
			"Files/shaders/bg/goldenweb.frag",
			"Files/shaders/bg/colorfulcubes.frag",
			"Files/shaders/bg/colorfulstripes.frag",
			"Files/shaders/bg/fireworks.frag",
			"Files/shaders/bg/fractaltrees.frag"
		};
		
		private App(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
		{
			GL.Enable(EnableCap.Texture2D);
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

			WindowBorder = WindowBorder.Fixed;
			VSync = VSyncMode.On;
		}

		public static App Create()
		{
			var nativeWindowSettings = new NativeWindowSettings()
			{
				Size = new Vector2i(800, 500),
				Title = "Weather",
				// This is needed to run on macos
				Flags = ContextFlags.ForwardCompatible,
			};

			App wnd = new App(GameWindowSettings.Default, nativeWindowSettings);
			return wnd;
		}


		protected override void OnLoad()
		{
			base.OnLoad();
			GL.ClearColor(0.0f, 0.2f, 0.45f, 1.0f);

			refreshButton = new Button(new float[] { -0.512f, -0.8f, -0.988f, -0.975f }, "  Refresh", RefreshData, this);
			exitButton = new Button(new float[] { -0.012f, -0.8f, -0.488f, -0.975f }, "  Exit", Close, this);

			refreshSprite = new Sprite(new float[] { -0.512f, -0.8f, -0.650f, -0.975f }, Shader.GenBasicShader(), Texture.LoadFromBitmap(new Bitmap("Files/refresh.png")));
			refreshSprite.Load();
			exitSprite = new Sprite(new float[] { -0.012f, -0.8f, -0.150f, -0.975f }, Shader.GenBasicShader(), Texture.LoadFromBitmap(new Bitmap("Files/x.png")));
			exitSprite.Load();

			refreshButton.Load();
			exitButton.Load();
			tomorrowIoData = new WeatherDataLabel(new float[] { 0.988f, 0.975f, 0.006f, -0.775f }, this);
			openWeatherMapData = new WeatherDataLabel(new float[] { -0.012f, 0.975f, -0.988f, -0.775f }, this);

			tomorrowIoData.Load();

			openWeatherMapData.Load();

			// All animations except for the first one are rare
			if(new Random().Next() % 10 == 0)
				bg = new AnimatedBackground(animatedBgSources[new Random().Next() % 6 + 1]);
			else
				bg = new AnimatedBackground(animatedBgSources[0]);
			bg.Load();

			RefreshData();
		}

		private void RefreshData()
		{

			new Thread(() =>
			{
				try
				{
					var weather = new TomorrowIoParser(WebParser.GetInstance()).CollectData();
					tomorrowIoData.LoadWeatherData(weather);
				}
				catch
				{
					tomorrowIoData.LoadWeatherData(null);
				}

			}).Start();

			new Thread(() =>
			{
				try
				{
					var weather = new OpenWeatherMapParser(WebParser.GetInstance()).CollectData();
					openWeatherMapData.LoadWeatherData(weather);
				}
				catch
				{
					openWeatherMapData.LoadWeatherData(null);
				}
			}).Start();
		}


		protected override void OnRenderFrame(FrameEventArgs e)
		{
			base.OnRenderFrame(e);
			GL.Clear(ClearBufferMask.ColorBufferBit);
			bg.Render();

			
			refreshButton.Render();
			exitButton.Render();

			exitSprite.Render();
			refreshSprite.Render();

			tomorrowIoData.Render();
			openWeatherMapData.Render();
			

			SwapBuffers();
		}


		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			base.OnUpdateFrame(e);

			refreshButton.Update(MouseState);
			exitButton.Update(MouseState);

			tomorrowIoData?.Update(MouseState);
			openWeatherMapData?.Update(MouseState);

			oTime += (float)e.Time;
			bg.Update(this, oTime);
		}

		protected override void OnResize(ResizeEventArgs e)
		{
			base.OnResize(e);

			GL.Viewport(0, 0, Size.X, Size.Y);
		}
	}
}
