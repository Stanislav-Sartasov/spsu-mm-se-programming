import controller.StateController;
import javafx.application.Application;
import javafx.scene.Scene;
import javafx.scene.layout.Background;
import javafx.scene.layout.BackgroundFill;
import javafx.scene.layout.BackgroundImage;
import javafx.scene.layout.BorderPane;
import javafx.scene.paint.Color;
import javafx.stage.Stage;
import org.springframework.context.annotation.AnnotationConfigApplicationContext;
import view.MainView;
import view.WeatherView;

public class JavaFXApp extends Application {
	public static void main(String[] args) {
		launch(args);
	}

	@Override
	public void start(Stage primaryStage) {
		primaryStage.setTitle("Weather");
		AnnotationConfigApplicationContext context = new AnnotationConfigApplicationContext(DIConfiguration.class);

		BorderPane pane = new BorderPane();
		primaryStage.setScene(new Scene(
				pane,
				600,
				500,
				Color.rgb(43, 43, 43))
		);
		primaryStage.setResizable(false);

		pane.setBackground(Background.EMPTY);

		MainView mainView = new MainView();
		pane.setCenter(mainView);

		var openWeatherMapView = (WeatherView) context.getBean("openWeatherMapView");
		var stormglassView = (WeatherView) context.getBean("stormglassView");

		var openWeatherMapController = (StateController) context.getBean("openWeatherMapStateController");
		var stormglassController = (StateController) context.getBean("stormglassStateController");

		mainView.addService("OpenWeatherMap", openWeatherMapView, openWeatherMapController);
		mainView.addService("Stormglass", stormglassView, stormglassController);

		primaryStage.show();
	}
}