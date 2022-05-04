import lib.weather.connection.Connection
import lib.weather.date.Location
import lib.weather.tomorrow.WeatherTomorrow
import org.json.JSONObject
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals
import kotlin.test.assertFails
import kotlin.test.assertNotEquals

class TestWeatherTomorrow {
    val jsonString =
        "{\"data\":{\"timelines\":[{\"timestep\":\"current\",\"endTime\":\"2022-05-03T13:53:37.732393331Z\",\"startTime\":\"2022-05-03T13:53:37.732393331Z\",\"intervals\":[{\"startTime\":\"2022-05-03T13:53:37.732393331Z\",\"values\":{\"temperature\":5.81}}]}]}}"

    @Test
    fun `Test without apikey`() {
        val weather = WeatherTomorrow.getWeather(Location(0.0, 0.0), "")
        assertEquals(weather.temperature, null)
        assertEquals(weather.humidity, null)
        assertEquals(weather.precipitation, null)
        assertEquals(weather.cloudCoverage, null)
        assertEquals(weather.windDirection, null)
        assertEquals(weather.windSpeed, null)
    }

    @Test
    fun `Test search paramether`() {
        val jo = WeatherTomorrow.searchParametherInJson("temperature", JSONObject(jsonString))
        if (jo is JSONObject)
            assertEquals(jo.get("sg").toString().toDouble(), 5.81)
        else
            assertFails { "Wasn't found field" }
    }

    @Test
    fun `Test getWeather`() {
        val connection = Connection("http://127.0.0.1:9000/api.json")
        connection.requestGet()
        val jo = connection.getResponseInJSON()
        val apikey = jo.get("tomorrow").toString()
        connection.disconect()
        val weather = WeatherTomorrow.getWeather(Location(59.9623493, 29.6695887), apikey)
        assertNotEquals(weather.temperature, null)
        assertNotEquals(weather.humidity, null)
        assertNotEquals(weather.precipitation, null)
        assertNotEquals(weather.cloudCoverage, null)
        assertNotEquals(weather.windDirection, null)
        assertNotEquals(weather.windSpeed, null)
    }

    @Test
    fun `Test generation url request`() {
        val ans = "https://api.tomorrow.io/v4/timelines?apikey=42&location=0.0,0.0&units=metric&timesteps=current" +
                "&fields=temperature%2CcloudCover%2Chumidity%2CprecipitationIntensity%2CwindSpeed%2CwindDirection%2C"
        assertEquals(WeatherTomorrow.generateUrlRequest(Location(0.0, 0.0), "42"), ans)
    }
}