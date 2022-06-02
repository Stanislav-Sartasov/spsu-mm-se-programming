package style

import javafx.scene.paint.Color
import tornadofx.*

class Styles : Stylesheet() {
	companion object {
		private val jbmono = loadFont("/font/jb-mono-regular.ttf", 14)
	}

	init {
		root {
			jbmono?.let { font = it }
			fontSize = 14.px
			baseColor = Color.WHITE
		}
	}
}