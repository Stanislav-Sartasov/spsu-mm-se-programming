import cli.App
import lib.weather.IWeatherAPI
import org.koin.core.context.startKoin
import org.koin.core.logger.Level
import org.koin.dsl.module


fun configure(args: Array<String>) = module {
    fun parse(args: Array<String>) = buildList<IWeatherAPI> {
        val apis = mapOf(
            "ow" to OpenWeatherAPI,
            "wb" to WeatherbitAPI
        )
        addAll(args.filter(apis.keys::contains).mapNotNull(apis::get).ifEmpty { apis.values })
    }
    single { parse(args) }
}

fun main(args: Array<String>) {
    startKoin {
        printLogger(Level.ERROR)
        modules(configure(args))
    }

    App().run()
}
