package tornadofxapp

import lib.weather.*
import io.ktor.client.*
import io.ktor.client.plugins.*
import io.ktor.client.plugins.contentnegotiation.*
import io.ktor.serialization.kotlinx.json.*
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.launch
import kotlinx.serialization.json.Json
import org.koin.core.component.KoinComponent
import org.koin.core.component.inject
import tornadofx.Controller
import tornadofx.EventBus
import tornadofx.FXEvent

class LoadWeather(val lat: Float, val lon: Float, val api: String) : FXEvent(EventBus.RunOn.BackgroundThread)

class WeatherLoaded(val result: Result<WeatherData>) : FXEvent(EventBus.RunOn.BackgroundThread) {
    val isSuccess = result.isSuccess
    val data = result.getOrDefault(WeatherData())
}


class AppController : Controller(), KoinComponent {
    private val apis by inject<List<IWeatherAPI>>()
    private var data: Result<WeatherData> = Result.success(WeatherData())
    private val client = HttpClient {
        expectSuccess = true
        install(HttpTimeout) {
            requestTimeoutMillis = 3000
        }
        install(ContentNegotiation) {
            json(Json {
                prettyPrint = true
                ignoreUnknownKeys = true
            })
        }
    }

    init {
        subscribe<LoadWeather> { event ->
            val x = GlobalScope.launch {
                update(event.lat, event.lon, event.api)
            }
            x.invokeOnCompletion {
                fire(WeatherLoaded(data))
            }
        }
    }

    fun getApisNames() = apis.map { it.name }

    private suspend fun update(lat: Float, lon: Float, apiName: String) {
        data = try {
            Result.success(
                apis.first { it.name == apiName }
                    .run { get(Coordinates(lat, lon), client) }
            )
        } catch (e: Throwable) {
            Result.failure(e)
        }
    }
}

