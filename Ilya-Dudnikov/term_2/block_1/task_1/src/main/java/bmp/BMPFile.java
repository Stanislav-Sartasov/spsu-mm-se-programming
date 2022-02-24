package bmp;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;

public class BMPFile {
	private BMPHeader bmpHeader;
	private BMPPixelStorage bmpPixelStorage;

	public BMPFile(ByteBuffer bytes) {
		bytes.order(ByteOrder.LITTLE_ENDIAN);

		bmpHeader = new BMPHeader(bytes);
		bmpPixelStorage = new BMPPixelStorage(
				bytes,
				bmpHeader.width,
				bmpHeader.height,
				bmpHeader.bitsPerPixel
		);
	}

	public static BMPFile readFromInputStream(InputStream inputStream) {
		byte[] bytes = new byte[0];
		try {
			bytes = inputStream.readAllBytes();
		} catch (IOException e) {
			System.out.println("Could not read bytes from the input file!");
			e.printStackTrace();
		}

		return new BMPFile(ByteBuffer.wrap(bytes));
	}

	public BMPPixelStorage getBmpPixelStorage() {
		return bmpPixelStorage;
	}

	public void writeToOutputStream(OutputStream outputStream) {
		try {
			outputStream.write(bmpHeader.toByteArray());

			for (int row = 0; row < bmpPixelStorage.getHeight(); row++) {
				for (int col = 0; col < bmpPixelStorage.getWidth(); col++) {
					outputStream.write(bmpPixelStorage.getPixel(row, col));
				}
			}
			
			outputStream.flush();
		} catch (IOException e) {
			System.out.println("Error: could not write to a file");
		}
	}
}
