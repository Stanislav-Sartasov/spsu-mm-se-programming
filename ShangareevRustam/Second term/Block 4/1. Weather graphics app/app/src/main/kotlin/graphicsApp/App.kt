package graphicsApp

import graphicsApp.view.MainView
import javafx.fxml.FXMLLoader
import javafx.scene.Scene
import javafx.scene.image.Image
import javafx.scene.paint.Color
import javafx.stage.Stage
import javafx.stage.StageStyle
import tornadofx.App

class App: App(MainView::class, Styles::class) {

	override fun start(stage: Stage) {
		val loader = FXMLLoader(javaClass.getResource("/hello.fxml"))
		val scene = Scene(loader.load())

		stage.initStyle(StageStyle.DECORATED)
		scene.fill = Color.TRANSPARENT
		stage.scene = scene
		stage.isResizable = false
		stage.title = "Weather application"
		stage.icons.add(Image("appIcon.png"))

		stage.show()
	}

	override fun stop() = Unit // to disable exception handling after closing the application
}