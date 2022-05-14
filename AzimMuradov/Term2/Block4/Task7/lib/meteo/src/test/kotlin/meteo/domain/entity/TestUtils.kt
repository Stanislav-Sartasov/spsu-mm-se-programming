package meteo.domain.entity

import org.junit.jupiter.params.provider.Arguments.arguments

object TestUtils {

    const val INT_42 = 42

    const val DOUBLE_42 = 42.42

    const val ABSOLUTE_TOLERANCE = 0.001

    fun <A, B> List<Pair<A, B>>.toArguments() = map { (a, b) -> arguments(a, b) }
}
