package meteo.app.presentation.views

import javafx.scene.Parent
import javafx.scene.layout.Priority
import javafx.scene.text.Font
import meteo.app.presentation.MeteoTornadoFxMessagesWizard.LOAD_BUTTON
import meteo.app.presentation.MeteoTornadoFxMessagesWizard.LOAD_BUTTON_TOOLTIP
import meteo.app.presentation.MeteoTornadoFxMessagesWizard.METEO
import tornadofx.*

class HeaderView : View() {

    val onLoad: () -> Unit by param()

    override val root: Parent = gridpane {
        row {
            label(text = METEO) {
                gridpaneConstraints { hGrow = Priority.ALWAYS }
                font = Font.font(26.0)
            }
            button(text = LOAD_BUTTON) {
                action(onLoad)
                tooltip(text = LOAD_BUTTON_TOOLTIP)
                font = Font.font(18.0)
            }
        }
        hgrow = Priority.ALWAYS
    }
}
