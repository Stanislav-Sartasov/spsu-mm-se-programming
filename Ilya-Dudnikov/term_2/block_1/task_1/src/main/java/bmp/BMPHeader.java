package bmp;

import java.nio.ByteBuffer;

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
}
