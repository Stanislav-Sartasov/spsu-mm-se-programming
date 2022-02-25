package filters;

import bmp.BMPFile;
import org.junit.jupiter.api.Test;

import java.io.*;
import java.nio.ByteBuffer;
import java.util.Arrays;

import static org.junit.jupiter.api.Assertions.*;

class FiltersTest {

	@Test
	void applyAverageFilter() throws IOException {
		FileInputStream fileInputStream = new FileInputStream("src/test/resources/ok_test.bmp");

		BMPFile bmpFile = BMPFile.readFromByteBuffer(ByteBuffer.wrap(fileInputStream.readAllBytes()));

		Filters filter = new Filters();
		filter.applyAverageFilter(bmpFile.getBmpPixelStorage());

		FileOutputStream fileOutputStream = new FileOutputStream("src/test/resources/test_avg_output.bmp");
		bmpFile.writeToOutputStream(fileOutputStream);
		fileInputStream.close();
		fileOutputStream.close();

		FileInputStream outputFile = new FileInputStream("src/test/resources/test_avg_output.bmp");
		FileInputStream correctFile = new FileInputStream("src/test/resources/correct_avg.bmp");

		byte[] outputFileBytes = outputFile.readAllBytes();
		byte[] correctFileBytes = correctFile.readAllBytes();

		outputFile.close();
		correctFile.close();

		File toDelete = new File("src/test/resources/test_avg_output.bmp");
		toDelete.delete();

		assertArrayEquals(correctFileBytes, outputFileBytes);
	}

	@Test
	void applyGaussianFilter() throws IOException {
		FileInputStream fileInputStream = new FileInputStream("src/test/resources/ok_test.bmp");

		BMPFile bmpFile = BMPFile.readFromByteBuffer(ByteBuffer.wrap(fileInputStream.readAllBytes()));

		Filters filter = new Filters();
		filter.applyGaussianFilter(bmpFile.getBmpPixelStorage());

		FileOutputStream fileOutputStream = new FileOutputStream("src/test/resources/test_gauss_output.bmp");
		bmpFile.writeToOutputStream(fileOutputStream);
		fileInputStream.close();
		fileOutputStream.close();

		FileInputStream outputFile = new FileInputStream("src/test/resources/test_gauss_output.bmp");
		FileInputStream correctFile = new FileInputStream("src/test/resources/correct_gauss.bmp");

		byte[] outputFileBytes = outputFile.readAllBytes();
		byte[] correctFileBytes = correctFile.readAllBytes();

		outputFile.close();
		correctFile.close();

		File toDelete = new File("src/test/resources/test_gauss_output.bmp");
		toDelete.delete();

		assertArrayEquals(correctFileBytes, outputFileBytes);
	}

	@Test
	void applySobelXFilter() throws IOException {
		FileInputStream fileInputStream = new FileInputStream("src/test/resources/ok_test.bmp");

		BMPFile bmpFile = BMPFile.readFromByteBuffer(ByteBuffer.wrap(fileInputStream.readAllBytes()));

		Filters filter = new Filters();
		filter.applySobelXFilter(bmpFile.getBmpPixelStorage());

		FileOutputStream fileOutputStream = new FileOutputStream("src/test/resources/test_sobelX_output.bmp");
		bmpFile.writeToOutputStream(fileOutputStream);
		fileInputStream.close();
		fileOutputStream.close();

		FileInputStream outputFile = new FileInputStream("src/test/resources/test_sobelX_output.bmp");
		FileInputStream correctFile = new FileInputStream("src/test/resources/correct_sobelX.bmp");

		byte[] outputFileBytes = outputFile.readAllBytes();
		byte[] correctFileBytes = correctFile.readAllBytes();

		outputFile.close();
		correctFile.close();

		File toDelete = new File("src/test/resources/test_sobelX_output.bmp");
		toDelete.delete();

		assertArrayEquals(correctFileBytes, outputFileBytes);
	}

	@Test
	void applySobelYFilter() throws IOException {
		FileInputStream fileInputStream = new FileInputStream("src/test/resources/ok_test.bmp");

		BMPFile bmpFile = BMPFile.readFromByteBuffer(ByteBuffer.wrap(fileInputStream.readAllBytes()));

		Filters filter = new Filters();
		filter.applySobelYFilter(bmpFile.getBmpPixelStorage());

		FileOutputStream fileOutputStream = new FileOutputStream("src/test/resources/test_sobelY_output.bmp");
		bmpFile.writeToOutputStream(fileOutputStream);
		fileInputStream.close();
		fileOutputStream.close();

		FileInputStream outputFile = new FileInputStream("src/test/resources/test_sobelY_output.bmp");
		FileInputStream correctFile = new FileInputStream("src/test/resources/correct_sobelY.bmp");

		byte[] outputFileBytes = outputFile.readAllBytes();
		byte[] correctFileBytes = correctFile.readAllBytes();

		outputFile.close();
		correctFile.close();

		File toDelete = new File("src/test/resources/test_sobelY_output.bmp");
		toDelete.delete();

		assertArrayEquals(correctFileBytes, outputFileBytes);
	}

	@Test
	void applyGrayscaleFilter() throws IOException {
		FileInputStream fileInputStream = new FileInputStream("src/test/resources/ok_test.bmp");

		BMPFile bmpFile = BMPFile.readFromByteBuffer(ByteBuffer.wrap(fileInputStream.readAllBytes()));

		Filters filter = new Filters();
		filter.applyGrayscaleFilter(bmpFile.getBmpPixelStorage());

		FileOutputStream fileOutputStream = new FileOutputStream("src/test/resources/test_grayscale_output.bmp");
		bmpFile.writeToOutputStream(fileOutputStream);
		fileInputStream.close();
		fileOutputStream.close();

		FileInputStream outputFile = new FileInputStream("src/test/resources/test_grayscale_output.bmp");
		FileInputStream correctFile = new FileInputStream("src/test/resources/correct_grayscale.bmp");

		byte[] outputFileBytes = outputFile.readAllBytes();
		byte[] correctFileBytes = correctFile.readAllBytes();

		outputFile.close();
		correctFile.close();

		File toDelete = new File("src/test/resources/test_grayscale_output.bmp");
		toDelete.delete();

		assertArrayEquals(correctFileBytes, outputFileBytes);
	}
}