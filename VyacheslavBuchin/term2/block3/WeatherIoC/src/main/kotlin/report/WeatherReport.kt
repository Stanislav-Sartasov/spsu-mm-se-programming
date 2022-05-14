package report

import entity.*

data class WeatherReport(
	val temperature: Temperature?,
	val cloudiness: Cloudiness?,
	val humidity: Humidity?,
	val precipitation: Precipitation?,
	val windDirection: WindDirection?,
	val windVelocity: Velocity?
)
