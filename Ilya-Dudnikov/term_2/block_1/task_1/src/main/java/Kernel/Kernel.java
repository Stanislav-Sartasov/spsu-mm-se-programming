package Kernel;

public class Kernel {
	private double[][] kernel;
	private int size;
	public Kernel(double[][] kernel) {
		this.kernel = kernel;
		size = kernel.length;

		if (size != 0 && (size != kernel[0].length || size % 2 != 0)) {
			System.out.println("Invalid kernel: kernel should be a square matrix with odd number of rows (columns)!");
		}
	}

	public int getSize() {
		return size;

	}
	public double getKernelValueAt(int i, int j) {
		return kernel[i][j];
	}
}
