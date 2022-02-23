package bmp;

import java.nio.ByteBuffer;

public class BMPPixelStorage {
	private int[][] image;

	public BMPPixelStorage(ByteBuffer bytes, int width, int height, short bitsPerPixel) {
		if (width % 4 != 0 && bitsPerPixel == 24)
			width += 4 - width % 4;

		int realWidth = width * (bitsPerPixel / 8);
		image = new int[height][];

		for (int row = 0; row < height; row++) {
			for (int col = 0; col < realWidth; col++) {
				image[row][col] = bytes.get();
			}
		}
	}
}
