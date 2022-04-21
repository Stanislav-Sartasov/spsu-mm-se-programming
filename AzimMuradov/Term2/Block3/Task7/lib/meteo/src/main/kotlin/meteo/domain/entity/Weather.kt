package meteo.domain.entity

data class Weather(
    val description: String?,
    val temperature: Temperature?,
    val cloudCoverage: CloudCoverage?,
    val humidity: Humidity?,
    val precipitation: Precipitation?,
    val windDirection: WindDirection?,
    val windSpeed: WindSpeed?,
)
