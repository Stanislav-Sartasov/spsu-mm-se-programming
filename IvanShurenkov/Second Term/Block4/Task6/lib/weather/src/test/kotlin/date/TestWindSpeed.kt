package date

import lib.weather.date.WindSpeed
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class TestWindSpeed {
    @Test
    fun `Test init`() {
        val windSpeeds = listOf(-1.0, 0.0, 1.0, 100.0, 1000.0)
        val ans = listOf(0.0, 0.0, 1.0, 100.0, 1000.0)
        for (i in windSpeeds.indices) {
            assertEquals(WindSpeed(windSpeeds[i]).speed, ans[i])
        }
    }
}