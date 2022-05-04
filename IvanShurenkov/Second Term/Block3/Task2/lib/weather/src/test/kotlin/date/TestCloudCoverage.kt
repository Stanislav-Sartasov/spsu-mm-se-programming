package date

import lib.weather.date.CloudCoverage
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class TestCloudCoverage {
    @Test
    fun `Test init`() {
        val cloudCoverages = listOf(-1.0, 0.0, 1.0, 100.0, 1000.0)
        val ans = listOf(0.0, 0.0, 1.0, 100.0, 100.0)
        for (i in cloudCoverages.indices) {
            assertEquals(CloudCoverage(cloudCoverages[i]).percent, ans[i])
        }
    }
}