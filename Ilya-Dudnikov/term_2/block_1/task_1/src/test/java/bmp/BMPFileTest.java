package bmp;

import org.junit.jupiter.api.Test;

import java.io.*;
import java.nio.ByteBuffer;

import static org.junit.jupiter.api.Assertions.*;

class BMPFileTest {

	@Test
	void readFromInputStreamFail1() throws IOException {
		FileInputStream fileInputStream = new FileInputStream("src/test/resources/corrupted_signature.bmp");

		assertThrows(
				IllegalArgumentException.class,
				() -> { BMPFile.readFromInputStream(fileInputStream); }
		);
	}

	@Test
	void readFromInputStreamFail2() throws IOException {
		FileInputStream fileInputStream = new FileInputStream("src/test/resources/corrupted_bpp.bmp");

		assertThrows(
				IllegalArgumentException.class,
				() -> BMPFile.readFromInputStream(fileInputStream)
		);
	}

	@Test
	void readFromInputStreamFail3() throws FileNotFoundException {
		FileInputStream fileInputStream = new FileInputStream("src/test/resources/less_bytes.bmp");

		assertThrows(
				IOException.class,
				() -> BMPFile.readFromInputStream(fileInputStream)
		);
	}

	@Test
	void writeToOutputStream() throws IOException {
		FileInputStream fileInputStream = new FileInputStream("src/test/resources/ok_test.bmp");
		byte[] inputFileBytes = fileInputStream.readAllBytes();
		BMPFile bmpFile = BMPFile.readFromByteBuffer(ByteBuffer.wrap(inputFileBytes));
		fileInputStream.close();

		FileOutputStream fileOutputStream = new FileOutputStream("src/test/resources/write_output.bmp");
		bmpFile.writeToOutputStream(fileOutputStream);
		fileOutputStream.close();

		FileInputStream outputFile = new FileInputStream("src/test/resources/write_output.bmp");

		assertArrayEquals(inputFileBytes, outputFile.readAllBytes());
	}
}