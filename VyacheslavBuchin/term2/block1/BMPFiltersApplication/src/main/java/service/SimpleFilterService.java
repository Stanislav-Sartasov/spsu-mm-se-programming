package service;

import filter.GrayscaleFilter;
import filter.IFilter;
import filter.KernelFilter;
import util.Combinators;
import util.Kernels;

import java.util.HashMap;

public class SimpleFilterService implements FilterService {

    private final HashMap<String, IFilter> map = new HashMap<>();

    {
        map.put("grayscale", new GrayscaleFilter());

        map.put("average", new KernelFilter(
                Kernels.constSymmetricalRectangleKernel(3, 3, 1.0 / 9)
        ));

        double eighth = 1.0 / 8;
        double sixteenth = 1.0 / 16;
        double fourth = 1.0 / 4;
        map.put("gaussian", new KernelFilter(
                Kernels.symmetricalRectangleKernel(3, 3,
                        sixteenth, eighth, sixteenth,
                        eighth, fourth, eighth,
                        sixteenth, eighth, sixteenth
                        )
        ));

        map.put("sobelX", new KernelFilter(
                Kernels.symmetricalRectangleKernel(3, 3,
                        -1, -2, -1,
                        0, 0, 0,
                        1, 2, 1
                        )
        ));

        map.put("sobelY", new KernelFilter(
                Kernels.symmetricalRectangleKernel(3, 3,
                        -1, 0, 1,
                        -2, 0, 2,
                        -1, 0, 1
                        )
        ));

        map.put("max", new KernelFilter(
                Kernels.constSymmetricalRectangleKernel(3, 3, 1),
                Combinators.MAX
        ));

        map.put("min", new KernelFilter(
                Kernels.constSymmetricalRectangleKernel(3, 3, 1),
                Combinators.MIN
        ));
    }

    @Override
    public IFilter get(String name) {
        return map.get(name);
    }

    @Override
    public void add(String name, IFilter filter) {
        map.put(name, filter);
    }

    @Override
    public boolean exists(String name) {
        return map.containsKey(name);
    }
}
