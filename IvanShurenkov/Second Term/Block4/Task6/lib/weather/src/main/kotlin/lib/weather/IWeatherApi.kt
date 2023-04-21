package lib.weather

import lib.weather.date.Location
import lib.weather.date.Weather
import org.json.JSONArray
import org.json.JSONObject
import org.kodein.di.DI
import java.net.HttpURLConnection
import java.net.URL

interface IWeatherApi {
    val name: String
    var stream: DI

    fun getWeather(): Weather

    fun updateWeather(location: Location, apikey: String)

    fun searchParametherInJson(name: String, jsonObject: Any): JSONObject? {
        if (jsonObject is JSONArray) {
            for (i in jsonObject) {
                val result = searchParametherInJson(name, i)
                if (result != null)
                    return result
            }
        }
        if (jsonObject is JSONObject) {
            if (jsonObject.has(name)) {
                if (jsonObject.get(name) is JSONObject)
                    return jsonObject.get(name) as JSONObject?
                else
                    return JSONObject("{\"sg\": ${jsonObject.get(name).toString()}}")
            } else {
                for (i in jsonObject.keys()) {
                    val result = searchParametherInJson(name, jsonObject.get(i))
                    if (result != null)
                        return result
                }
            }
        }
        return null
    }

    fun getURLConnection(url: String): HttpURLConnection {
        return URL(url).openConnection() as HttpURLConnection
    }
}
