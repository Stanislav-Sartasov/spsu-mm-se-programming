import lib.weather.IWeatherApi
import lib.weather.date.Location
import lib.weather.date.Weather
import lib.weather.stormglass.WeatherStormGlass
import lib.weather.stream.*
import lib.weather.stream.Action.*
import lib.weather.tomorrow.WeatherTomorrow
import org.kodein.di.DI
import org.kodein.di.bindSingleton
import org.kodein.di.instance

fun main(args: Array<String>) {
    val location = Location(59.9623493, 29.6695887)
    var weathersApi: List<IWeatherApi> = listOf(WeatherTomorrow, WeatherStormGlass)
    var weathers = mutableMapOf<String, Weather>()

    val stream = DI {
        bindSingleton<InputStream>() { InputStream() }
        bindSingleton<ErrorStream>() { ErrorStream() }
        bindSingleton<OutputStream>() { OutputStream() }
    }

    val input: InputStream by stream.instance()
    val output: OutputStream by stream.instance()
    val error: ErrorStream by stream.instance()
    for (i in weathersApi.indices) {
        weathersApi[i].stream = stream
    }

    val apiKeys = mutableMapOf<String, String>()
    for (i in args) {
        val splitedArgs = i.split('=')
        if (splitedArgs.size == 2) {
            apiKeys[splitedArgs[0]] = splitedArgs[1]
        }
    }

    var doRequest = true
    while (true) {
        for (i in weathersApi) {
            var weather: Weather? = Weather()
            if (i.name in apiKeys) {
                if (doRequest)
                    weather = apiKeys[i.name]?.let { i.getWeather(location, it) }
                //output.print(i.name)
                //output.print(weather as Weather)
            } else {
                error.print("${i.name} hasn't api key")
            }
            weathers[i.name] = weather ?: Weather()
        }
        doRequest = false

        for (i in weathersApi) {
            output.print(i.name)
            output.print(weathers[i.name] as Weather)
        }

        while (true) {
            val enter = input.read()
            if (enter.action == ADD) {
                val service = ServiceLoader.loadService(enter.commandArgs[0])
                if (service != null) {
                    apiKeys[service.name] = enter.commandArgs[1]
                    weathersApi += service
                }
            }
            if (enter.action == UPDATE) {
                doRequest = true
                break
            }
            if (enter.action == EXIT)
                break
        }
        if (!doRequest)
            break
    }
    return
}
