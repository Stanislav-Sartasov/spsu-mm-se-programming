import entity.*
import service.weather.open_weather_map.OpenWeatherMapService
import service.weather.tomorrow_io.TomorrowIOService

fun main() {
	val lat = 59.94
	val lon = 30.313
	val location = Location(name = "Saint-Petersburg", latitude = lat, longitude = lon)
	WeatherAppCLI(location, listOf(OpenWeatherMapService(), TomorrowIOService())).run()
}
