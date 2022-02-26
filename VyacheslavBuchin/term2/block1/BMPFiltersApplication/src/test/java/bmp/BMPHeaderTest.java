package bmp;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class BMPHeaderTest {

    @BeforeEach
    void setUp() {
    }

    @Test
    void ConstructorShouldThrowExceptionIfSignatureIsNotCorrect() {
        var thrown = assertThrows(IllegalArgumentException.class,
                () -> new BMPHeader(
                    new char[2], 0, 0, 0, 0, 0, 0, (short) 0, (short) 24, 0, 0, 0, 0, 0, 0
                )
        );
        assertEquals(thrown.getMessage(), "Unsupported file format");
    }

    @Test
    void ConstructorShouldThrowExceptionIfPixelDepthIsNotCorrect() {
        var signature = new char[] { 'B', 'M' };
        var thrown = assertThrows(IllegalArgumentException.class,
                () -> new BMPHeader(
                        signature, 0, 0, 0, 0, 0, 0, (short) 0, (short) 16, 0, 0, 0, 0, 0, 0
                )
        );
        assertEquals(thrown.getMessage(), "Only 32 or 24 bits per pixel are supported. This file has 16");
    }
}