package meteo.stormglass.data

import io.mockk.every
import io.mockk.mockk
import meteo.domain.entity.Location
import meteo.stormglass.data.StormGlassApi.SG
import org.junit.jupiter.api.Test
import java.net.URI
import java.net.http.HttpHeaders
import java.net.http.HttpRequest
import java.time.Instant
import java.time.temporal.ChronoUnit
import kotlin.test.assertEquals

internal class StormGlassApiTest {

    @Test
    fun `create get weather request`() {
        val lat = 42.0
        val lon = 42.0
        val key = "fake"
        val currHour = Instant.now().truncatedTo(ChronoUnit.HOURS).epochSecond

        val mockedRequest = mockk<HttpRequest>()

        every { mockedRequest.method() } returns "GET"
        every { mockedRequest.uri() } returns URI.create(
            "https://api.stormglass.io/v2/weather/point?lat=$lat&lng=$lon&params=airTemperature,cloudCover,humidity,precipitation,windDirection,windSpeed&start=$currHour&end=$currHour&source=$SG"
        )
        every { mockedRequest.headers() } returns HttpHeaders.of(mapOf("Authorization" to listOf(key))) { _, _ -> true }
        every { mockedRequest == any() } answers { valueAny == mockedRequest }

        assertEquals(
            expected = mockedRequest,
            actual = StormGlassApi.createGetWeatherRequest(location = Location(lat, lon), key = key)
        )
    }
}
