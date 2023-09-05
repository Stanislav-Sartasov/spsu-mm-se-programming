package meteo.stormglass.data

import meteo.domain.entity.*
import meteo.stormglass.data.StormGlassApi.SG
import meteo.stormglass.data.model.StormGlassModel
import meteo.stormglass.data.model.StormGlassWeatherModel
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.Arguments.arguments
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.assertEquals

internal class MappersTest {

    @ParameterizedTest
    @MethodSource("stormGlassModelsWithWeather")
    fun `convert StormGlassModel to Weather`(stormGlassModel: StormGlassModel, weather: Weather) {
        assertEquals(
            expected = weather,
            actual = stormGlassModel.toWeatherData()
        )
    }


    private companion object {

        @JvmStatic
        fun stormGlassModelsWithWeather() = listOf(
            StormGlassModel(
                hours = listOf(
                    StormGlassWeatherModel(
                        airTemperature = mapOf(SG to 42.0),
                        cloudCover = mapOf(SG to 42.0),
                        humidity = mapOf(SG to 42.0),
                        precipitation = mapOf(SG to 42.0),
                        windDirection = mapOf(SG to 42.0),
                        windSpeed = mapOf(SG to 42.0),
                    )
                )
            ) to Weather(
                description = null,
                temperature = Temperature.inCelsius(value = 42.0),
                cloudCoverage = CloudCoverage.inPercent(value = 42),
                humidity = Humidity.inPercent(value = 42),
                precipitation = Precipitation.inMmPerHour(value = 42.0),
                windDirection = WindDirection.inDegrees(value = 42),
                windSpeed = WindSpeed.inMetersPerSecond(value = 42.0)
            )
        ).map { (a, b) -> arguments(a, b) }
    }
}
