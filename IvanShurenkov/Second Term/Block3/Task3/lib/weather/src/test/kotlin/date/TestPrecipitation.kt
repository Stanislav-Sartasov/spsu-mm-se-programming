package date

import lib.weather.date.Precipitation
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class TestPrecipitation {
    @Test
    fun `Test init`() {
        val precipitations = listOf(-1.0, 0.0, 1.0, 100.0, 1000.0)
        val ans = listOf(0.0, 0.0, 1.0, 100.0, 1000.0)
        for (i in precipitations.indices) {
            assertEquals(Precipitation(precipitations[i]).mmPerHour, ans[i])
        }
    }
}