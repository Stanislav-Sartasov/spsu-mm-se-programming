package bmp;

import org.junit.jupiter.api.Test;

import java.io.FileInputStream;
import java.io.IOException;
import java.nio.ByteBuffer;

import static org.junit.jupiter.api.Assertions.*;

class BMPHeaderTest {
	@Test
	void readBmpHeaderSuccess() throws IOException {
		FileInputStream fileInputStream = new FileInputStream("src/test/resources/ok_test.bmp");
		BMPHeader bmpHeader = new BMPHeader(ByteBuffer.wrap(fileInputStream.readNBytes(54)));

		BMPHeader validBmpHeader = new BMPHeader(
				(char) 19778,
				2880054,
				0,
				54,
				40,
				1200,
				800,
				(short) 1,
				(short) 24,
				0,
				2880000,
				3780,
				3780,
				0,
				0
		);

		assertEquals(validBmpHeader.signature, bmpHeader.signature);
		assertEquals(validBmpHeader.fileSize, bmpHeader.fileSize);
		assertEquals(validBmpHeader.reserved, bmpHeader.reserved);
		assertEquals(validBmpHeader.dataOffset, bmpHeader.dataOffset);
		assertEquals(validBmpHeader.headerSize, bmpHeader.headerSize);
		assertEquals(validBmpHeader.width, bmpHeader.width);
		assertEquals(validBmpHeader.height, bmpHeader.height);
		assertEquals(validBmpHeader.planes, bmpHeader.planes);
		assertEquals(validBmpHeader.bitsPerPixel, bmpHeader.bitsPerPixel);
		assertEquals(validBmpHeader.compression, bmpHeader.compression);
		assertEquals(validBmpHeader.imageSize, bmpHeader.imageSize);
		assertEquals(validBmpHeader.pixelPerMeterX, bmpHeader.pixelPerMeterX);
		assertEquals(validBmpHeader.pixelPerMeterY, bmpHeader.pixelPerMeterY);
		assertEquals(validBmpHeader.colorsUsed, bmpHeader.colorsUsed);
		assertEquals(validBmpHeader.importantColors, bmpHeader.importantColors);
	}

	@Test
	void toByteArray() throws IOException {
		FileInputStream fileInputStream = new FileInputStream("src/test/resources/ok_test.bmp");
		byte[] bytes = fileInputStream.readNBytes(54);
		BMPHeader bmpHeader = new BMPHeader(ByteBuffer.wrap(bytes));

		assertArrayEquals(bytes, bmpHeader.toByteArray());
	}

	@Test
	void checkBmpHeaderSuccess() throws IOException {
		FileInputStream fileInputStream = new FileInputStream("src/test/resources/ok_test.bmp");
		BMPHeader bmpHeader = new BMPHeader(ByteBuffer.wrap(fileInputStream.readNBytes(54)));

		bmpHeader.checkBmpHeader();
	}

	@Test
	void checkBmpHeaderFail1() throws IOException {
		FileInputStream fileInputStream = new FileInputStream("src/test/resources/corrupted_signature.bmp");
		BMPHeader bmpHeader = new BMPHeader(ByteBuffer.wrap(fileInputStream.readNBytes(54)));

		assertThrows(IllegalArgumentException.class, bmpHeader::checkBmpHeader);
	}

	@Test
	void checkBmpHeaderFail2() throws IOException {
		FileInputStream fileInputStream = new FileInputStream("src/test/resources/corrupted_bpp.bmp");
		BMPHeader bmpHeader = new BMPHeader(ByteBuffer.wrap(fileInputStream.readNBytes(54)));

		assertThrows(IllegalArgumentException.class, bmpHeader::checkBmpHeader);
	}
}