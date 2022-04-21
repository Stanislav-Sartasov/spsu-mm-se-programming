package meteo.stormglass.data

import kotlinx.serialization.decodeFromString
import kotlinx.serialization.json.Json
import meteo.data.MeteoApi
import meteo.data.MeteoRepository
import meteo.domain.entity.Location
import meteo.domain.entity.Weather
import meteo.send
import meteo.stormglass.data.model.StormGlassErrorModel
import meteo.stormglass.data.model.StormGlassModel
import java.net.http.HttpClient

class StormGlassRepository(
    private val client: HttpClient,
    private val api: MeteoApi,
    private val key: String,
    private val json: Json,
) : MeteoRepository {

    override suspend fun getWeather(location: Location): Result<Weather> = try {
        client.send<StormGlassModel>(
            request = api.createGetWeatherRequest(location = location, key = key),
            json = json,
            convertErrorBody = {
                json.decodeFromString<StormGlassErrorModel>(it).errors.toList().joinToString { (k, v) ->
                    "$k: $v"
                }
            }
        ).map(StormGlassModel::toWeatherData)
    } catch (e: Throwable) {
        Result.failure(e)
    }
}
