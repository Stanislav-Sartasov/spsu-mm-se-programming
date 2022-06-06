package lib.weather.date

class Weather {
    var temperature: Temperature? = null
    var cloudCoverage: CloudCoverage? = null
    var humidity: Humidity? = null
    var precipitation: Precipitation? = null
    var windSpeed: WindSpeed? = null
    var windDirection: WindDirection? = null

    override fun toString(): String {
        var ret = "Temperature: " + if (temperature != null) {
            "${temperature!!.celsius}°C ${temperature!!.fahrenheit}°F"
        } else "No date available"
        ret += "\nCloud coverage: " + if (cloudCoverage != null) "${cloudCoverage!!.percent}"
        else "No date available"
        ret += "\nHumidity: " + if (humidity != null) "${humidity!!.percent}"
        else "No date available"
        ret += "\nPrecipitation: " + if (precipitation != null) "${precipitation!!.mmPerHour}"
        else "No date available"
        ret += "\nWind speed: " + if (windSpeed != null) "${windSpeed!!.speed}"
        else "No date available"
        ret += "\nWind direction: " + if (windDirection != null) "${windDirection!!.degree}°"
        else "No date available"
        return ret
    }
}