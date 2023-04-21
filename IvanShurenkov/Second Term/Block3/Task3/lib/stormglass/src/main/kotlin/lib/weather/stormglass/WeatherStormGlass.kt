package lib.weather.stormglass

import lib.weather.IWeatherApi
import lib.weather.connection.Connection
import lib.weather.date.*
import lib.weather.stream.*
import org.json.JSONObject
import org.kodein.di.DI
import org.kodein.di.bindSingleton
import org.kodein.di.instance

object WeatherStormGlass : IWeatherApi {
    override val name = "stormglass.io"
    private val mainUrl = "https://api.stormglass.io/v2/weather/point"
    private val fields =
        listOf("airTemperature", "cloudCover", "humidity", "precipitation", "windSpeed", "windDirection")
    override var stream: DI = DI {
        bindSingleton<ErrorStream>() { ErrorStream() }
        bindSingleton<OutputStream>() { OutputStream() }
    }

    fun generateUrlRequest(location: Location): String {
        var url = "$mainUrl?lat=${location.lat}&lng=${location.lon}&params="
        for (i in fields.indices) {
            url += fields[i] + if (i < fields.size - 1) "," else ""
        }
        return url
    }

    fun getJsonFile(connection: Connection): JSONObject {
        val returnCode = connection.requestGet()
        val errorStream: ErrorStream by stream.instance()

        val weatherInJSON = connection.getResponseInJSON()
        connection.disconect()

        if (weatherInJSON.has("error")) {
            errorStream.print(weatherInJSON.get("error").toString())
            return JSONObject("{}")
        }
        if (weatherInJSON.has("errors")) {
            errorStream.print(weatherInJSON.get("key").toString())
            return JSONObject("{}")
        }
        if (returnCode != "200") {
            errorStream.print("Response code: $returnCode")
            return JSONObject("{}")
        }
        return weatherInJSON
    }

    fun parceJson(weatherInJSON: JSONObject): Weather {
        val weather = Weather()
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

    override fun getWeather(location: Location, apikey: String): Weather {
        val connection = Connection(getURLConnection(generateUrlRequest(location)), stream)
        connection.setAuthorizationHeader(apikey)
        return parceJson(getJsonFile(connection))
    }
}
