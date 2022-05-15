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
				620,
				520,
				Color.rgb(43, 43, 43))
		);

		pane.setBackground(Background.EMPTY);

		MainView mainView = new MainView();
		pane.setCenter(mainView);

		WeatherView weatherView = (WeatherView) context.getBean("weatherView");

		StateController controller = (StateController) context.getBean("openWeatherMapStateController");

		mainView.addService("OpenWeatherMap", weatherView, controller);
		mainView.outputDataFromService("OpenWeatherMap");

		primaryStage.show();
	}
}