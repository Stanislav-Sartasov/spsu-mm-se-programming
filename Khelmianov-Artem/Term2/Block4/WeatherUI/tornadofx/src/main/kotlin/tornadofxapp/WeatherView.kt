package tornadofxapp

import javafx.application.Platform
import javafx.scene.layout.Priority
import lib.weather.format
import tornadofx.*

class WeatherView : View("WeatherView") {
    override val root = gridpane {
        subscribe<WeatherLoaded> { result ->
            Platform.runLater {
                this@gridpane.clear()
                if (result.isSuccess) {
                    for (line in result.data.format().split("\n")) {
                        row {
                            label(line.split(":")[0].trim()) {
                                gridpaneConstraints { hGrow = Priority.ALWAYS }
                            }
                            label(line.split(":")[1].trim())
                        }
                    }
                } else {
                    row {
                        label("Error occurred:")
                    }
                    row {
                        label(result.result.exceptionOrNull()!!.message!!) {
                            isWrapText = true
                            textFill = c("red")
                        }
                    }
                }
            }
        }
    }
}
