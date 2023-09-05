import io.ktor.client.*
import io.ktor.client.engine.mock.*
import io.ktor.http.*
import kotlinx.coroutines.runBlocking
import org.junit.jupiter.api.Test
import java.io.File
import kotlin.test.assertEquals

internal class WeatherbitAPITest {

    @Test
    fun get() {
        val engine = MockEngine { _ ->
            respond(
                content = File("src/test/resources/wbit_example.json").readText(),
                status = HttpStatusCode.OK,
                headers = headersOf(HttpHeaders.ContentType, "application/json")
            )
        }
        val client = HttpClient(engine)

        val response: WeatherData
        runBlocking {
            response = WeatherbitAPI.get(Coordinates(0F, 0F), client)
        }
        val reference = WeatherData(
            source = "WeatherBit",
            coordinates = Coordinates(lat = 59.0F, lon = 6.0F),
            city = "Jørpeland",
            tempC = 8.9F,
            tempF = 48.02F,
            clouds = 100,
            description = "Облачно",
            humidity = 95,
            windDir = 265,
            windSpeed = 1.96739F,
            precipitation = 0.0F
        )
        assertEquals(response, reference)
    }
}