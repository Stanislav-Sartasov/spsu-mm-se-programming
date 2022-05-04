import lib.weather.IWeatherApi
import lib.weather.date.Location
import lib.weather.date.Weather
import lib.weather.stormglass.WeatherStormGlass
import lib.weather.tomorrow.WeatherTomorrow

fun main(args: Array<String>) {
    val location = Location(59.9623493, 29.6695887)
    val weathersApi: List<IWeatherApi> = listOf(WeatherTomorrow, WeatherStormGlass)
    val weathers: MutableList<Weather> = mutableListOf()
    val progArgs = mutableMapOf<String, String>()
    for (i in args) {
        val splitedArg = i.split('=')
        if (splitedArg.size == 2) {
            progArgs.put(splitedArg[0], splitedArg[1])
        }
    }
    var doRequest = true
    while (true) {
        for (i in weathersApi) {
            var weather: Weather? = Weather()
            if (i.name in progArgs) {
                if (doRequest)
                    weather = progArgs[i.name]?.let { i.getWeather(location, it) }
                println(i.name)
                println(weather)
            } else {
                println("${i.name} hasn't api key")
            }
            if (weather != null)
                weathers += weather
            else
                weathers += Weather()
        }
        doRequest = false

        while (true) {
            print("\rEnter u - to update; q - to exit: ")
            val enter = readLine()!!
            if (enter == "u") {
                doRequest = true
                break
            }
            if (enter == "q")
                break
        }
        if (!doRequest)
            break
    }
    return
}