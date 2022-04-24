package meteo.app.presentation

import meteo.domain.entity.*
import meteo.ln
import java.util.*

object MeteoCliMessagesWizard {

    val GREETINGS_MESSAGE = buildString {
        append("Добро пожаловать в Meteo!".ln())
        append("Для просмотра списка команд исполните \"помощь\".".ln())
    }

    val HELP_MESSAGE = buildString {
        append("Помощь:".ln())
        append("  - Команды:".ln())
        append("    * Обновить данные о погоде           : ${MeteoCliApp.REFRESH.joinToString()}.".ln())
        append("    * Вызов текущего сообщения с помощью : ${MeteoCliApp.HELP.joinToString()}.".ln())
        append("    * Выход из приложения                : ${MeteoCliApp.EXIT.joinToString()}.".ln())
    }

    val WRONG_COMMAND_MESSAGE = "Неверная команда! Для просмотра списка команд исполните \"помощь\".".ln()

    fun loadingServiceMessage(serviceName: String) = "Сервис $serviceName: Загрузка...".ln()

    fun Weather.toHumanReadable(serviceName: String) = buildString {
        append("Сервис $serviceName:".ln())
        append("  погода            : ${description.takeUnless(String?::isNullOrBlank) ?: NO_DATA}".ln())
        append("  температура       : ${temperature?.toHumanReadable() ?: NO_DATA}".ln())
        append("  облачность        : ${cloudCoverage?.toHumanReadable() ?: NO_DATA}".ln())
        append("  влажность         : ${humidity?.toHumanReadable() ?: NO_DATA}".ln())
        append("  осадки            : ${precipitation?.toHumanReadable() ?: NO_DATA}".ln())
        append("  направление ветра : ${windDirection?.toHumanReadable() ?: NO_DATA}".ln())
        append("  скорость ветра    : ${windSpeed?.toHumanReadable() ?: NO_DATA}".ln())
    }

    fun errorWithLoadingServiceMessage(serviceName: String, errorMessage: String) =
        "Сервис ${serviceName}: Ошибка \"${errorMessage}\"".ln()

    val CLOSING_APP_MESSAGE = "Закрываем приложение...".ln()


    // Utils

    private fun Temperature.toHumanReadable() = "${celsius.toHumanReadable()} °C / ${fahrenheit.toHumanReadable()} °F"

    private fun CloudCoverage.toHumanReadable() = "$percent%"

    private fun Humidity.toHumanReadable() = "$percent%"

    private fun Precipitation.toHumanReadable() = "${mmPerHour.toHumanReadable()} мм/час"

    private fun WindDirection.toHumanReadable() = "${degrees}°"

    private fun WindSpeed.toHumanReadable() = "${metersPerSecond.toHumanReadable()} м/сек"


    private fun Double.toHumanReadable() = String.format(
        locale = Locale.Builder().setLanguage("ru").setRegion("RU").build(),
        format = "%.2f",
        args = arrayOf(this)
    )

    private const val NO_DATA = "Данных нет"
}
