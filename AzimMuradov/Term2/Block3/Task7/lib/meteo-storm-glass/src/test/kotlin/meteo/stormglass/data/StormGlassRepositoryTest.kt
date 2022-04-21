@file:OptIn(ExperimentalCoroutinesApi::class)

package meteo.stormglass.data

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
internal class StormGlassRepositoryTest {

    @MockK
    private lateinit var mockedResponse: HttpResponse<String>

    @MockK
    private lateinit var mockedClient: HttpClient

    @MockK
    private lateinit var mockedApi: MeteoApi

    private lateinit var repository: StormGlassRepository

    @OptIn(ExperimentalSerializationApi::class)
    @BeforeEach
    fun setUp() {
        every { mockedResponse.statusCode() } returns 200
        every { mockedResponse.body() } returns """
            |{
            |  "hours": [
            |    {
            |      "airTemperature": {
            |        "sg": 42.0
            |      },
            |      "cloudCover": {
            |        "sg": 42.0
            |      },
            |      "humidity": {
            |        "sg": 42.0
            |      },
            |      "precipitation": {
            |        "sg": 42.0
            |      },
            |      "windDirection": {
            |        "sg": 42.0
            |      },
            |      "windSpeed": {
            |        "sg": 42.0
            |      }
            |    }
            |  ]
            |}
        """.trimMargin(marginPrefix = "|")

        every {
            mockedClient.sendAsync<String>(any(), any())
        } returns CompletableFuture.completedFuture(mockedResponse)

        repository = StormGlassRepository(
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
            |  "errors": {
            |    "err_1": "msg_1",
            |    "err_2": "msg_2"
            |  }
            |}
        """.trimMargin(marginPrefix = "|")
        every { mockedApi.createGetWeatherRequest(any(), any()) } returns mockk()

        assertEquals(
            expected = Result.failure<Weather>(Exception("err_1: msg_1, err_2: msg_2")).toString(),
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
            temperature = Temperature.inCelsius(value = 42.0),
            cloudCoverage = CloudCoverage.inPercent(value = 42),
            humidity = Humidity.inPercent(value = 42),
            precipitation = Precipitation.inMmPerHour(value = 42.0),
            windDirection = WindDirection.inDegrees(value = 42),
            windSpeed = WindSpeed.inMetersPerSecond(value = 42.0)
        )
    }
}
