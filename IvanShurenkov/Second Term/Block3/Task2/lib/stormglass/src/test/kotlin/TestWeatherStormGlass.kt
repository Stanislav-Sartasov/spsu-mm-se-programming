import io.mockk.every
import io.mockk.mockk
import lib.weather.connection.Connection
import lib.weather.date.Location
import lib.weather.stormglass.WeatherStormGlass
import org.json.JSONObject
import org.junit.jupiter.api.Test
import java.io.File
import kotlin.test.assertEquals

class TestWeatherStormGlass {
    @Test
    fun `Test getJsonFile`() {
        val conn = mockk<Connection>()
        val requestReturns = listOf("200", "200", "401", "200")
        val responseReturns = listOf(
            JSONObject("{\"errors\": 401, \"key\":\"test\"}"),
            JSONObject("{\"error\":\"401\"}"),
            JSONObject("{\"test\": \"test\"}"),
            JSONObject("{\"test\": \"test\"}")
        )
        val correctAns = listOf(JSONObject("{}"), JSONObject("{}"),
            JSONObject("{}"), JSONObject("{\"test\": \"test\"}"))
        every { conn.disconect() } answers { nothing }
        for (i in correctAns.indices) {
            every { conn.requestGet() } returns requestReturns[i]
            every { conn.getResponseInJSON() } returns responseReturns[i]
            assertEquals(WeatherStormGlass.getJsonFile(conn).toString(), correctAns[i].toString())
        }
    }

    @Test
    fun `Parce Json file`() {
        val jsonString = File("src/test/resources/stormglass.json").inputStream().readBytes().toString(Charsets.UTF_8)
        val weather = WeatherStormGlass.parceJson(JSONObject(jsonString))
        assertEquals(5.59, weather.temperature!!.celsius)
        assertEquals(83.26, weather.humidity!!.percent)
        assertEquals(0.0, weather.precipitation!!.mmPerHour)
        assertEquals(87.16, weather.cloudCoverage!!.percent)
        assertEquals(254.74, weather.windDirection!!.degree)
        assertEquals(7.98, weather.windSpeed!!.speed)
    }

    @Test
    fun `Test generation url request`() {
        val ans =
            "https://api.stormglass.io/v2/weather/point?lat=0.0&lng=0.0&params=airTemperature,cloudCover,humidity,precipitation,windSpeed,windDirection"
        assertEquals(WeatherStormGlass.generateUrlRequest(Location(0.0, 0.0)), ans)
    }
}