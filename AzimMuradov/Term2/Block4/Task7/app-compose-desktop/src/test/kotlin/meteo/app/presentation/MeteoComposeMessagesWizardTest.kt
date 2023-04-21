package meteo.app.presentation

import meteo.app.presentation.MeteoComposeMessagesWizard.isLoading
import meteo.app.presentation.MeteoComposeMessagesWizard.loadingErrorMessage
import meteo.app.presentation.MeteoComposeMessagesWizard.toHumanReadable
import meteo.domain.entity.*
import meteo.presentation.state.*
import org.junit.jupiter.api.Test
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.Arguments.arguments
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.assertEquals

internal class MeteoComposeMessagesWizardTest {

    @ParameterizedTest
    @MethodSource("weatherWithData")
    fun `convert weather to human readable format`(weather: Weather, data: List<NamedValue<String>>) {
        assertEquals(expected = data, actual = weather.toHumanReadable())
    }

    @ParameterizedTest
    @MethodSource("meteoStatesWithIsLoading")
    fun `check if meteo state is in loading state`(meteoState: MeteoState, isLoading: Boolean) {
        assertEquals(expected = isLoading, actual = meteoState.isLoading)
    }

    @Test
    fun `get loading error message`() {
        assertEquals(expected = "Ошибка \"error\"", actual = loadingErrorMessage(LoadingState.Error("error")))
    }


    private companion object {

        @JvmStatic
        fun weatherWithData() = listOf(
            Weather(
                description = null,
                temperature = null,
                cloudCoverage = null,
                humidity = null,
                precipitation = null,
                windDirection = null,
                windSpeed = null
            ) to listOf(
                NamedValue(name = "погода", value = "Данных нет"),
                NamedValue(name = "температура", value = "Данных нет"),
                NamedValue(name = "облачность", value = "Данных нет"),
                NamedValue(name = "влажность", value = "Данных нет"),
                NamedValue(name = "осадки", value = "Данных нет"),
                NamedValue(name = "направление ветра", value = "Данных нет"),
                NamedValue(name = "скорость ветра", value = "Данных нет"),
            ),
            Weather(
                description = "sample",
                temperature = Temperature.inKelvin(value = 42.0),
                cloudCoverage = CloudCoverage.inPercent(value = 42),
                humidity = Humidity.inPercent(value = 42),
                precipitation = Precipitation.inMmPerHour(value = 42.0),
                windDirection = WindDirection.inDegrees(value = 42),
                windSpeed = WindSpeed.inMetersPerSecond(value = 42.0)
            ) to listOf(
                NamedValue(name = "погода", value = "sample"),
                NamedValue(name = "температура", value = "-231,15 °C / -384,07 °F"),
                NamedValue(name = "облачность", value = "42%"),
                NamedValue(name = "влажность", value = "42%"),
                NamedValue(name = "осадки", value = "42,00 мм/час"),
                NamedValue(name = "направление ветра", value = "42°"),
                NamedValue(name = "скорость ветра", value = "42,00 м/сек"),
            )
        ).map { (a, b) -> arguments(a, b) }

        @JvmStatic
        fun meteoStatesWithIsLoading() = listOf(
            MeteoState.Uninitialised to false,
            MeteoState.Initialised(listOf(NamedValue("", LoadingState.Loading))) to true,
            MeteoState.Initialised(listOf(NamedValue("", LoadingState.Error("")))) to false,
        ).map { (a, b) -> arguments(a, b) }
    }
}
