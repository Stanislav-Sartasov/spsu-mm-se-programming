import io.ktor.client.*
import io.ktor.client.engine.mock.*
import io.ktor.client.plugins.contentnegotiation.*
import io.ktor.http.*
import io.ktor.serialization.kotlinx.json.*
import kotlinx.coroutines.runBlocking
import kotlinx.serialization.json.Json
import org.junit.jupiter.api.Test
import java.io.File
import kotlin.test.assertEquals

internal class OpenWeatherAPITest {

    @Test
    fun `get successful`() {
        val engine = MockEngine {
            respond(
                content = File("src/test/resources/ow_example.json").readText(),
                status = HttpStatusCode.OK,
                headers = headersOf(HttpHeaders.ContentType, "application/json")
            )
        }
        val client = HttpClient(engine) {
            install(ContentNegotiation) {
                json(Json { ignoreUnknownKeys = true })
            }
        }

        val response: WeatherData
        runBlocking {
            response = OpenWeatherAPI.get(Coordinates(0F, 0F), client)
        }
        val reference = WeatherData(
            source = "OpenWeather",
            coordinates = Coordinates(lat = 59.9F, lon = 30.3F),
            city = "SaintPetersburg",
            tempC = 284.49F,
            tempF = 544.082F,
            clouds = 0,
            description = "clearsky",
            humidity = 37,
            windDir = 290,
            windSpeed = 5.0F,
            precipitation = 0.0F
        )
        assertEquals(response, reference)
    }
}