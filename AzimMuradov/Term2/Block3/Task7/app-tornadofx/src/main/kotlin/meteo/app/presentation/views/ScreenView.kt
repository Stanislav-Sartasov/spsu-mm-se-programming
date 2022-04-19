package meteo.app.presentation.views

import javafx.scene.Parent
import javafx.scene.layout.Priority
import meteo.app.presentation.MeteoTornadoFxMessagesWizard.METEO
import meteo.app.presentation.MeteoTornadoFxStore
import meteo.presentation.wish.MeteoWish
import org.kodein.di.instance
import org.kodein.di.tornadofx.kodeinDI
import tornadofx.*

class ScreenView : View(title = METEO) {

    private val store: MeteoTornadoFxStore by kodeinDI().instance()

    override val root: Parent = vbox {
        this += find<HeaderView>(HeaderView::onLoad to { store.consume(MeteoWish.Load) })
        spacer { maxHeight = 10.0 }
        separator()
        spacer(Priority.ALWAYS)

        store.state.addListener { _, _, newValue ->
            clear()
            this += find<HeaderView>(HeaderView::onLoad to { store.consume(MeteoWish.Load) })
            spacer { maxHeight = 10.0 }
            separator()
            spacer(Priority.ALWAYS)
            this += find<ContentView>(ContentView::state to newValue)
        }

        store.consume(MeteoWish.Load)

        padding = insets(all = 40.0)
        hgrow = Priority.ALWAYS
        vgrow = Priority.ALWAYS
    }
}
