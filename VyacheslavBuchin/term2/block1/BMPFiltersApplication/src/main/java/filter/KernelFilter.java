package filter;

import bmp.BMPColor;
import bmp.BMPColorMap;
import kernel.Kernel;
import util.Combinators;

import java.util.function.Function;
import java.util.stream.DoubleStream;

public class KernelFilter extends CombinationFilter {
    protected Kernel kernel;

    public KernelFilter(Kernel kernel) {
        this(kernel, Combinators.SUM);
    }

    public KernelFilter(Kernel kernel, Function<DoubleStream, Integer> combinator) {
        super(combinator);
        this.kernel = kernel;
    }

    @Override
    public BMPColor modified(BMPColorMap map, int x, int y) {
        var items = kernel.getItems();

        DoubleStream.Builder[] values = new DoubleStream.Builder[3];
        for (int c = 0; c < 3; c++)
            values[c] = DoubleStream.builder();

        for (var item : items) {
            int newY = y + item.dy();
            int newX = x + item.dx();
            if (!map.exists(newY, newX))
                continue;

            var currentRGB = map.get(newY, newX).RGB();
            for (int c = 0; c < 3; c++)
                values[c].add(item.coefficient() * currentRGB[c]);
        }
        return new BMPColor(
                combinator.apply(values[0].build()),
                combinator.apply(values[1].build()),
                combinator.apply(values[2].build()),
                map.get(y, x).alpha()
        );
    }
}
