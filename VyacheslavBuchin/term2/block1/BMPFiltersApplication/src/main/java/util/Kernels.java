package util;

import kernel.Kernel;
import kernel.KernelItem;

public final class Kernels {
    private Kernels() {
        throw new RuntimeException("Kernels class is not supposed to be instanced");
    }

    public static Kernel symmetricalRectangleKernel(int width, int height, double... coefficients) {
        if (coefficients.length != width * height)
            throw new IllegalArgumentException("Number of coefficients must be equal to width * height");
        if (width % 2 != 1 || height % 2 != 1)
            throw new IllegalArgumentException("Width and height must be odd numbers");

        var items = new KernelItem[width * height];
        for (int i = 0; i < height; i++) {
            for (int j = 0; j < width; j++) {
                int ind = i * height + j;
                items[ind] = new KernelItem(j - width / 2, height / 2 - i, coefficients[ind]);
            }
        }
        return new Kernel(items);
    }

    public static Kernel constSymmetricalRectangleKernel(int width, int height, double coefficient) {
        var coefficients = new double[width * height];
        for (int i = 0; i < width * height; i++)
            coefficients[i] = coefficient;
        return symmetricalRectangleKernel(width, height, coefficients);
    }
}
