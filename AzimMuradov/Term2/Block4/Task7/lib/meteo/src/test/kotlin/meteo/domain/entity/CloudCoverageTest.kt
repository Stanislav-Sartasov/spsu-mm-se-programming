package meteo.domain.entity

import meteo.domain.entity.TestUtils.INT_42
import meteo.domain.entity.TestUtils.toArguments
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.assertEquals

internal class CloudCoverageTest {

    // `CloudCoverage?` is used to prohibit inlining in the bytecode

    @ParameterizedTest
    @MethodSource("valuesWithCloudCoverages")
    fun `get cloud coverage in percent`(value: Int, cloudCoverage: CloudCoverage?) {
        assertEquals(
            expected = value,
            actual = cloudCoverage!!.percent,
        )
    }


    private companion object {

        @JvmStatic
        fun valuesWithCloudCoverages() = listOf(
            INT_42 to CloudCoverage.inPercent(value = INT_42)
        ).toArguments()
    }
}
