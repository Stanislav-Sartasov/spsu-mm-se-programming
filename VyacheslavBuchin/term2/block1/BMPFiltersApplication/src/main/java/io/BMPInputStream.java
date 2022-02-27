package io;

import bmp.BMPColor;
import bmp.BMPColorMap;
import bmp.BMPFile;
import bmp.BMPHeader;

import java.io.BufferedInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;

public class BMPInputStream extends BufferedInputStream {

	public BMPInputStream(InputStream in) {
		super(in);
	}

	private int readInt() throws IOException {
		return ByteBuffer.wrap(readNBytes(4)).order(ByteOrder.LITTLE_ENDIAN).getInt();
	}

	private short readShort() throws IOException {
		return ByteBuffer.wrap(readNBytes(2)).order(ByteOrder.LITTLE_ENDIAN).getShort();
	}

	private BMPHeader readBMPHeader() throws IOException {
		char[] signature = {(char) read(), (char) read()};
		return new BMPHeader(
				signature,
				readInt(),
				readInt(),
				readInt(),
				readInt(),
				readInt(),
				readInt(),
				readShort(),
				readShort(),
				readInt(),
				readInt(),
				readInt(),
				readInt(),
				readInt(),
				readInt()
		);
	}

	private BMPColorMap readColorMap(int bytesPerColor, int width, int height) throws IOException {
		var colorMap = new BMPColor[height][width];

		for (int i = 0; i < height; i++) {
			var row = colorMap[i];
			for (int j = 0; j < width; j++) {
				var color = new BMPColor(
						read(),
						read(),
						read(),
						bytesPerColor == 4 ? read() : 255);

				row[j] = color;
			}

			int gapBytes = (4 - width * bytesPerColor % 4) % 4;
			for (int j = 0; j < gapBytes; j++)
				read();
		}

		return new BMPColorMap(colorMap);
	}

	public BMPFile readBMPFile() throws IOException {
		var header = readBMPHeader();

		var colorMap = readColorMap(
				header.bitsPerPixel() / 24,
				header.width(),
				header.height()
		);

		return new BMPFile(header, colorMap);
	}
}
