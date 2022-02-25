package bmp;

import org.junit.jupiter.api.Test;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
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
				() -> { BMPFile.readFromInputStream(fileInputStream); }
		);
	}

	@Test
	void readFromInputStreamFail3() throws FileNotFoundException {
		FileInputStream fileInputStream = new FileInputStream("src/test/resources/less_bytes.bmp");

		assertThrows(
				IOException.class,
				() -> { BMPFile.readFromInputStream(fileInputStream); }
		);
	}

	@Test
	void writeToOutputStream() {
	}
}