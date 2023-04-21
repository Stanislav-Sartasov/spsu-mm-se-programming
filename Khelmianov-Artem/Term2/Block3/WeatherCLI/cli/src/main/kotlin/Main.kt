import cli.App

fun main() {
    val app = App()
    app.addAPI(listOf(OpenWeatherAPI, WeatherbitAPI))
    app.run()
}