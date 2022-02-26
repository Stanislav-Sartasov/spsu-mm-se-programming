package filter;

import bmp.BMPColor;
import bmp.BMPColorMap;

public interface IFilter {
    BMPColor modified(BMPColorMap map, int x, int y);
}
