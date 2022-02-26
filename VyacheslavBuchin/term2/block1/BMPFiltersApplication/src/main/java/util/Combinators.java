package util;

import java.util.function.Function;
import java.util.stream.DoubleStream;

public final class Combinators {
    private Combinators() {
        throw new RuntimeException("Combinators class is not supposed to be instanced");
    }

    private static int normalize(double value) {
        int result = Math.min((int)value, 255);
        result = Math.max(result, 0);
        return result;
    }

    public static final Function<DoubleStream, Integer> SUM = (values) -> normalize(values.sum());

    public static final Function<DoubleStream, Integer> MAX = (values) -> normalize(values.max().getAsDouble());

    public static final Function<DoubleStream, Integer> MIN = (values) -> normalize(values.min().getAsDouble());
}
