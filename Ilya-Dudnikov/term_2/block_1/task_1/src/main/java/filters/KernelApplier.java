package filters;

import Kernel.Kernel;
import bmp.BMPPixelStorage;

public class KernelApplier {
	private boolean isValidPixel(int row, int col, int imageWidth, int imageHeight) {
		return (0 <= row && row < imageHeight && 0 <= col && col < imageWidth);
	}

	private int normalizeValue(double value) {
		if (value <= 0)
			return 0;

		if (value >= 255)
			return 255;

		return (int)value;
	}

	public void applyKernel(BMPPixelStorage bmpPixelStorage, Kernel kernel) {
		int[][] oldImage = new int[bmpPixelStorage.getHeight()][];

		for (int row = 0; row < bmpPixelStorage.getHeight(); row++) {
			oldImage[row] = new int[bmpPixelStorage.getWidth()];

			for (int col = 0; col < bmpPixelStorage.getWidth(); col++) {
				oldImage[row][col] = bmpPixelStorage.getPixel(row, col);
			}
		}

		int halfKernelSize = kernel.getSize() / 2;

		int bytesPerPixel = bmpPixelStorage.getBitsPerPixel() / 8;
		for (int row = 0; row < bmpPixelStorage.getHeight(); row++) {
			for (int col = 0; col < bmpPixelStorage.getWidth(); col += bytesPerPixel) {
				for (int color = 0; color < 3; color++) {
					double newPixelValue = 0;
					for (int dy = -halfKernelSize; dy <= halfKernelSize; dy++) {
						for (int dx = -halfKernelSize; dx <= halfKernelSize; dx++) {
							if (!isValidPixel(
									row + dx,
									col + dy,
									bmpPixelStorage.getWidth(),
									bmpPixelStorage.getHeight()
							)) {
								continue;
							}

							newPixelValue += kernel.getKernelValueAt(row + dx, col + dy) * oldImage[row + dx][col + dy];
						}
					}

					bmpPixelStorage.setPixel(row, col, normalizeValue(newPixelValue));
				}
			}
		}
	}
}
