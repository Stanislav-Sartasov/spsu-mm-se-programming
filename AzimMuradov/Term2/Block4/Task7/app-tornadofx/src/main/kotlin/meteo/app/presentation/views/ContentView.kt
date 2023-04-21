package meteo.app.presentation.views

import javafx.scene.Parent
import javafx.scene.layout.Priority
import meteo.presentation.state.MeteoState
import tornadofx.*

class ContentView : Fragment() {

    val state: MeteoState by param()

    override val root: Parent = vbox {
        when (val state = state) {
            MeteoState.Uninitialised -> Unit
            is MeteoState.Initialised -> {
                val weatherList = state.weatherList
                if (weatherList.isNotEmpty()) {
                    this += find<ServiceView>(ServiceView::state to weatherList.first())
                    weatherList.subList(fromIndex = 1, toIndex = weatherList.size).forEach {
                        spacer { minHeight = 20.0 }
                        this += find<ServiceView>(ServiceView::state to it)
                    }
                }
            }
        }
        hgrow = Priority.ALWAYS
    }
}
