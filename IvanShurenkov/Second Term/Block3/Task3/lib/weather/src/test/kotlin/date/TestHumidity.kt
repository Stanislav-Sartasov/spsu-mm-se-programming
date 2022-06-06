package date

import lib.weather.date.Humidity
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class TestHumidity {
    @Test
    fun `Test init`() {
        val humidity = listOf(-1.0, 0.0, 1.0, 100.0, 1000.0)
        val ans = listOf(0.0, 0.0, 1.0, 100.0, 100.0)
        for (i in humidity.indices) {
            assertEquals(Humidity(humidity[i]).percent, ans[i])
        }
    }
}