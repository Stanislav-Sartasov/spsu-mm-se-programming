package date

import lib.weather.date.Temperature
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class TestTempreture {
    @Test
    fun `Test init`() {
        val temp = listOf(-1000.0, -273.15, 0.0)
        val ans = listOf(-273.15, -273.15, 0.0)
        for (i in temp.indices) {
            assertEquals(Temperature(temp[i]).celsius, ans[i])
        }
    }

    @Test
    fun `Test convert to fahrenheit`() {
        val temp = listOf(-1000.0, -273.15, 0.0, 10.0)
        val ans = listOf(-459.67, -459.67, 32.0, 50.0)
        for (i in temp.indices) {
            assertEquals(Temperature(temp[i]).fahrenheit, ans[i])
        }
    }
}