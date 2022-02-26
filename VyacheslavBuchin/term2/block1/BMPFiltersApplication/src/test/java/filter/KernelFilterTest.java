package filter;

import bmp.BMPColor;
import bmp.BMPColorMap;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import util.Combinators;
import util.Kernels;

import static org.junit.jupiter.api.Assertions.*;

class KernelFilterTest {

    private KernelFilter filter;
    private BMPColorMap map;

    @BeforeEach
    void setUp() {
        var pixels = new BMPColor[3][3];
        map = new BMPColorMap(pixels);
    }

    @Test
    void IdentityFilterShouldNotChangeColor() {
        filter = new KernelFilter(Kernels.symmetricalRectangleKernel(1, 1, 1));
        var color = new BMPColor(1, 1, 1, 1);
        map.set(0, 0, color);
        assertEquals(color, filter.modified(map, 0, 0));
    }

    @Test
    void MinFilterWithBlackColorShouldReturnBlack() {
        filter = new KernelFilter(Kernels.constSymmetricalRectangleKernel(3, 3, 1), Combinators.MIN);
        var black = new BMPColor(0, 0, 0, 255);
        var white = new BMPColor(255, 255, 255, 255);
        map.set(0, 0, white);
        map.set(0, 1, black);
        map.set(1, 0, white);
        map.set(1, 1, white);
        assertEquals(black, filter.modified(map, 0, 0));
    }
}