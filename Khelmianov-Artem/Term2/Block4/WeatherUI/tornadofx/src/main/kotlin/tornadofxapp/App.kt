package tornadofxapp

import javafx.stage.Stage
import tornadofx.App


class TornadoFXApp : App(AppView::class) {
    override fun start(stage: Stage) {
        stage.apply {
            isResizable = false
            width = 500.0
            height = 300.0
        }
        super.start(stage)
    }
}
