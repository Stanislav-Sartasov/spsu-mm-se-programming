package meteo.app.presentation

import javafx.stage.Stage
import meteo.app.presentation.views.ScreenView
import tornadofx.*

class MeteoTornadoFxApp : App(primaryView = ScreenView::class) {

    override fun start(stage: Stage) {
        super.start(stage)

        stage.run {
            width = 800.0
            height = 800.0
            isResizable = false
            centerOnScreen()
        }
    }
}
