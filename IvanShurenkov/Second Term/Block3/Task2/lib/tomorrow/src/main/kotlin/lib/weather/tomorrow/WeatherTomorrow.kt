package lib.weather.tomorrow

import lib.weather.IWeatherApi
import lib.weather.connection.Connection
import lib.weather.date.*
import org.json.JSONArray
import org.json.JSONObject

object WeatherTomorrow : IWeatherApi {
    override val name = "tomorrow.io"
    private var mainUrl = "https://api.tomorrow.io/v4/timelines"
    private val fields =
        listOf("temperature", "cloudCover", "humidity", "precipitationIntensity", "windSpeed", "windDirection")

    fun generateUrlRequest(location: Location, apikey: String): String {
        var url = "$mainUrl?apikey=$apikey&location=${location.toString()}&units=metric&timesteps=current&fields="
        for (i in fields.indices) {
            url += fields[i] + "%2C"
        }
        return url
    }

    fun getJsonFile(connection: Connection): JSONObject {
        val returnCode = connection.requestGet()

        val weatherInJSON = connection.getResponseInJSON()
        connection.disconect()

        if (weatherInJSON.has("warnings")) {
            for (i in weatherInJSON.get("warnings") as JSONArray) {
                println((i as JSONObject).get("message"))
            }
            return JSONObject("{}")
        }
        if (weatherInJSON.has("message")) {
            println(weatherInJSON.get("message").toString())
            return JSONObject("{}")
        }
        if (weatherInJSON.has("error")) {
            println(weatherInJSON.get("error"))
            return JSONObject("{}")
        }
        if (returnCode != "200") {
            println("Response code: $returnCode")
            return JSONObject("{}")
        }
        return weatherInJSON
    }

    fun parceJson(weatherInJSON: JSONObject): Weather {
        val weather = Weather()
        var result = searchParametherInJson("temperature", weatherInJSON)
        if (result != null)
            weather.temperature = Temperature(result.get("sg").toString().toDouble())

        result = searchParametherInJson("cloudCover", weatherInJSON)
        if (result != null)
            weather.cloudCoverage = CloudCoverage(result.get("sg").toString().toDouble())

        result = searchParametherInJson("humidity", weatherInJSON)
        if (result != null)
            weather.humidity = Humidity(result.get("sg").toString().toDouble())

        result = searchParametherInJson("precipitationIntensity", weatherInJSON)
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

    override fun getWeather(location: Location, apikey: String): Weather {
        return parceJson(getJsonFile(Connection(generateUrlRequest(location, apikey))))
    }
}
