package meteo.app.presentation

import javafx.beans.property.SimpleObjectProperty
import kotlinx.coroutines.*
import kotlinx.coroutines.javafx.JavaFx
import meteo.app.Service
import meteo.app.kodeinDI
import meteo.presentation.mvi.MviStore
import meteo.presentation.state.MeteoState
import meteo.presentation.wish.MeteoWish
import org.kodein.di.direct
import org.kodein.di.erasedList
import org.kodein.type.erased
import tornadofx.*
import kotlin.reflect.KClass

class MeteoTornadoFxStore : ViewModel() {

    init {
        FX.dicontainer = object : DIContainer {
            override fun <T : Any> getInstance(type: KClass<T>): T = kodeinDI.direct.Instance(
                argType = erasedList(),
                type = erased(type),
                arg = Service.values().toList()
            )
        }
    }

    private val store: MviStore<MeteoWish, MeteoState> by di()

    val state = SimpleObjectProperty<MeteoState>(MeteoState.Uninitialised)

    fun consume(wish: MeteoWish) = store.consume(wish)

    init {
        CoroutineScope(Dispatchers.JavaFx).launch {
            store.state.collect {
                state.value = it
            }
        }
    }
}
