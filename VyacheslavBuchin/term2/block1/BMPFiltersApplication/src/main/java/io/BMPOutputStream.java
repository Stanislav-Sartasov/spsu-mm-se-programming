package io;

import bmp.BMPColorMap;
import bmp.BMPFile;
import bmp.BMPHeader;

import java.io.BufferedOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;

public class BMPOutputStream extends BufferedOutputStream {

    public BMPOutputStream(OutputStream out) {
        super(out);
    }

    private void writeInt(int number) throws IOException {
        byte[] bytes = ByteBuffer.allocate(4).order(ByteOrder.LITTLE_ENDIAN).putInt(number).array();
        for (byte b : bytes)
            write(b);
    }

    private void writeShort(short number) throws IOException {
        byte[] bytes = ByteBuffer.allocate(2).order(ByteOrder.LITTLE_ENDIAN).putShort(number).array();
        for (byte b : bytes)
            write(b);
    }

    private void writeBMPHeader(BMPHeader header) throws IOException {
        write(header.signature()[0]);
        write(header.signature()[1]);
        writeInt(header.fileSize());
        writeInt(header.reserved());
        writeInt(header.dataOffset());
        writeInt(header.size());
        writeInt(header.width());
        writeInt(header.height());
        writeShort(header.planes());
        writeShort(header.bitsPerPixel());
        writeInt(header.compression());
        writeInt(header.imageSize());
        writeInt(header.xPixelsPerM());
        writeInt(header.yPixelsPerM());
        writeInt(header.colorsUsed());
        writeInt(header.colorsImportant());
    }

    private void writeColorMap(int bytesPerColor, int width, int height, BMPColorMap colorMap) throws IOException {
        for (int i = 0; i < height; i++) {
            for (int j = 0; j < width; j++) {
                var color = colorMap.get(i, j);
                write(color.red());
                write(color.green());
                write(color.blue());
                if (bytesPerColor == 4)
                    write(color.alpha());
            }

            int gapBytes = (4 - width * bytesPerColor % 4) % 4;
            for (int j = 0; j < gapBytes; j++) {
                write(0);
            }
        }
    }

    public void writeBMPFile(BMPFile file) throws IOException {
        if (file == null)
            return;

        var header = file.header();
        writeBMPHeader(header);

        writeColorMap(
                header.bitsPerPixel() / 24,
                header.width(),
                header.height(),
                file.colorMap()
        );

    }
}
