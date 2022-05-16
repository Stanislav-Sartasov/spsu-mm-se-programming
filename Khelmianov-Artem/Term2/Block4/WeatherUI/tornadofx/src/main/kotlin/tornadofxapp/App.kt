package tornadofxapp

import javafx.stage.Stage
import tornadofx.App


class TornadoFXApp : App(AppView::class) {
    override fun start(stage: Stage) {
        super.start(stage)
        stage.run {
            isResizable = false
            width = 500.0
            height = 300.0
        }
    }
}
