package meteo.app.presentation

import meteo.app.presentation.MeteoCliMessagesWizard.toHumanReadable
import meteo.domain.entity.*
import meteo.ln
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
                append("Сервис service:".ln())
                append("  погода            : Данных нет".ln())
                append("  температура       : Данных нет".ln())
                append("  облачность        : Данных нет".ln())
                append("  влажность         : Данных нет".ln())
                append("  осадки            : Данных нет".ln())
                append("  направление ветра : Данных нет".ln())
                append("  скорость ветра    : Данных нет".ln())
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
                append("Сервис service:".ln())
                append("  погода            : sample".ln())
                append("  температура       : -231,15 °C / -384,07 °F".ln())
                append("  облачность        : 42%".ln())
                append("  влажность         : 42%".ln())
                append("  осадки            : 42,00 мм/час".ln())
                append("  направление ветра : 42°".ln())
                append("  скорость ветра    : 42,00 м/сек".ln())
            },
        ).map { (a, b) -> arguments(a, b) }
    }
}
