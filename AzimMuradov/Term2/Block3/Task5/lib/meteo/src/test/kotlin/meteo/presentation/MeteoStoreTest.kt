package meteo.presentation

import app.cash.turbine.test
import kotlinx.coroutines.ExperimentalCoroutinesApi
import kotlinx.coroutines.flow.MutableSharedFlow
import kotlinx.coroutines.test.*
import meteo.domain.MeteoInteractor
import meteo.domain.entity.Location
import meteo.domain.entity.Weather
import meteo.presentation.state.*
import meteo.presentation.wish.MeteoWish
import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals
import kotlin.time.DurationUnit.SECONDS
import kotlin.time.ExperimentalTime
import kotlin.time.toDuration

internal class MeteoStoreTest {

    @OptIn(ExperimentalCoroutinesApi::class)
    private val testScope = TestScope(UnconfinedTestDispatcher())

    private lateinit var store: MeteoStore

    private lateinit var interactor: MeteoInteractor

    @OptIn(ExperimentalCoroutinesApi::class)
    @BeforeEach
    fun setUp() {
        interactor = createMockInteractor()
        store = MeteoStore(
            interactors = listOf(interactor),
            location = ZERO_LOCATION,
            scope = testScope
        )
    }


    @OptIn(ExperimentalTime::class, ExperimentalCoroutinesApi::class)
    @Test
    fun `collect state`() = runTest {
        store.state.test(timeout = 3.toDuration(SECONDS)) {
            assertEquals(expected = MeteoState.Uninitialised, actual = awaitItem())

            interactor.updateWeather(ZERO_LOCATION)

            assertEquals(
                expected = MeteoState.Initialised(
                    listOf(NamedValue(name = SERVICE_NAME, value = LoadingState.Success(NULL_WEATHER_DATA)))
                ),
                actual = awaitItem()
            )

            cancel()
        }
    }

    @OptIn(ExperimentalTime::class, ExperimentalCoroutinesApi::class)
    @Test
    fun `consume wish`() = runTest {
        store.state.test(timeout = 3.toDuration(SECONDS)) {
            assertEquals(expected = MeteoState.Uninitialised, actual = awaitItem())

            store.consume(MeteoWish.Load)

            assertEquals(
                expected = MeteoState.Initialised(
                    listOf(NamedValue(name = SERVICE_NAME, value = LoadingState.Loading))
                ),
                actual = awaitItem()
            )
            assertEquals(
                expected = MeteoState.Initialised(
                    listOf(NamedValue(name = SERVICE_NAME, value = LoadingState.Success(NULL_WEATHER_DATA)))
                ),
                actual = awaitItem()
            )

            cancel()
        }
    }


    private companion object {

        const val SERVICE_NAME = "MOCK_SERVICE"

        val NULL_WEATHER_DATA = Weather(
            description = null,
            temperature = null,
            cloudCoverage = null,
            humidity = null,
            precipitation = null,
            windDirection = null,
            windSpeed = null
        )

        val SUCCESSFUL_NULL_WEATHER_DATA = Result.success(NULL_WEATHER_DATA)

        val ZERO_LOCATION = Location(lat = 0.0, lon = 0.0)

        fun createMockInteractor() = object : MeteoInteractor {

            override val serviceName: String = SERVICE_NAME

            override val weather: MutableSharedFlow<Result<Weather>> = MutableSharedFlow()

            override suspend fun updateWeather(location: Location) {
                weather.emit(SUCCESSFUL_NULL_WEATHER_DATA)
            }
        }
    }
}
