package meteo.app.presentation

import javafx.beans.property.SimpleObjectProperty
import kotlinx.coroutines.*
import kotlinx.coroutines.javafx.JavaFx
import meteo.app.Service
import meteo.presentation.mvi.MviStore
import meteo.presentation.state.MeteoState
import meteo.presentation.wish.MeteoWish
import org.kodein.di.instance
import org.kodein.di.tornadofx.kodeinDI
import tornadofx.*

class MeteoTornadoFxStore : ViewModel() {

    private val store: MviStore<MeteoWish, MeteoState> by kodeinDI().instance(arg = parseArgs(app.parameters.raw))

    val state = SimpleObjectProperty<MeteoState>(MeteoState.Uninitialised)

    fun consume(wish: MeteoWish) = store.consume(wish)

    init {
        CoroutineScope(Dispatchers.JavaFx).launch {
            store.state.collect {
                state.value = it
            }
        }
    }


    private fun parseArgs(args: List<String>): List<Service> {
        val servicesMap = mapOf(
            "ow" to Service.OpenWeather,
            "sg" to Service.StormGlass
        )

        val resArgs = args.filter(servicesMap.keys::contains).toSet().ifEmpty {
            servicesMap.keys
        }

        return resArgs.map(servicesMap::getValue)
    }
}
