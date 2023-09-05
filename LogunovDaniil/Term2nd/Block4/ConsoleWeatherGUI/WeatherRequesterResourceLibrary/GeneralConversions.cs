namespace WeatherRequesterResourceLibrary
{
	public static class GeneralConversions
	{
		private static WindDirections[] Directions = new WindDirections[] { 
			WindDirections.N,
			WindDirections.NE,
			WindDirections.E,
			WindDirections.SE,
			WindDirections.S,
			WindDirections.SW,
			WindDirections.W,
			WindDirections.NW
			};

		public static double ConvertTempFromCToF(double tempC)
		{
			return tempC * 9 / 5 + 32;
		}

		public static WindDirections GetDirectionFromAngle(double windAngle)
		{
			windAngle += 22.5;
			int direction = ((int)windAngle / 45) % 8;
			return Directions[direction];
		}
	}
}
