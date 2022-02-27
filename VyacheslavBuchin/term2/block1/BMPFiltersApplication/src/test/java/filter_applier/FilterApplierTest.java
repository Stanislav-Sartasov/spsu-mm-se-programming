package filter_applier;

import bmp.BMPColor;
import bmp.BMPColorMap;
import filter.GrayscaleFilter;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Nested;
import org.junit.jupiter.api.Test;
import util.BMPColorMaps;

import static org.junit.jupiter.api.Assertions.*;

class FilterApplierTest {

    private IFilterApplier applier;
    private BMPColorMap map;

    @BeforeEach
    void setUp() {
        var pixels = new BMPColor[1][1];
        map = new BMPColorMap(pixels);
        applier = new FilterApplier(map);
    }

    @Test
    void ToBMPColorMapShouldReturnTheSameMapIfNoAppliesPerformed() {
        map.set(0, 0, new BMPColor(0, 0, 0, 255));
        assertTrue(BMPColorMaps.equal(map, applier.toBMPColorMap()));
    }

    @Nested
    class ApplyTest {
        @Test
        void ApplyingGrayscaleFilterShouldMakeTheSameMapIfGivenMapIsMonochrome() {
            map.set(0, 0, new BMPColor(0, 0, 0, 255));
            applier.apply(new GrayscaleFilter());
            assertTrue(BMPColorMaps.equal(map, applier.toBMPColorMap()));
        }

        @Test
        void ApplyingNullFilterShouldMakeNoChangesToColorMap() {
            map.set(0, 0, new BMPColor(0, 0, 0, 255));
            applier.apply(null);
            assertTrue(BMPColorMaps.equal(map, applier.toBMPColorMap()));
        }

        @Test
        void ApplyShouldReturnApplier() {
            map.set(0, 0, new BMPColor(0, 0, 0, 255));
            assertEquals(FilterApplier.class, applier.apply(null).getClass());
        }
    }
}