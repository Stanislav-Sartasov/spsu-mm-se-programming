package date

import lib.weather.date.*
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class TestWeather {
    @Test
    fun `Test toString`() {
        val weather = Weather()
        val ans = listOf(
            Pair("Temperature: 1.0°C 33.8°F", "Temperature: No date available"),
            Pair("Cloud coverage: 2.0", "Cloud coverage: No date available"),
            Pair("Humidity: 3.0", "Humidity: No date available"),
            Pair("Precipitation: 4.0", "Precipitation: No date available"),
            Pair("Wind speed: 5.0", "Wind speed: No date available"),
            Pair("Wind direction: 6.0°", "Wind direction: No date available")
        )
        for (i in 0..ans.size) {
            if (i > 0)
                weather.temperature = Temperature(1.0)
            if (i > 1)
                weather.cloudCoverage = CloudCoverage(2.0)
            if (i > 2)
                weather.humidity = Humidity(3.0)
            if (i > 3)
                weather.precipitation = Precipitation(4.0)
            if (i > 4)
                weather.windSpeed = WindSpeed(5.0)
            if (i > 5)
                weather.windDirection = WindDirection(6.0)
            val strings = weather.toString().split('\n')
            for (j in strings.indices) {
                if (j < i)
                    assertEquals(strings[j], ans[j].first)
                else
                    assertEquals(strings[j], ans[j].second)
            }
        }
    }
}