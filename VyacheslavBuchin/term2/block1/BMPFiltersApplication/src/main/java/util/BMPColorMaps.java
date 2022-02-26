package util;

import bmp.BMPColor;
import bmp.BMPColorMap;

public final class BMPColorMaps {
    private BMPColorMaps() {
        throw new RuntimeException("BMPColorMaps class is not supposed to be instanced");
    }

    public static BMPColorMap copy(BMPColorMap other) {
        int height = other.pixels().length;
        int width = other.pixels()[0].length;
        var pixels = new BMPColor[height][width];

        for (int i = 0; i < height; i++) {
            var row = pixels[i];
            for (int j = 0; j < width; j++) {
                row[j] = BMPColors.copy(
                        other.pixels()[i][j]
                );
            }
        }

        return new BMPColorMap(pixels);
    }

    public static boolean equal(BMPColorMap map1, BMPColorMap map2) {
        if (map1.width() != map2.width() ||
            map1.height() != map2.height())
            return false;

        for (int i = 0; i < map1.height(); i++) {
            for (int j = 0; j < map1.width(); j++) {
                if (!map1.get(i, j).equals(map2.get(i, j)))
                    return false;
            }
        }

        return true;
    }
}
