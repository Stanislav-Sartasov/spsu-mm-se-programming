package meteo.app

import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.serialization.ExperimentalSerializationApi
import kotlinx.serialization.json.Json
import meteo.app.presentation.MeteoComposeApp
import meteo.domain.MeteoInteractor
import meteo.domain.MeteoInteractorImpl
import meteo.domain.entity.Location
import meteo.openweather.data.OpenWeatherApi
import meteo.openweather.data.OpenWeatherRepository
import meteo.presentation.MeteoStore
import meteo.presentation.mvi.MviStore
import meteo.presentation.state.MeteoState
import meteo.presentation.wish.MeteoWish
import meteo.stormglass.data.StormGlassApi
import meteo.stormglass.data.StormGlassRepository
import org.kodein.di.*
import java.net.http.HttpClient
import kotlin.time.Duration.Companion.seconds
import kotlin.time.toJavaDuration

@OptIn(ExperimentalSerializationApi::class)
object KodeinModules {

    val mainModule = DI.Module(name = "main") {
        import(presentationModule)
        import(interactorModule)
        import(repositoryModule)
        import(apiModule)
        import(networkModule)
        import(serializationModule)
    }


    private val presentationModule = DI.Module(name = "presentation") {
        bindFactory { services: List<Service> ->
            MeteoComposeApp(
                store = instance(arg = services),
            )
        }

        bindFactory<List<Service>, MviStore<MeteoWish, MeteoState>> { services: List<Service> ->
            MeteoStore(
                interactors = services.map { instance(arg = it) },
                location = Location(lat = 59.9393, lon = 30.321),
                scope = CoroutineScope(Dispatchers.IO)
            )
        }
    }

    private val interactorModule = DI.Module(name = "interactor") {
        bindFactory<Service, MeteoInteractor> { service: Service ->
            MeteoInteractorImpl(
                serviceName = service.serviceName,
                repository = instance(arg = service)
            )
        }
    }

    private val repositoryModule = DI.Module(name = "repository") {
        bindMultiton { service: Service ->
            when (service) {
                Service.OpenWeather -> instance<OpenWeatherRepository>()
                Service.StormGlass -> instance<StormGlassRepository>()
            }
        }

        bindSingleton {
            OpenWeatherRepository(
                client = instance(),
                api = instance(arg = Service.OpenWeather),
                key = getEnvOrNull(name = "openweather.key") ?: "NOT_FOUND",
                json = instance()
            )
        }

        bindSingleton {
            StormGlassRepository(
                client = instance(),
                api = instance(arg = Service.StormGlass),
                key = getEnvOrNull(name = "stormglass.key") ?: "NOT_FOUND",
                json = instance()
            )
        }
    }

    private val apiModule = DI.Module(name = "api") {
        bindMultiton { service: Service ->
            when (service) {
                Service.OpenWeather -> instance<OpenWeatherApi>()
                Service.StormGlass -> instance<StormGlassApi>()
            }
        }

        bindSingleton {
            OpenWeatherApi
        }

        bindSingleton {
            StormGlassApi
        }
    }


    private val networkModule = DI.Module(name = "network") {
        bindSingleton {
            HttpClient.newBuilder()
                .version(HttpClient.Version.HTTP_2)
                .connectTimeout(3.seconds.toJavaDuration())
                .build()
        }
    }

    private val serializationModule = DI.Module(name = "serialization") {
        bindSingleton {
            Json {
                ignoreUnknownKeys = true
                isLenient = true
                explicitNulls = false
            }
        }
    }


    private fun getEnvOrNull(name: String) = System.getenv()[name]
}
