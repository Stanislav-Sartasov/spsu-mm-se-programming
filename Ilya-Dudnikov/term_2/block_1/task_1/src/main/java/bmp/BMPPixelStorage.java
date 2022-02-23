package bmp;

import java.nio.ByteBuffer;

public class BMPPixelStorage {
	private int[][] image;
	private int width;
	private int height;
	private int bitsPerPixel;

	public BMPPixelStorage(ByteBuffer bytes, int width, int height, short bitsPerPixel) {
		if (width % 4 != 0 && bitsPerPixel == 24)
			width += 4 - width % 4;

		int realWidth = width * (bitsPerPixel / 8);

		this.width = realWidth;
		this.height = height;
		this.bitsPerPixel = bitsPerPixel;

		image = new int[height][];

		for (int row = 0; row < height; row++) {
			for (int col = 0; col < realWidth; col++) {
				image[row][col] = bytes.get();
			}
		}
	}

	public int getPixel(int i, int j) {
		return image[i][j];
	}

	public void setPixel(int i, int j, int value) {
		image[i][j] = value;
	}

	public int getWidth() {
		return width;
	}

	public int getHeight() {
		return height;
	}

	public int getBitsPerPixel() {
		return bitsPerPixel;
	}
}
