package bmp;

import org.junit.jupiter.api.Test;

import javax.imageio.ImageIO;
import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.nio.ByteBuffer;

import static org.junit.jupiter.api.Assertions.*;

class BMPPixelStorageTest {
	@Test
	void readBmpPixels() throws IOException {
		FileInputStream fileInputStream = new FileInputStream("src/test/resources/ok_test.bmp");
		ByteBuffer byteBuffer = ByteBuffer.wrap(fileInputStream.readAllBytes());

		BMPHeader bmpHeader = BMPHeader.readFromByteBuffer(byteBuffer);
		BMPPixelStorage bmpPixelStorage = BMPPixelStorage.readFromByteBuffer(
				byteBuffer,
				bmpHeader.width,
				bmpHeader.height,
				bmpHeader.bitsPerPixel
		);

		fileInputStream.close();
		fileInputStream = new FileInputStream("src/test/resources/ok_test.bmp");

		BufferedImage image = ImageIO.read(fileInputStream);
		ImageIO.write(image, "BMP", new File("wtf.bmp"));

		int imageHeight = image.getHeight();
		int imageWidth = image.getWidth();

		for (int row = 0; row < imageHeight; row++) {
			for (int col = 0; col < imageWidth; col++) {
				Color pixelRGB = new Color(image.getRGB(col, imageHeight - row - 1));

				assertEquals(pixelRGB.getBlue(), bmpPixelStorage.getPixel(row, 3 * col));
				assertEquals(pixelRGB.getGreen(), bmpPixelStorage.getPixel(row, 3 * col + 1));
				assertEquals(pixelRGB.getRed(), bmpPixelStorage.getPixel(row, 3 * col + 2));
			}
		}
	}
}