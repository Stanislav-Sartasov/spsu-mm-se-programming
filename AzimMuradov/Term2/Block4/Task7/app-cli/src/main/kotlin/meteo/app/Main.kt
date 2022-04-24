package meteo.app

import meteo.app.presentation.MeteoCliApp
import org.kodein.di.DI
import org.kodein.di.instance

fun main(args: Array<String>) {
    val kodeinDI = DI {
        import(KodeinModules.mainModule)
    }

    val app: MeteoCliApp by kodeinDI.instance(arg = parseArgs(args))

    app.run()
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
