package filter;

import bmp.BMPColor;
import bmp.BMPColorMap;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import util.BMPColorMaps;

import static org.junit.jupiter.api.Assertions.*;

class GrayscaleFilterTest {

	private BMPColorMap map;

	@BeforeEach
	void setUp() {
		var pixels = new BMPColor[3][3];
		map = new BMPColorMap(pixels);
	}

	@Test
	void GrayscaleFilterShouldNotChangeBlackColor() {
		var black = new BMPColor(0, 0, 0, 255);
		map.set(0, 0, black);
		assertEquals(black, new GrayscaleFilter().modified(map, 0, 0));
	}

	@Test
	void GrayscaleFilterShouldNotChangeWhiteColor() {
		var white = new BMPColor(255, 255, 255, 255);
		map.set(0, 0, white);
		assertEquals(white, new GrayscaleFilter().modified(map, 0, 0));
	}

	@Test
	void GrayscaleFilterShouldChangeNotMonochromeColor() {
		var color = new BMPColor(15, 22, 87, 255);
		map.set(0, 0, color);
		assertNotEquals(color, new GrayscaleFilter().modified(map, 0, 0));
	}

}