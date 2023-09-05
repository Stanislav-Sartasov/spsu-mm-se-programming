import kotlinx.serialization.json.Json

abstract class AWeatherAPI(
    protected val api: String
) : IWeatherAPI {
    protected var json: Json = Json { ignoreUnknownKeys = true }
}