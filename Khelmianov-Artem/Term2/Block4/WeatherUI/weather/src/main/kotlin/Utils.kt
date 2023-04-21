package lib.weather

fun WeatherData.format(): String {
    return """
       #  Data source:      ${if (this.source != null) "${this.source}" else "No data"}
       #  City:             ${this.city ?: "No data"}${if (this.coordinates != null) " (${this.coordinates.lat}, ${this.coordinates.lon})" else ""}
       #  Weather:          ${if (this.description != null) "${this.description}" else "No data"}
       #  Temperature:      ${if (this.tempC != null) "${this.tempC} °C (${this.tempF} °F)" else "No data"}
       #  Cloud cover:      ${if (this.clouds != null) "${this.clouds} %" else "No data"}
       #  Humidity:         ${if (this.humidity != null) "${this.humidity} %" else "No data"}
       #  Wind speed:       ${if (this.windSpeed != null) "${this.windSpeed} m/s" else "No data"}
       #  Wind direction:   ${if (this.windDir != null) "${this.windDir}°" else "No data"}
       #  Precipitation:    ${if (this.precipitation != null) "${this.precipitation} mm/h" else "No data"}
   """.trimMargin("#")
}

