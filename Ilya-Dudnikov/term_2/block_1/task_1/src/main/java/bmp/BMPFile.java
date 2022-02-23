package bmp;

import java.io.IOException;
import java.io.InputStream;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;

public class BMPFile {
	BMPHeader bmpHeader;
	BMPPixelStorage bmpPixelStorage;

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
}
