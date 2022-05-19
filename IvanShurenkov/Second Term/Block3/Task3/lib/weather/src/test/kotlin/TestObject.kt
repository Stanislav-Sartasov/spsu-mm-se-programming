import lib.weather.IWeatherApi
import lib.weather.date.Location
import lib.weather.date.Weather
import lib.weather.stream.*
import org.kodein.di.DI
import org.kodein.di.bindSingleton

object TestObject : IWeatherApi {
    override val name: String = "Test object"
    override var stream: DI = DI {
        bindSingleton<ErrorStream>("error") { ErrorStream() }
        bindSingleton<OutputStream>("output") { OutputStream() }
    }

    override fun getWeather(location: Location, apikey: String): Weather {
        return Weather()
    }
}