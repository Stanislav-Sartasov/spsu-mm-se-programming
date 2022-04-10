package meteo.app.presentation

import meteo.domain.entity.*
import meteo.ln

object MeteoCliMessagesWizard {

    val GREETINGS_MESSAGE = buildString {
        appendLine("Добро пожаловать в Meteo!")
        appendLine("Для просмотра списка команд исполните \"помощь\".")
    }

    val HELP_MESSAGE = buildString {
        appendLine("Помощь:")
        appendLine("  - Команды:")
        appendLine("    * Обновить данные о погоде           : ${MeteoCliApp.REFRESH.joinToString()}.")
        appendLine("    * Вызов текущего сообщения с помощью : ${MeteoCliApp.HELP.joinToString()}.")
        appendLine("    * Выход из приложения                : ${MeteoCliApp.EXIT.joinToString()}.")
    }

    val WRONG_COMMAND_MESSAGE = "Неверная команда! Для просмотра списка команд исполните \"помощь\".".ln()

    fun loadingServiceMessage(serviceName: String) = "Сервис $serviceName: Загрузка...".ln()

    fun Weather.toHumanReadable(serviceName: String) = buildString {
        appendLine("Сервис $serviceName:")
        appendLine("  погода            : ${description.takeUnless(String?::isNullOrBlank) ?: NO_DATA}")
        appendLine("  температура       : ${temperature?.toHumanReadable() ?: NO_DATA}")
        appendLine("  облачность        : ${cloudCoverage?.toHumanReadable() ?: NO_DATA}")
        appendLine("  влажность         : ${humidity?.toHumanReadable() ?: NO_DATA}")
        appendLine("  осадки            : ${precipitation?.toHumanReadable() ?: NO_DATA}")
        appendLine("  направление ветра : ${windDirection?.toHumanReadable() ?: NO_DATA}")
        appendLine("  скорость ветра    : ${windSpeed?.toHumanReadable() ?: NO_DATA}")
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


    private fun Double.toHumanReadable() = String.format("%.2f", this)

    private const val NO_DATA = "Данных нет"
}
