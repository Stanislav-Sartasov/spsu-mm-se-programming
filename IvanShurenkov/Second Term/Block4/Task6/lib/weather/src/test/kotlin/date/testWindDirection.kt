package date

import lib.weather.date.WindDirection
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class testWindDirection {
    @Test
    fun `Test init`() {
        val degrees = listOf(-1.0, 0.0, 1.0, 100.0, 1000.0)
        val ans = listOf(0.0, 0.0, 1.0, 100.0, 360.0)
        for (i in degrees.indices) {
            assertEquals(WindDirection(degrees[i]).degree, ans[i])
        }
    }
}