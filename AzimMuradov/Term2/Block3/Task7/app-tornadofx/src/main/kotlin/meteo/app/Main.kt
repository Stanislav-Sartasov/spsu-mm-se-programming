package meteo.app

import meteo.app.presentation.MeteoTornadoFxApp
import org.kodein.di.DI
import tornadofx.*

val kodeinDI = DI {
    import(KodeinModules.mainModule)
}

fun main(args: Array<String>) {
    launch<MeteoTornadoFxApp>(args)
}

private fun parseArgs(args: Array<String>): List<Service> {
    val servicesMap = mapOf(
        "ow" to Service.OpenWeather,
        "sg" to Service.StormGlass
    )

    val resArgs = args.filter(servicesMap.keys::contains).toSet().ifEmpty {
        servicesMap.keys
    }

    return resArgs.map(servicesMap::getValue)
}
