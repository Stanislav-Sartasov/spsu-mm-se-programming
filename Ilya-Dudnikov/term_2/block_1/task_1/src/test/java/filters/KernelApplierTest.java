package filters;

import Kernel.Kernel;
import bmp.BMPFile;
import org.junit.jupiter.api.Test;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.nio.ByteBuffer;

import static org.junit.jupiter.api.Assertions.*;

class KernelApplierTest {

	@Test
	void applyKernel() throws IOException {
		FileInputStream fileInputStream = new FileInputStream("src/test/resources/ok_test.bmp");
		BMPFile bmpFile = BMPFile.readFromByteBuffer(ByteBuffer.wrap(fileInputStream.readAllBytes()));

		Kernel kernel = Kernel.createKernel(new double[][] {
				{ 1, 3, 1 },
				{ 0, -1, 2 },
				{ 1, 1, 1 }
		});

		KernelApplier kernelApplier = new KernelApplier();
		kernelApplier.applyKernel(bmpFile.getBmpPixelStorage(), kernel);

		FileOutputStream fileOutputStream = new FileOutputStream("src/test/resources/kernel_applier_output.bmp");
		bmpFile.writeToOutputStream(fileOutputStream);
		fileInputStream.close();
		fileOutputStream.close();

		FileInputStream outputFile = new FileInputStream("src/test/resources/kernel_applier_output.bmp");
		FileInputStream correctFile = new FileInputStream("src/test/resources/correct_kernel_applied.bmp");

		assertArrayEquals(correctFile.readAllBytes(), outputFile.readAllBytes());
	}
}