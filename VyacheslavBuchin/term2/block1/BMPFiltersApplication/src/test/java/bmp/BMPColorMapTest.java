package bmp;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Nested;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class BMPColorMapTest {

    private BMPColorMap map;
    private final int width = 1;
    private final int height = 1;
    private final BMPColor color = new BMPColor(32, 22, 28, 42);

    @BeforeEach
    void setUp() {
        var array = new BMPColor[height][width];
        array[0][0] = color;
        map = new BMPColorMap(array);
    }

    @Nested
    class ConstructorTest {
        @Test
        void ConstructorShouldThrowExceptionIfGivenEmptyArray() {
            var array = new BMPColor[0][0];
            var thrown = assertThrows(IllegalArgumentException.class, () -> new BMPColorMap(array));
            assertEquals(thrown.getMessage(), "Given array must not be empty");
        }

        @Test
        void ConstructorShouldThrowExceptionIfGivenArrayIsNotRectangular() {
            var array = new BMPColor[2][];
            array[0] = new BMPColor[1];
            array[1] = new BMPColor[3];
            var thrown = assertThrows(IllegalArgumentException.class, () -> new BMPColorMap(array));
            assertEquals(thrown.getMessage(), "Given array must be rectangular");
        }
    }

    @Test
    void WidthShouldReturnCorrectWidth() {
        assertEquals(map.width(), width);
    }

    @Test
    void HeightShouldReturnCorrectHeight() {
        assertEquals(map.height(), height);
    }

    @Test
    void GetShouldReturnCorrectValue() {
        assertEquals(map.get(0, 0), color);
    }

    @Nested
    class SetTest {
        @Test
        void SetShouldChangeCorrectItem() {
            var newColor = new BMPColor(1, 1, 1, 1);
            map.set(0, 0, newColor);
            assertEquals(map.get(0, 0), newColor);
        }

        @Test
        void SetShouldReturnOldValue() {
            var newColor = new BMPColor(1, 1, 1, 1);
            assertEquals(map.set(0, 0, newColor), color);
        }
    }

    @Nested
    class ExistsTest {
        @Test
        void ExistsShouldReturnTrueIfValueExists() {
            assertTrue(map.exists(0, 0));
        }

        @Test
        void ExistsShouldReturnFalseIfValueExists() {
            assertFalse(map.exists(13, 37));
        }
    }
}