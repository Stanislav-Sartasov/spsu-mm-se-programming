package meteo.openweather.data

import kotlinx.serialization.decodeFromString
import kotlinx.serialization.json.Json
import meteo.data.MeteoApi
import meteo.data.MeteoRepository
import meteo.domain.entity.Location
import meteo.domain.entity.Weather
import meteo.openweather.data.model.OpenWeatherErrorModel
import meteo.openweather.data.model.OpenWeatherModel
import meteo.send
import java.net.http.HttpClient

class OpenWeatherRepository(
    private val client: HttpClient,
    private val api: MeteoApi,
    private val key: String,
    private val json: Json,
) : MeteoRepository {

    override suspend fun getWeather(location: Location): Result<Weather> = try {
        client.send<OpenWeatherModel>(
            request = api.createGetWeatherRequest(location = location, key = key),
            json = json,
            convertErrorBody = { json.decodeFromString<OpenWeatherErrorModel>(it).message }
        ).map(OpenWeatherModel::toWeatherData)
    } catch (e: Throwable) {
        Result.failure(e)
    }
}
