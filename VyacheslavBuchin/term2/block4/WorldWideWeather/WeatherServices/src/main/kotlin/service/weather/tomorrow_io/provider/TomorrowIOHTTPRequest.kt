package service.weather.tomorrow_io.provider

import entity.Location
import service.weather.request.WeatherServiceHTTPRequest
import java.io.BufferedReader
import java.io.InputStreamReader
import java.net.URL

class TomorrowIOHTTPRequest(location: Location) : WeatherServiceHTTPRequest(location) {
	private val apiKey = System.getenv("tomorrowIOAPIKey")
	private val domain = "https://api.tomorrow.io/v4/timelines"
	private val params = "temperature,cloudCover,humidity,precipitationIntensity,windSpeed,windDirection"

	override fun provideJSON(): String {
		val url = URL("$domain?location=${location.latitude},${location.longitude}&fields=$params&timesteps=current&units=metric&apikey=$apiKey")
		val connection = url.openConnection()
		connection.connectTimeout = 5000
		BufferedReader(InputStreamReader(connection.getInputStream())).use { inputStream ->
			return inputStream.readText()
		}
	}
}