package meteo.app

import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.serialization.ExperimentalSerializationApi
import kotlinx.serialization.json.Json
import meteo.app.presentation.MeteoCliApp
import meteo.app.presentation.MeteoCliView
import meteo.domain.MeteoInteractorImpl
import meteo.domain.entity.Location
import meteo.openweather.data.OpenWeatherApi
import meteo.openweather.data.OpenWeatherRepository
import meteo.presentation.MeteoStore
import meteo.stormglass.data.StormGlassApi
import meteo.stormglass.data.StormGlassRepository
import java.net.http.HttpClient
import kotlin.time.Duration.Companion.seconds
import kotlin.time.toJavaDuration

@OptIn(ExperimentalSerializationApi::class)
fun main() {
    val client = HttpClient.newBuilder()
        .version(HttpClient.Version.HTTP_2)
        .connectTimeout(3.seconds.toJavaDuration())
        .build()

    val json = Json {
        ignoreUnknownKeys = true
        isLenient = true
        explicitNulls = false
    }

    val spb = Location(lat = 59.9393, lon = 30.321)

    fun getEnvOrNull(name: String) = System.getenv()[name]

    val app = MeteoCliApp(
        MeteoStore(
            interactors = listOf(
                MeteoInteractorImpl(
                    serviceName = "Open Weather",
                    repository = OpenWeatherRepository(
                        client = client,
                        api = OpenWeatherApi,
                        key = getEnvOrNull(name = "openweather.key") ?: "NOT_FOUND",
                        json = json
                    )
                ),
                MeteoInteractorImpl(
                    serviceName = "Storm Glass",
                    repository = StormGlassRepository(
                        client = client,
                        api = StormGlassApi,
                        key = getEnvOrNull(name = "stormglass.key") ?: "NOT_FOUND",
                        json = json
                    )
                )
            ),
            location = spb,
            scope = CoroutineScope(Dispatchers.IO)
        ),
        MeteoCliView(
            outputStream = System.out,
            errorStream = System.err
        ),
        inputStream = System.`in`,
        outputStream = System.out,
    )


    app.run()
}
