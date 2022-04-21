package meteo.app.presentation

import javafx.stage.Stage
import meteo.app.KodeinModules
import meteo.app.presentation.views.ScreenView
import org.kodein.di.DI
import org.kodein.di.DIAware
import org.kodein.di.tornadofx.installTornadoSource
import tornadofx.*

class MeteoTornadoFxApp : App(primaryView = ScreenView::class), DIAware {

    override val di: DI
        get() = DI {
            installTornadoSource()
            import(KodeinModules.mainModule)
        }

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
