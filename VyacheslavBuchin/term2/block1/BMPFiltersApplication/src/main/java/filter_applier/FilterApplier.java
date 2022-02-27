package filter_applier;

import bmp.BMPColorMap;
import filter.IFilter;
import util.BMPColorMaps;

public class FilterApplier implements IFilterApplier {

    private BMPColorMap map;

    public FilterApplier(BMPColorMap map) {
        this.map = map;
    }

    @Override
    public IFilterApplier apply(IFilter filter) {

        if (filter == null)
            return this;

        var workingMap = BMPColorMaps.copy(map);

        for (int i = 0; i < workingMap.height(); i++) {
            for (int j = 0; j < workingMap.width(); j++) {
                workingMap.set(i, j, filter.modified(map, j, i));
            }
        }

        map = BMPColorMaps.copy(workingMap);

        return this;
    }

    @Override
    public BMPColorMap toBMPColorMap() {
        return map;
    }
}
