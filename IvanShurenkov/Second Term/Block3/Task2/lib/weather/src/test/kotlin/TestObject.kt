import lib.weather.IWeatherApi
import lib.weather.date.Location
import lib.weather.date.Weather

object TestObject : IWeatherApi {
    override val name: String = "Test object"

    override fun getWeather(location: Location, apikey: String): Weather {
        return Weather()
    }
}