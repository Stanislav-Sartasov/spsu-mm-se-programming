import io.mockk.every
import io.mockk.mockk
import lib.weather.connection.Connection
import lib.weather.date.Location
import lib.weather.tomorrow.WeatherTomorrow
import org.json.JSONObject
import org.junit.jupiter.api.Test
import java.io.File
import kotlin.test.assertEquals

class TestWeatherTomorrow {
    @Test
    fun `Test getJsonFile`() {
        val conn = mockk<Connection>()
        val requestReturns = listOf("200", "200", "401", "401", "200")
        val responseReturns = listOf(
            JSONObject("{\"warnings\":[{\"message\":\"test\"}]}"),
            JSONObject("{\"message\":\"test\"}"),
            JSONObject("{\"error\":\"401\"}"),
            JSONObject("{\"test\": \"test\"}"),
            JSONObject("{\"test\": \"test\"}")
        )
        val correctAns = listOf(JSONObject("{}"), JSONObject("{}"), JSONObject("{}"),
            JSONObject("{}"), JSONObject("{\"test\": \"test\"}"))
        every { conn.disconect() } answers { nothing }
        for (i in correctAns.indices) {
            every { conn.requestGet() } returns requestReturns[i]
            every { conn.getResponseInJSON() } returns responseReturns[i]
            assertEquals(WeatherTomorrow.getJsonFile(conn).toString(), correctAns[i].toString())
        }
    }

    @Test
    fun `Parce Json file`() {
        val jsonString = File("src/test/resources/tomorrow.json").inputStream().readBytes().toString(Charsets.UTF_8)
        val weather = WeatherTomorrow.parceJson(JSONObject(jsonString))
        assertEquals(weather.temperature!!.celsius, 5.81)
        assertEquals(weather.humidity!!.percent, 57.0)
        assertEquals(weather.precipitation!!.mmPerHour, 0.0)
        assertEquals(weather.cloudCoverage!!.percent, 64.0)
        assertEquals(weather.windDirection!!.degree, 287.63)
        assertEquals(weather.windSpeed!!.speed, 9.63)
    }

    @Test
    fun `Test generation url request`() {
        val ans = "https://api.tomorrow.io/v4/timelines?apikey=42&location=0.0,0.0&units=metric&timesteps=current" +
                "&fields=temperature%2CcloudCover%2Chumidity%2CprecipitationIntensity%2CwindSpeed%2CwindDirection%2C"
        assertEquals(WeatherTomorrow.generateUrlRequest(Location(0.0, 0.0), "42"), ans)
    }
}