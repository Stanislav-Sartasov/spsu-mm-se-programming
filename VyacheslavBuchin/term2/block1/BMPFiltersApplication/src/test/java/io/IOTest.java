package io;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import util.BMPColorMaps;

import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.nio.ByteBuffer;
import java.nio.channels.FileChannel;

import static org.junit.jupiter.api.Assertions.*;

class IOTest {

    @BeforeEach
    void setUp() {
    }

    @Test
    void FileAfterReadingAndWritingShouldStaySame() {
        try (var fileIS = new FileInputStream("src/test/resources/test.bmp");
            var bmpInput = new BMPInputStream(fileIS);
            var fileOS = new FileOutputStream("src/test/resources/test_copy.bmp");
            var bmpOutput = new BMPOutputStream(fileOS)) {
            var file = bmpInput.readBMPFile();
            bmpOutput.writeBMPFile(file);

            bmpOutput.flush();
            var bmpInputCopy = new BMPInputStream(new FileInputStream("src/test/resources/test_copy.bmp"));
            var fileCopy = bmpInputCopy.readBMPFile();
            bmpInputCopy.close();

            assertTrue(BMPColorMaps.equal(file.colorMap(), fileCopy.colorMap()));

        } catch (IOException e) {
            System.err.println(e.getMessage());
        }
    }
}