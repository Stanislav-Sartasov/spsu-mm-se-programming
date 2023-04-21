import UI.WeatherApp
import lib.weather.IWeatherApi
import org.kodein.di.instance
import tornadofx.*
//stormglass.io
//tomorrow.io

fun main(args: Array<String>) {
    launch<WeatherApp>(args)
    return
}
