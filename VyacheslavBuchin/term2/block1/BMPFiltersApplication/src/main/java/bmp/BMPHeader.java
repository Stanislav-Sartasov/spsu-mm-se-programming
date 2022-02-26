package bmp;

public record BMPHeader(
        char[] signature,
        int fileSize,
        int reserved,
        int dataOffset,

        int size,
        int width,
        int height,
        short planes,
        short bitsPerPixel,
        int compression,
        int imageSize,
        int xPixelsPerM,
        int yPixelsPerM,
        int colorsUsed,
        int colorsImportant
) {

    public BMPHeader {
        if (signature.length != 2 || signature[0] != 'B' || signature[1] != 'M')
            throw new IllegalArgumentException("Unsupported file format");
        if (bitsPerPixel != 24 && bitsPerPixel != 32)
            throw new IllegalArgumentException(String.format("Only 32 or 24 bits per pixel are supported. This file has %d", bitsPerPixel));
    }

}


