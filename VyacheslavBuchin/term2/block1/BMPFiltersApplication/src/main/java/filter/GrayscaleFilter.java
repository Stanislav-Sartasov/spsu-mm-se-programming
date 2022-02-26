package filter;

import bmp.BMPColor;
import bmp.BMPColorMap;

public class GrayscaleFilter implements IFilter {
    @Override
    public BMPColor modified(BMPColorMap map, int x, int y) {
        var pixelColor = map.get(y, x);
        int average = (pixelColor.red() + pixelColor.green() + pixelColor.blue()) / 3;
        return new BMPColor(average, average, average, pixelColor.alpha());
    }
}
