package meteo.app.presentation

import meteo.app.presentation.MeteoCliMessagesWizard.toHumanReadable
import meteo.domain.entity.*
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.Arguments.arguments
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.assertEquals

internal class MeteoCliMessagesWizardTest {

    @ParameterizedTest
    @MethodSource("weatherWithTexts")
    fun `convert weather to human readable format`(weather: Weather, text: String) {
        assertEquals(
            expected = text,
            actual = weather.toHumanReadable(serviceName = "service")
        )
    }


    private companion object {

        @JvmStatic
        fun weatherWithTexts() = listOf(
            Weather(
                description = null,
                temperature = null,
                cloudCoverage = null,
                humidity = null,
                precipitation = null,
                windDirection = null,
                windSpeed = null
            ) to buildString {
                appendLine("Сервис service:")
                appendLine("  погода            : Данных нет")
                appendLine("  температура       : Данных нет")
                appendLine("  облачность        : Данных нет")
                appendLine("  влажность         : Данных нет")
                appendLine("  осадки            : Данных нет")
                appendLine("  направление ветра : Данных нет")
                appendLine("  скорость ветра    : Данных нет")
            },
            Weather(
                description = "sample",
                temperature = Temperature.inKelvin(value = 42.0),
                cloudCoverage = CloudCoverage.inPercent(value = 42),
                humidity = Humidity.inPercent(value = 42),
                precipitation = Precipitation.inMmPerHour(value = 42.0),
                windDirection = WindDirection.inDegrees(value = 42),
                windSpeed = WindSpeed.inMetersPerSecond(value = 42.0)
            ) to buildString {
                appendLine("Сервис service:")
                appendLine("  погода            : sample")
                appendLine("  температура       : -231.15 °C / -384.07 °F")
                appendLine("  облачность        : 42%")
                appendLine("  влажность         : 42%")
                appendLine("  осадки            : 42.00 мм/час")
                appendLine("  направление ветра : 42°")
                appendLine("  скорость ветра    : 42.00 м/сек")
            },
        ).map { (a, b) -> arguments(a, b) }
    }
}
