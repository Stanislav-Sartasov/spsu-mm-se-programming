package service.weather.open_weather_map.provider

import entity.Location
import service.weather.request.WeatherServiceHTTPRequest
import java.io.BufferedReader
import java.io.InputStreamReader
import java.net.URL

class OpenWeatherMapHTTPRequest(location: Location) : WeatherServiceHTTPRequest(location) {
	private val apiKey = System.getenv("openWeatherMapAPIKey")
	private val domain = "https://api.openweathermap.org/data/2.5/weather"

	override fun provideJSON(): String {
		val url = URL("$domain?lat=${location.latitude}&lon=${location.longitude}&units=metric&appid=$apiKey")
		val connection = url.openConnection()
		connection.connectTimeout = 5000
		BufferedReader(InputStreamReader(connection.getInputStream())).use { inputStream ->
			return inputStream.readText()
		}
	}
}