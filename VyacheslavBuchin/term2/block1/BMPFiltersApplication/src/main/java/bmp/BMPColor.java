package bmp;

public record BMPColor(int red, int green, int blue, int alpha) {

	public int[] RGB() {
		return new int[]{red, green, blue};
	}

}
