package tornadofxapp

import tornadofx.*


class AppView : View() {
    override val root = form {
        vbox(5) {
            this += find(Controls::class)
            separator()
            this += find(WeatherView::class)
        }
    }
}

