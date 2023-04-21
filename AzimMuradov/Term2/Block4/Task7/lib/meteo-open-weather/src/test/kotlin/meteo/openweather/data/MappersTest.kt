package meteo.openweather.data

import meteo.domain.entity.*
import meteo.openweather.data.model.*
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.Arguments.arguments
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.assertEquals

internal class MappersTest {

    @ParameterizedTest
    @MethodSource("openWeatherModelsWithWeather")
    fun `convert OpenWeatherModel to Weather`(openWeatherModel: OpenWeatherModel, weather: Weather) {
        assertEquals(
            expected = weather,
            actual = openWeatherModel.toWeatherData()
        )
    }


    private companion object {

        @JvmStatic
        fun openWeatherModelsWithWeather() = listOf(
            OpenWeatherModel(
                weather = null,
                main = null,
                wind = null,
                clouds = null,
                rain = null,
                snow = null
            ) to Weather(
                description = null,
                temperature = null,
                cloudCoverage = null,
                humidity = null,
                precipitation = Precipitation.inMmPerHour(value = 0.0),
                windDirection = null,
                windSpeed = null
            ),
            OpenWeatherModel(
                weather = listOf(),
                main = MainModel(temp = null, humidity = null),
                wind = WindModel(speed = null, deg = null),
                clouds = CloudsModel(all = null),
                rain = RainModel(`1h` = null),
                snow = SnowModel(`1h` = null)
            ) to Weather(
                description = "",
                temperature = null,
                cloudCoverage = null,
                humidity = null,
                precipitation = Precipitation.inMmPerHour(value = 0.0),
                windDirection = null,
                windSpeed = null
            ),
            OpenWeatherModel(
                weather = listOf(WeatherModel(description = null)),
                main = MainModel(temp = 42.0, humidity = 42),
                wind = WindModel(speed = 42.0, deg = 42),
                clouds = CloudsModel(all = 42),
                rain = RainModel(`1h` = 42.0),
                snow = SnowModel(`1h` = 42.0)
            ) to Weather(
                description = "",
                temperature = Temperature.inKelvin(value = 42.0),
                cloudCoverage = CloudCoverage.inPercent(value = 42),
                humidity = Humidity.inPercent(value = 42),
                precipitation = Precipitation.inMmPerHour(value = 84.0),
                windDirection = WindDirection.inDegrees(value = 42),
                windSpeed = WindSpeed.inMetersPerSecond(value = 42.0)
            ),
            OpenWeatherModel(
                weather = listOf(WeatherModel(description = "fake")),
                main = MainModel(temp = 42.0, humidity = 42),
                wind = WindModel(speed = 42.0, deg = 42),
                clouds = CloudsModel(all = 42),
                rain = RainModel(`1h` = 42.0),
                snow = SnowModel(`1h` = 42.0)
            ) to Weather(
                description = "fake",
                temperature = Temperature.inKelvin(value = 42.0),
                cloudCoverage = CloudCoverage.inPercent(value = 42),
                humidity = Humidity.inPercent(value = 42),
                precipitation = Precipitation.inMmPerHour(value = 84.0),
                windDirection = WindDirection.inDegrees(value = 42),
                windSpeed = WindSpeed.inMetersPerSecond(value = 42.0)
            )
        ).map { (a, b) -> arguments(a, b) }
    }
}
