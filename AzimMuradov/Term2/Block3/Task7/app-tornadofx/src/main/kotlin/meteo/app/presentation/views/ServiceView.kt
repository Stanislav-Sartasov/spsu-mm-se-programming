package meteo.app.presentation.views

import javafx.scene.Parent
import javafx.scene.layout.Priority
import meteo.app.presentation.MeteoTornadoFxMessagesWizard.loadingErrorMessage
import meteo.app.presentation.MeteoTornadoFxMessagesWizard.toHumanReadable
import meteo.app.presentation.MeteoTornadoFxMessagesWizard.type
import meteo.domain.entity.Weather
import meteo.presentation.state.LoadingState
import meteo.presentation.state.NamedValue
import tornadofx.*

class ServiceView : Fragment() {

    val state: NamedValue<LoadingState<Weather>> by param()

    override val root: Parent = vbox {
        val (serviceName, loadingState) = state

        this += find<DataRowView>(
            DataRowView::state to NamedValue(name = serviceName, value = loadingState.type),
            DataRowView::fontSize to 15.0
        )
        spacer { maxHeight = 10.0 }
        separator()
        spacer { maxHeight = 10.0 }
        vbox {
            when (loadingState) {
                LoadingState.Loading -> Unit
                is LoadingState.Success -> {
                    val data = loadingState.value.toHumanReadable()

                    this += find<DataRowView>(DataRowView::state to data.first())
                    data.subList(fromIndex = 1, toIndex = data.size).forEach {
                        spacer { minHeight = 5.0 }
                        this += find<DataRowView>(DataRowView::state to it)
                    }
                }
                is LoadingState.Error -> {
                    this += find<DataRowView>(
                        DataRowView::state to NamedValue(name = loadingErrorMessage(loadingState), value = "")
                    )
                }
            }
        }
        hgrow = Priority.ALWAYS
        minHeight = 250.0
    }
}
