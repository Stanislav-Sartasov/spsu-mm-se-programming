package meteo.openweather.data

import io.mockk.every
import io.mockk.mockk
import meteo.domain.entity.Location
import org.junit.jupiter.api.Test
import java.net.URI
import java.net.http.HttpHeaders
import java.net.http.HttpRequest
import kotlin.test.assertEquals

internal class OpenWeatherApiTest {

    @Test
    fun `create get weather request`() {
        val lat = 42.0
        val lon = 42.0
        val key = "fake"

        val mockedRequest = mockk<HttpRequest>()

        every { mockedRequest.method() } returns "GET"
        every { mockedRequest.uri() } returns URI.create(
            "https://api.openweathermap.org/data/2.5/weather?lat=$lat&lon=$lon&appid=$key&lang=ru"
        )
        every { mockedRequest.headers() } returns HttpHeaders.of(mapOf()) { _, _ -> true }
        every { mockedRequest == any() } answers { valueAny == mockedRequest }

        assertEquals(
            expected = mockedRequest,
            actual = OpenWeatherApi.createGetWeatherRequest(location = Location(lat, lon), key = key)
        )
    }
}
