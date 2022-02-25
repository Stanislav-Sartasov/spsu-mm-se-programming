package bmp;

import java.io.IOException;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;

public class BMPHeader {
    char signature;
    int fileSize;
    int reserved;
    int dataOffset;

    int headerSize;
    int width;
    int height;
    short planes;
    short bitsPerPixel;
    int compression;
    int imageSize;
    int pixelPerMeterX;
    int pixelPerMeterY;
    int colorsUsed;
    int importantColors;

    public BMPHeader(char signature,
                     int fileSize,
                     int reserved,
                     int dataOffset,
                     int headerSize,
                     int width,
                     int height,
                     short planes,
                     short bitsPerPixel,
                     int compression,
                     int imageSize,
                     int pixelPerMeterX,
                     int pixelPerMeterY,
                     int colorsUsed,
                     int importantColors) {
        this.signature = signature;
        this.fileSize = fileSize;
        this.reserved = reserved;
        this.dataOffset = dataOffset;
        this.headerSize = headerSize;
        this.width = width;
        this.height = height;
        this.planes = planes;
        this.bitsPerPixel = bitsPerPixel;
        this.compression = compression;
        this.imageSize = imageSize;
        this.pixelPerMeterX = pixelPerMeterX;
        this.pixelPerMeterY = pixelPerMeterY;
        this.colorsUsed = colorsUsed;
        this.importantColors = importantColors;
    }

    private BMPHeader(ByteBuffer bytes) {
        bytes.order(ByteOrder.LITTLE_ENDIAN);

        signature = bytes.getChar();
        fileSize = bytes.getInt();
        reserved = bytes.getInt();
        dataOffset = bytes.getInt();
        headerSize = bytes.getInt();
        width = bytes.getInt();
        height = bytes.getInt();
        planes = bytes.getShort();
        bitsPerPixel = bytes.getShort();
        compression = bytes.getInt();
        imageSize = bytes.getInt();
        pixelPerMeterX = bytes.getInt();
        pixelPerMeterY = bytes.getInt();
        colorsUsed = bytes.getInt();
        importantColors = bytes.getInt();
    }

    public static BMPHeader readFromByteBuffer(ByteBuffer bytes) throws IOException {
        if (bytes.remaining() < 54) {
            throw new IOException("Too few bytes in a given file!");
        }

        return new BMPHeader(bytes);
    }

    public byte[] toByteArray() {
        ByteBuffer byteBuffer = ByteBuffer.allocate(54);
        byteBuffer.order(ByteOrder.LITTLE_ENDIAN);

        byteBuffer.putChar(signature);
        byteBuffer.putInt(fileSize);
        byteBuffer.putInt(reserved);
        byteBuffer.putInt(dataOffset);
        byteBuffer.putInt(headerSize);
        byteBuffer.putInt(width);
        byteBuffer.putInt(height);
        byteBuffer.putShort(planes);
        byteBuffer.putShort(bitsPerPixel);
        byteBuffer.putInt(compression);
        byteBuffer.putInt(imageSize);
        byteBuffer.putInt(pixelPerMeterX);
        byteBuffer.putInt(pixelPerMeterY);
        byteBuffer.putInt(colorsUsed);
        byteBuffer.putInt(importantColors);

        return byteBuffer.array();
    }

    public void checkBmpHeader() {
        if (signature != 0x4D42) {
            throw new IllegalArgumentException("Given file is corrupted!");
        }

        if (bitsPerPixel != 24 && bitsPerPixel != 32) {
            throw new IllegalArgumentException("Depth of the image should either be 24 or 32 bits per pixel");
        }
    }
}
