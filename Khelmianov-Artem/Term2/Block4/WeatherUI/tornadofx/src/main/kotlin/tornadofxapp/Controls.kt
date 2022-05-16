package tornadofxapp

import javafx.beans.property.SimpleFloatProperty
import javafx.geometry.Orientation
import javafx.scene.control.RadioButton
import javafx.scene.control.ToggleGroup
import tornadofx.*


class Controls : View("Controls") {
    private val controller: AppController by inject()
    private val group = ToggleGroup()

    private var currentApi = ""
    private var latProp = SimpleFloatProperty().apply { value = 59.9f }
    private var lat by latProp
    private var lonProp = SimpleFloatProperty().apply { value = 30.3f }
    private var lon by lonProp


    override val root =
        hbox(5) {
            button(text = "Get") {
                action {
                    fire(LoadWeather(lat, lon, currentApi))
                }
            }
            separator(Orientation.VERTICAL)
            vbox(5) {
                for (name in controller.getApisNames()) {
                    radiobutton(name, group).apply {
                        action { currentApi = this.text }
                    }
                }
                group.toggles.first().apply {
                    isSelected = true
                    currentApi = (this as RadioButton).text
                }
            }
            separator(Orientation.VERTICAL)
            vbox(5) {
                hbox {
                    text("lat: ")
                    textfield(latProp) {
                        filterInput {
                            it.controlNewText.isFloat()
                            it.controlNewText.toFloat() in -90f..90f
                        }
                    }
                }
                hbox {
                    text("lon: ")
                    textfield(lonProp) {
                        filterInput {
                            it.controlNewText.isFloat()
                            it.controlNewText.toFloat() in -180f..180f
                        }
                    }
                }
            }
        }
}

