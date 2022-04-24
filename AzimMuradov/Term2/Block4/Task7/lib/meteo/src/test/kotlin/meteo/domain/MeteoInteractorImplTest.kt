package meteo.domain

import app.cash.turbine.test
import kotlinx.coroutines.ExperimentalCoroutinesApi
import kotlinx.coroutines.test.runTest
import meteo.data.MeteoRepository
import meteo.domain.entity.Location
import meteo.domain.entity.Weather
import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals
import kotlin.time.*

internal class MeteoInteractorImplTest {

    private lateinit var interactor: MeteoInteractorImpl

    @BeforeEach
    fun setUp() {
        interactor = MeteoInteractorImpl(
            serviceName = SERVICE_NAME,
            repository = mockRepository
        )
    }


    @Test
    fun `get service name`() {
        assertEquals(expected = SERVICE_NAME, actual = interactor.serviceName)
    }

    @OptIn(ExperimentalTime::class, ExperimentalCoroutinesApi::class)
    @Test
    fun `update weather one time`() = runTest {
        interactor.weather.test(timeout = 3.toDuration(DurationUnit.SECONDS)) {
            interactor.updateWeather(location = ZERO_LOCATION)
            assertEquals(expected = SUCCESSFUL_NULL_WEATHER_DATA, actual = awaitItem())
            cancel()
        }
    }


    private companion object {

        const val SERVICE_NAME = "MOCK_SERVICE"

        val SUCCESSFUL_NULL_WEATHER_DATA = Result.success(
            Weather(
                description = null,
                temperature = null,
                cloudCoverage = null,
                humidity = null,
                precipitation = null,
                windDirection = null,
                windSpeed = null
            )
        )

        val ZERO_LOCATION = Location(lat = 0.0, lon = 0.0)

        val mockRepository = object : MeteoRepository {

            override suspend fun getWeather(location: Location): Result<Weather> =
                SUCCESSFUL_NULL_WEATHER_DATA
        }
    }
}
