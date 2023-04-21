package meteo.app.presentation

import meteo.app.presentation.MeteoTornadoFxMessagesWizard.loadingErrorMessage
import meteo.app.presentation.MeteoTornadoFxMessagesWizard.toHumanReadable
import meteo.app.presentation.MeteoTornadoFxMessagesWizard.type
import meteo.domain.entity.*
import meteo.presentation.state.LoadingState
import meteo.presentation.state.NamedValue
import org.junit.jupiter.api.Test
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.Arguments.arguments
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.assertEquals

internal class MeteoTornadoFxMessagesWizardTest {

    @ParameterizedTest
    @MethodSource("weatherWithData")
    fun `convert weather to human readable format`(weather: Weather, data: List<NamedValue<String>>) {
        assertEquals(expected = data, actual = weather.toHumanReadable())
    }

    @ParameterizedTest
    @MethodSource("loadingStatesWithTypes")
    fun `get loading state type`(loadingState: LoadingState<*>, type: String) {
        assertEquals(expected = type, actual = loadingState.type)
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
        fun loadingStatesWithTypes() = listOf(
            LoadingState.Loading to "Загрузка...",
            LoadingState.Success(value = null) to "Успех",
            LoadingState.Error(message = "") to "Ошибка",
        ).map { (a, b) -> arguments(a, b) }
    }
}
