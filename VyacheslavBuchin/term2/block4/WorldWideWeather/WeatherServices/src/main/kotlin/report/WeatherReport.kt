package report

import entity.*

data class WeatherReport(
	val temperature: Temperature? = null,
	val cloudiness: Cloudiness? = null,
	val humidity: Humidity? = null,
	val precipitation: Precipitation? = null,
	val windDirection: WindDirection? = null,
	val windVelocity: Velocity? = null
)
