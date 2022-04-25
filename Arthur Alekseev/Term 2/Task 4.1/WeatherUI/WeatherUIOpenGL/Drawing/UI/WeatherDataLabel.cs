using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Drawing;
using WeatherUIOpenGL.Drawing.Common;
using WeatherUI.Weather;

namespace WeatherUIOpenGL.Drawing.UI
{
	public class WeatherDataLabel : IRenderable
	{
		// Variables for inner labels drawing
		private float offsetx = 0.012f;
		private float offsety = 0.012f;
		private float gap = 0.012f;

		// on-screen coordinates
		private float[] glCoordinates;
		private int[] pixelCoords;

		// Variables for async data loading
		private bool buttonsLoaded;
		private string dataRecieved;

		// Variables for smooth appearing animation
		private int currentLoadingNode = 0;
		private float currentLoadingAnimation = 0f;
		private float animationSpeed = 0.06f;

		private App app;

		// Renderables
		private Sprite noConnectionSprite;
		private RectangleBackground background;
		private Button[] dataLabels;

		public WeatherDataLabel(float[] coordinates, App app)
		{
			glCoordinates = coordinates;
			this.app = app;
			pixelCoords = new int[4];
			dataLabels = new Button[0];

			background = new RectangleBackground(coordinates);
			background.Color = new Vector4(1f, 1f, 1f, 0.6f);

			updatePixelScale();
			setupNoConnectionSprite();
		}

		private void setupNoConnectionSprite()
		{
			float yOffset = (glCoordinates[3] - glCoordinates[1] - 2 * offsety + gap * 6) / 8;
			float yGap = (glCoordinates[3] - glCoordinates[1]) / 4;
			float xGap = (glCoordinates[2] - glCoordinates[0]) / 3;

			noConnectionSprite = new Sprite(new float[] { glCoordinates[0] + xGap, glCoordinates[1] + yOffset + yGap, glCoordinates[2] - xGap, glCoordinates[3] - yGap }, Shader.GenBasicShader(), Texture.LoadFromBitmap(new Bitmap("Files/noconnection.png")));
		}

		public void Load()
		{
			background.Load();
			noConnectionSprite.Load();
		}

		public void LoadWeatherData(WeatherData? data)
		{
			if (data == null)
				dataRecieved = "No Data Recieved";
			else
				dataRecieved = data.ToString();

			dataLabels = new Button[dataRecieved.Split("\n").Length];
			buttonsLoaded = false;
			currentLoadingNode = 0;
		}

		public void Render()
		{
			background.Render();

			// Fill buttons when data is here
			if (!buttonsLoaded && dataLabels.Length != 0)
			{
				createLabels();
				LoadLabels();
				buttonsLoaded = true;
			}
			else
			{
				// Render all initialized labels
				foreach (var item in dataLabels)
					if (item != null)
						item.Render();
			}

			// Bad data
			if (dataLabels.Length != 8)
			{
				noConnectionSprite.Render();
			}
		}

		private void createLabels()
		{
			string[] lines = dataRecieved.ToString().Split("\n");

			float xLength = glCoordinates[2] - glCoordinates[0] + 2 * offsetx;
			float yLength = (glCoordinates[3] - glCoordinates[1] - 2 * offsety + gap * 6) / 8;

			for (int i = 0; i < dataLabels.Length; i++)
			{
				dataLabels[i] = new Button(new float[] { glCoordinates[0] - offsetx, glCoordinates[1] - offsety + ((i) * yLength) - gap, glCoordinates[0] - offsetx + xLength, glCoordinates[1] - offsety + ((i + 1) * yLength) }, lines[i], () => { }, app);
				dataLabels[i].colorIdle = new Vector4(new Vector3(1f), 0);
				dataLabels[i].colorHover = new Vector4(new Vector3(1f), 0);
				dataLabels[i].textSprite.textOpacity = 0f;
			}
		}

		private void LoadLabels()
		{
			for (int i = 0; i < dataLabels.Length; i++)
				if (dataLabels[i] != null)
					dataLabels[i].Load();
		}

		private void updatePixelScale()
		{
			for (int i = 0; i < 4; i++)
				pixelCoords[i] = (int)((glCoordinates[i] + 1) * app.Size[i % 2]) / 2;
			pixelCoords[1] = app.Size.Y - pixelCoords[1];
			pixelCoords[3] = app.Size.Y - pixelCoords[3];

			background.UpdateInnerResolution(new Vector2(pixelCoords[0] - pixelCoords[2], pixelCoords[3] - pixelCoords[1]));
		}

		public void Update(MouseState mouseState)
		{
			for (int i = 0; i < dataLabels.Length; i++)
				if (dataLabels[i] != null)
					dataLabels[i].Update(mouseState);

			if(currentLoadingNode < dataLabels.Length)
				doSmoothAnim();
		}

		private void doSmoothAnim()
		{
			if (dataLabels[currentLoadingNode] != null)
			{
				dataLabels[currentLoadingNode].colorIdle = new Vector4(new Vector3(1f), currentLoadingAnimation * 0.6f);
				dataLabels[currentLoadingNode].colorHover = new Vector4(new Vector3(1f), currentLoadingAnimation * 0.8f);
				dataLabels[currentLoadingNode].textSprite.textOpacity = currentLoadingAnimation;
				if (currentLoadingAnimation > 1f)
				{
					currentLoadingAnimation = 0f;
					currentLoadingNode++;
				}
				currentLoadingAnimation += animationSpeed;
			}
		}
	}
}
