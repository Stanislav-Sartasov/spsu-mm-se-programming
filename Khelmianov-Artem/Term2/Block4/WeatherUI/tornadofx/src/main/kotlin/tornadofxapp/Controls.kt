package tornadofxapp

import javafx.beans.property.SimpleFloatProperty
import javafx.geometry.Orientation
import javafx.scene.control.RadioButton
import javafx.scene.control.ToggleGroup
import tornadofx.*
import java.text.NumberFormat
import java.text.ParseException
import java.util.*


class Controls : View("Controls") {
    private val controller: AppController by inject()
    private val group = ToggleGroup()
    private val formatter = NumberFormat.getInstance(Locale.getDefault())

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
                            try {
                                return@filterInput formatter.parse(it.controlNewText).toFloat() in -90f..90f
                            } catch (e: ParseException) {
                                return@filterInput false
                            }
                        }
                    }
                }
                hbox {
                    text("lon: ")
                    textfield(lonProp) {
                        filterInput {
                            try {
                                return@filterInput formatter.parse(it.controlNewText).toFloat() in -180f..180f
                            } catch (e: ParseException) {
                                return@filterInput false
                            }
                        }
                    }
                }
            }
        }
}

