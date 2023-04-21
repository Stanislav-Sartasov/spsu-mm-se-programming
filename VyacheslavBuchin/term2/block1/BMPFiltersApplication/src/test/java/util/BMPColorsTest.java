package util;

import bmp.BMPColor;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class BMPColorsTest {

	private BMPColor color;

	@BeforeEach
	void setUp() {
		color = new BMPColor(32, 22, 28, 42);
	}

	@Test
	void CopyShouldReturnCorrectCopy() {
		var copy = BMPColors.copy(color);
		assertEquals(color, copy);
	}
}