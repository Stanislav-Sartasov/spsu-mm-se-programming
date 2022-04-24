@file:OptIn(ExperimentalCoroutinesApi::class)

package meteo.openweather.data

import io.mockk.every
import io.mockk.impl.annotations.MockK
import io.mockk.junit5.MockKExtension
import io.mockk.mockk
import kotlinx.coroutines.ExperimentalCoroutinesApi
import kotlinx.coroutines.test.runTest
import kotlinx.serialization.ExperimentalSerializationApi
import kotlinx.serialization.json.Json
import meteo.data.MeteoApi
import meteo.domain.entity.*
import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Test
import org.junit.jupiter.api.extension.ExtendWith
import java.net.http.HttpClient
import java.net.http.HttpResponse
import java.util.concurrent.*
import kotlin.test.assertEquals

@ExtendWith(MockKExtension::class)
internal class OpenWeatherRepositoryTest {

    @MockK
    private lateinit var mockedResponse: HttpResponse<String>

    @MockK
    private lateinit var mockedClient: HttpClient

    @MockK
    private lateinit var mockedApi: MeteoApi

    private lateinit var repository: OpenWeatherRepository

    @OptIn(ExperimentalSerializationApi::class)
    @BeforeEach
    fun setUp() {
        every { mockedResponse.statusCode() } returns 200
        every { mockedResponse.body() } returns "{}"

        every {
            mockedClient.sendAsync<String>(any(), any())
        } returns CompletableFuture.completedFuture(mockedResponse)

        repository = OpenWeatherRepository(
            client = mockedClient,
            api = mockedApi,
            key = "fake",
            json = Json {
                ignoreUnknownKeys = true
                isLenient = true
                explicitNulls = false
            }
        )
    }


    @Test
    fun `get weather`() = runTest {
        every { mockedApi.createGetWeatherRequest(any(), any()) } returns mockk()

        assertEquals(
            expected = Result.success(WEATHER).toString(),
            actual = repository.getWeather(ZERO_LOCATION).toString()
        )
    }

    @Test
    fun `fail to get weather because of the response`() = runTest {
        every { mockedResponse.statusCode() } returns 400
        every { mockedResponse.body() } returns """
            |{
            |  "cod": 400,
            |  "message": "msg_1"
            |}
        """.trimMargin(marginPrefix = "|")
        every { mockedApi.createGetWeatherRequest(any(), any()) } returns mockk()

        assertEquals(
            expected = Result.failure<Weather>(Exception("msg_1")).toString(),
            actual = repository.getWeather(ZERO_LOCATION).toString()
        )
    }

    @Test
    fun `fail to get weather because of the api`() = runTest {
        every { mockedApi.createGetWeatherRequest(any(), any()) } answers { throw Exception("error") }

        assertEquals(
            expected = Result.failure<Weather>(Exception("error")).toString(),
            actual = repository.getWeather(ZERO_LOCATION).toString()
        )
    }


    private companion object {

        val ZERO_LOCATION = Location(lat = 0.0, lon = 0.0)

        val WEATHER = Weather(
            description = null,
            temperature = null,
            cloudCoverage = null,
            humidity = null,
            precipitation = Precipitation.inMmPerHour(value = 0.0),
            windDirection = null,
            windSpeed = null
        )
    }
}
