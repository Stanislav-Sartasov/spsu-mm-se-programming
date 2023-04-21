import lib.weather.IWeatherApi
import lib.weather.date.Location
import lib.weather.stormglass.WeatherStormGlass
import lib.weather.stream.Stream
import lib.weather.tomorrow.WeatherTomorrow
import org.kodein.di.DI
import org.kodein.di.bindSingleton

object KodeinInjactions {
    private val services: List<IWeatherApi> = listOf(WeatherTomorrow, WeatherStormGlass)
    var servicesNames = emptyList<String>()
        private set

    init {
        for (i in services)
            servicesNames += i.name
    }

    val stream = DI {
        bindSingleton<Stream>() { Stream() }
    }

    val location = DI {
        bindSingleton("spb") { Location(59.9623493, 29.6695887) }
    }

    val service = DI {
        for (i in services) {
            i.stream = stream
            bindSingleton<IWeatherApi>(i.name) { i }
        }
    }

    val apikey = DI {
        for (i in services) {
            bindSingleton<String>(i.name) { System.getenv(i.name) ?: "" }
        }
    }
}
