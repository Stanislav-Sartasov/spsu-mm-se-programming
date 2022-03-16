package util;

import bmp.BMPColor;
import bmp.BMPColorMap;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Nested;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class BMPColorMapsTest {

	private BMPColorMap map;

	@BeforeEach
	void setUp() {
		var array = new BMPColor[1][1];
		array[0][0] = new BMPColor(1, 1, 1, 1);
		map = new BMPColorMap(array);
	}

	@Nested
	class EqualsTest {

		private BMPColorMap anotherMap;

		@BeforeEach
		void setUp() {
			var array = new BMPColor[1][1];
			array[0][0] = new BMPColor(1, 1, 1, 1);
			anotherMap = new BMPColorMap(array);
		}

		@Test
		void EqualShouldReturnTrueIfGivenMapsAreEqual() {
			assertTrue(BMPColorMaps.equal(map, anotherMap));
		}

		@Test
		void EqualShouldReturnFalseIfGivenMapsAreNotEqual() {
			anotherMap.set(0, 0, new BMPColor(32, 22, 28, 42));
			assertFalse(BMPColorMaps.equal(map, anotherMap));
		}

		@Test
		void EqualShouldReturnFalseIfGivenMapsAreDifferentSized() {
			var array = new BMPColor[1][2];
			array[0][0] = new BMPColor(1, 1, 1, 1);
			array[0][1] = new BMPColor(1, 1, 1, 1);
			anotherMap = new BMPColorMap(array);
			assertFalse(BMPColorMaps.equal(map, anotherMap));

			array = new BMPColor[2][1];
			array[0][0] = new BMPColor(1, 1, 1, 1);
			array[1][0] = new BMPColor(1, 1, 1, 1);
			anotherMap = new BMPColorMap(array);
			assertFalse(BMPColorMaps.equal(map, anotherMap));
		}

	}

	@Test
	void CopyShouldCreateAValidCopy() {
		var copy = BMPColorMaps.copy(map);
		assertTrue(BMPColorMaps.equal(map, copy));
	}
}