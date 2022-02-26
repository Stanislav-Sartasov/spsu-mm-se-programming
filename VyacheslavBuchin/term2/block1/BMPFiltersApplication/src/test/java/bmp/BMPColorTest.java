package bmp;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class BMPColorTest {

    private BMPColor testingColor;
    private final int red = 32;
    private final int green = 22;
    private final int blue = 28;
    private final int alpha = 42;

    @BeforeEach
    void setUp() {
        testingColor = new BMPColor(red, green, blue, alpha);
    }

    @Test
    void RGBMethodShouldReturnComponentArray() {
        var rgb = testingColor.RGB();
        assertEquals(red, rgb[0]);
        assertEquals(green, rgb[1]);
        assertEquals(blue, rgb[2]);
    }

}