package lib.weather

import lib.weather.date.Location
import lib.weather.date.Weather
import org.json.JSONArray
import org.json.JSONObject

interface IWeatherApi {
    val name: String

    fun getWeather(location: Location, apikey: String): Weather

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
}
