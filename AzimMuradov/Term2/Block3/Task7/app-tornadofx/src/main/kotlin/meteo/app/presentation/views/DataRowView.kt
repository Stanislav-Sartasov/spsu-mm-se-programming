package meteo.app.presentation.views

import javafx.scene.Parent
import javafx.scene.layout.Priority
import javafx.scene.text.Font
import meteo.presentation.state.NamedValue
import tornadofx.*

class DataRowView : Fragment() {

    val state: NamedValue<String> by param()

    val fontSize: Double by param(defaultValue = 13.0)

    override val root: Parent = gridpane {
        val (name, value) = state
        row {
            label(text = name) {
                gridpaneConstraints { hGrow = Priority.ALWAYS }
                font = Font.font(fontSize)
            }
            label(text = value) {
                font = Font.font(fontSize)
            }
        }
        hgrow = Priority.ALWAYS
    }
}
