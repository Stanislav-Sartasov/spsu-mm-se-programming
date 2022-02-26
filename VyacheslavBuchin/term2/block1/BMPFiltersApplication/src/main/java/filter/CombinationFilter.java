package filter;

import java.util.function.Function;
import java.util.stream.DoubleStream;

public abstract class CombinationFilter implements IFilter {
    protected Function<DoubleStream, Integer> combinator;

    public CombinationFilter(Function<DoubleStream, Integer> combinator) {
        this.combinator = combinator;
    }
}
