package lib.weather.stormglass

import lib.weather.IWeatherApi
import lib.weather.connection.Connection
import lib.weather.date.*
import org.json.JSONArray
import org.json.JSONObject

object WeatherStormGlass : IWeatherApi {
    override val name = "stormglass.io"
    private val mainUrl = "https://api.stormglass.io/v2/weather/point"
    private val fields =
        listOf("airTemperature", "cloudCover", "humidity", "precipitation", "windSpeed", "windDirection")

    fun generateUrlRequest(location: Location, apikey: String): String {
        var url = "$mainUrl?lat=${location.lat}&lng=${location.lon}&params="
        for (i in fields.indices) {
            url += fields[i] + if (i < fields.size - 1) "," else ""
        }
        return url
    }

    override fun getWeather(location: Location, apikey: String): Weather {
        val weather = Weather()
        //println(generateUrlRequest(location, apikey))
        val connection = Connection(generateUrlRequest(location, apikey))
        connection.setAuthorizationHeader(apikey)

        val returnCode = connection.requestGet()
        if (returnCode != "200") {
            return weather
        }

        val weatherInJSON = connection.getResponseInJSON()
        connection.disconect()

        if (weatherInJSON.has("error")) {
            println(weatherInJSON.get("error"))
            return weather
        }
        if (weatherInJSON.has("errors")) {
            println(weatherInJSON.get("key").toString())
            return weather
        }
        if (weatherInJSON.has("warnings")) {
            for (i in weatherInJSON.get("warnings") as JSONArray) {
                println((i as JSONObject).get("message"))
            }
            return weather
        }

        var result = searchParametherInJson("airTemperature", weatherInJSON)
        if (result != null)
            weather.temperature = Temperature((result.get("sg")).toString().toDouble())

        result = searchParametherInJson("cloudCover", weatherInJSON)
        if (result != null)
            weather.cloudCoverage = CloudCoverage(result.get("sg").toString().toDouble())

        result = searchParametherInJson("humidity", weatherInJSON)
        if (result != null)
            weather.humidity = Humidity(result.get("sg").toString().toDouble())

        result = searchParametherInJson("precipitation", weatherInJSON)
        if (result != null)
            weather.precipitation = Precipitation(result.get("sg").toString().toDouble())

        result = searchParametherInJson("windSpeed", weatherInJSON)
        if (result != null)
            weather.windSpeed = WindSpeed(result.get("sg").toString().toDouble())

        result = searchParametherInJson("windDirection", weatherInJSON)
        if (result != null)
            weather.windDirection = WindDirection(result.get("sg").toString().toDouble())

        return weather
    }
}
