package meteo.app.presentation

import meteo.domain.entity.*
import meteo.presentation.state.LoadingState
import meteo.presentation.state.NamedValue
import java.util.*

object MeteoTornadoFxMessagesWizard {

    const val METEO: String = "Meteo"

    const val LOAD_BUTTON: String = "Обновить"

    const val LOAD_BUTTON_TOOLTIP: String = "Получить последние данные о погоде в СПб"

    fun Weather.toHumanReadable() = listOf(
        "погода" to getHumanReadable(description),
        "температура" to temperature.toHumanReadable(),
        "облачность" to cloudCoverage.toHumanReadable(),
        "влажность" to humidity.toHumanReadable(),
        "осадки" to precipitation.toHumanReadable(),
        "направление ветра" to windDirection.toHumanReadable(),
        "скорость ветра" to windSpeed.toHumanReadable()
    ).map { (name, value) -> NamedValue(name, value) }

    val LoadingState<*>.type: String
        get() = when (this) {
            LoadingState.Loading -> "Загрузка..."
            is LoadingState.Success -> "Успех"
            is LoadingState.Error -> "Ошибка"
        }

    fun loadingErrorMessage(error: LoadingState.Error): String = "Ошибка \"${error.message}\""


    // Utils

    private fun getHumanReadable(description: String?) = description ?: NO_DATA

    private fun Temperature?.toHumanReadable() = this?.run {
        "${celsius.toHumanReadable()} °C / ${fahrenheit.toHumanReadable()} °F"
    } ?: NO_DATA

    private fun CloudCoverage?.toHumanReadable() = this?.run { "$percent%" } ?: NO_DATA

    private fun Humidity?.toHumanReadable() = this?.run { "$percent%" } ?: NO_DATA

    private fun Precipitation?.toHumanReadable() = this?.run { "${mmPerHour.toHumanReadable()} мм/час" } ?: NO_DATA

    private fun WindDirection?.toHumanReadable() = this?.run { "${degrees}°" } ?: NO_DATA

    private fun WindSpeed?.toHumanReadable() = this?.run { "${metersPerSecond.toHumanReadable()} м/сек" } ?: NO_DATA


    private fun Double.toHumanReadable() = String.format(
        locale = Locale.Builder().setLanguage("ru").setRegion("RU").build(),
        format = "%.2f",
        args = arrayOf(this)
    )

    private const val NO_DATA = "Данных нет"
}
