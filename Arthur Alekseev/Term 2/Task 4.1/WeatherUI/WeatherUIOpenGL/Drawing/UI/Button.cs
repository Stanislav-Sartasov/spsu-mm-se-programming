using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using WeatherUIOpenGL.Drawing.Common;

namespace WeatherUIOpenGL.Drawing.UI
{
	public class Button : IRenderable
	{
		// Visual stuff
		private bool hover;
		private float animationState;
		private float animationSpeed = 0.12f;

		// Colors for states
		public Vector4 colorIdle;
		public Vector4 colorHover;

		private Action _onClick;

		// on-screen coordinates
		private int[] pixelCoords;
		private float[] glCoordinates;

		// Renderables
		public RectangleBackground rectangleBackground { get; private set; }
		public TextSprite textSprite { get; private set; }

		private App _app;
		private Vector2 previousAppSize;

		public Button(Bounds bounds, string text, Action onClick, App app)
		{
			_app = app;
			_onClick = onClick;
			pixelCoords = new int[4];
			glCoordinates = new float[] { bounds[0], bounds[1], bounds[2], bounds[3] };
			textSprite = new TextSprite(bounds, text);
			rectangleBackground = new RectangleBackground(bounds);
			rectangleBackground.Color = colorHover;

			colorIdle = new Vector4(1f, 1f, 1f, 0.6f);
			colorHover = new Vector4(1f, 1f, 1f, 0.9f);
		}
		public void Load()
		{
			rectangleBackground.Load();
			textSprite.Load();
		}

		public void Render()
		{
			rectangleBackground.Render();
			textSprite.Render();
		}

		public void Update(MouseState mouse)
		{
			if (_app.Size != previousAppSize)
			{
				UpdatePixelScale();
				previousAppSize = _app.Size;
			}
			
			if (CheckCursorOverlap(mouse.Position))
			{
				hover = true;
				if (mouse.IsButtonDown(MouseButton.Left) && !mouse.WasButtonDown(MouseButton.Left))
					_onClick.Invoke();
			}
			else
			{
				hover = false;
			}

			UpdateButtonColor(hover);
		}

		private bool CheckCursorOverlap(Vector2 cursorPosition)
		{
			return pixelCoords[0] > cursorPosition.X && pixelCoords[3] > cursorPosition.Y && pixelCoords[2] < cursorPosition.X && pixelCoords[1] < cursorPosition.Y;
		}

		private void UpdatePixelScale()
		{
			for (int i = 0; i < 4; i++)
				pixelCoords[i] = (int)((glCoordinates[i] + 1) * _app.Size[i % 2]) / 2;
			pixelCoords[1] = _app.Size.Y - pixelCoords[1];
			pixelCoords[3] = _app.Size.Y - pixelCoords[3];

			rectangleBackground.UpdateInnerResolution(new Vector2(pixelCoords[0] - pixelCoords[2], pixelCoords[3] - pixelCoords[1]));
		}


		private void UpdateButtonColor(bool isHovered)
		{
			if (isHovered)
			{
				if (animationState < 1.0f)
					animationState += animationSpeed;
			}
			else
			{
				if (animationState > 0.0f)
					animationState -= animationSpeed;
			}

			for (int i = 0; i < 4; i++)
				rectangleBackground.Color[i] = colorIdle[i] * (1 - animationState) + colorHover[i] * animationState;
		}
	}
}
