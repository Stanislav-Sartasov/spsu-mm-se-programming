package bmp;

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

    public BMPHeader() {}

    public BMPHeader(ByteBuffer bytes) {
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
}
