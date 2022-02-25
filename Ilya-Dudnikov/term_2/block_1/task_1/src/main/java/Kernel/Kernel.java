package Kernel;

public class Kernel {
	private double[][] kernel;
	private int size;
	private Kernel(double[][] kernel) {
		this.kernel = kernel;
		size = kernel.length;
	}

	public static Kernel createKernel(double[][] kernel) {
		int size = kernel.length;

		if (size == 0 || (size != kernel[0].length || size % 2 == 0)) {
			throw new IllegalArgumentException("Invalid kernel: kernel should be a square matrix with odd number of rows (columns)!");
		}

		for (double[] row : kernel) {
			if (row.length != size) {
				throw new IllegalArgumentException("Invalid kernel: number of columns in each row should be equal");
			}
		}

		return new Kernel(kernel);
	}

	public int getSize() {
		return size;

	}
	public double getKernelValueAt(int i, int j) {
		return kernel[i][j];
	}
}
