package filters;

import Kernel.Kernel;
import bmp.BMPPixelStorage;

public class Filters {
	public void applyAverageFilter(BMPPixelStorage bmpPixelStorage) {
		Kernel kernel = new Kernel(new double[][]{{
			1.0 / 9
		}});

		KernelApplier kernelApplier = new KernelApplier();
		kernelApplier.applyKernel(bmpPixelStorage, kernel);
	}

	public void applyGaussianFilter(BMPPixelStorage bmpPixelStorage) {
		Kernel kernel = new Kernel(new double[][] {
				{ 1.0 / 16, 1.0 / 8, 1.0 / 16 },
				{ 1.0 / 8, 1.0 / 4, 1.0 / 8 },
				{ 1.0 / 16, 1.0 / 8, 1.0 / 16 }
		});

		KernelApplier kernelApplier = new KernelApplier();
		kernelApplier.applyKernel(bmpPixelStorage, kernel);
	}

	public void applySobelXFilter(BMPPixelStorage bmpPixelStorage) {
		Kernel kernel = new Kernel(new double[][] {
				{ -1, 0, 1 },
				{ -2, 0, 2 },
				{ -1, 0, 1 }
		});

		KernelApplier kernelApplier = new KernelApplier();
		kernelApplier.applyKernel(bmpPixelStorage, kernel);
	}

	public void applySobelYFilter(BMPPixelStorage bmpPixelStorage) {
		Kernel kernel = new Kernel(new double[][] {
				{ -1, -2, -1 },
				{ 0, 0, 0 },
				{ 1, 2, 1 }
		});

		KernelApplier kernelApplier = new KernelApplier();
		kernelApplier.applyKernel(bmpPixelStorage, kernel);
	}

	public void applyGrayscaleFilter(BMPPixelStorage bmpPixelStorage) {
		int bytesPerPixel = bmpPixelStorage.getBitsPerPixel() / 8;

		for (int row = 0; row < bmpPixelStorage.getHeight(); row++) {
			for (int col = 0; col < bmpPixelStorage.getWidth() - 2; col += bytesPerPixel) {
				int newValue = (bmpPixelStorage.getPixel(row, col) + bmpPixelStorage.getPixel(row, col + 1) + bmpPixelStorage.getPixel(row, col + 2)) / 3;

				bmpPixelStorage.setPixel(row, col, newValue);
				bmpPixelStorage.setPixel(row, col + 1, newValue);
				bmpPixelStorage.setPixel(row, col + 2, newValue);
			}
		}
	}
}
