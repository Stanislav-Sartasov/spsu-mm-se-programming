import lib.weather.IWeatherApi
import lib.weather.date.Location
import lib.weather.date.Weather
import lib.weather.stream.*
import org.kodein.di.DI
import org.kodein.di.bindSingleton

object TestObject : IWeatherApi {
    override val name: String = "Test object"
    override var stream: DI = DI {
        bindSingleton<Stream>("error") { Stream() }
    }

    override fun updateWeather(location: Location, apikey: String) {}

    override fun getWeather(): Weather {
        return Weather()
    }
}