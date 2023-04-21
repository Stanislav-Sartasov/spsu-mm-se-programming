package bmp;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;

public class BMPFile {
	private BMPHeader bmpHeader;
	private BMPPixelStorage bmpPixelStorage;

	public BMPFile(BMPHeader bmpHeader, BMPPixelStorage bmpPixelStorage) {
		this.bmpHeader = bmpHeader;
		this.bmpPixelStorage = bmpPixelStorage;
	}

	private void checkBmpHeader() throws IllegalArgumentException {
		bmpHeader.checkBmpHeader();
	}

	public static BMPFile readFromByteBuffer(ByteBuffer bytes) throws IOException {
		 bytes.order(ByteOrder.LITTLE_ENDIAN);

		 BMPHeader bmpHeader = BMPHeader.readFromByteBuffer(bytes);
		 BMPPixelStorage bmpPixelStorage = BMPPixelStorage.readFromByteBuffer(bytes, bmpHeader.width, bmpHeader.height, bmpHeader.bitsPerPixel);

		 return new BMPFile(bmpHeader, bmpPixelStorage);
	}

	public static BMPFile readFromInputStream(InputStream inputStream) throws IOException {
		byte[] bytes = new byte[0];

		try {
			bytes = inputStream.readAllBytes();
		} catch (IOException e) {
			throw new IOException("Could not read data from the file!\n", e);
		}

		if (bytes.length < 54) {
			throw new IOException("Too few bytes in a given file");
		}

		BMPFile returnValue = BMPFile.readFromByteBuffer(ByteBuffer.wrap(bytes));
		returnValue.checkBmpHeader();

		return returnValue;
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
