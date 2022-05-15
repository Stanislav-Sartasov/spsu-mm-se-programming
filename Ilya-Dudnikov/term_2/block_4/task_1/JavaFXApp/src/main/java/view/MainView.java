package view;

import controller.StateController;
import javafx.geometry.Pos;
import javafx.scene.SnapshotParameters;
import javafx.scene.control.Button;
import javafx.scene.control.ContentDisplay;
import javafx.scene.image.ImageView;
import javafx.scene.image.WritableImage;
import javafx.scene.layout.*;
import javafx.scene.paint.Color;
import javafx.scene.shape.Rectangle;
import javafx.scene.text.Text;

import java.util.HashMap;
import java.util.Map;

public class MainView extends BorderPane {
	private static final double SPB_LAT = 60;
	private static final double SPB_LON = 30;
	private static final String RESOURCES_PATH = "src/main/resources/";
	private static final String HOVERED_REFRESH_BUTTON_STYLE = "" +
			"-fx-background-color: rgba(43, 43, 43, .3);" +
			"-fx-background-radius: 50%;" +
			"-fx-cursor: hand";
	private static final String REFRESH_BUTTON_STYLE = "" +
			"-fx-background-color: transparent;" +
			"-fx-background-radius: 50%";
	private static final String ON_CLICK_REFRESH_BUTTON = "" +
			"-fx-background-color: rgba(43, 43, 43, .5);" +
			"-fx-background-radius: 50%;" +
			"-fx-cursor: closed_hand";
	private Rectangle rectangle;

	private Map<String, WeatherView> weatherViewMap;
	private Map<String, StateController> controllerMap;

	public MainView() {
		setMaxWidth(600);
		setMaxHeight(500);

		setBackground(Background.EMPTY);

		weatherViewMap = new HashMap<>();
		controllerMap = new HashMap<>();

		ImageView imageView = new ImageView("file:" + RESOURCES_PATH + "bg_img.jpg");
		imageView.setPreserveRatio(true);
		imageView.setFitWidth(600);

		Rectangle clip = new Rectangle(
				imageView.getFitWidth(), 500
		);
		clip.setArcWidth(15);
		clip.setArcHeight(15);
		imageView.setClip(clip);

		SnapshotParameters parameters = new SnapshotParameters();
		parameters.setFill(Color.TRANSPARENT);
		WritableImage image = imageView.snapshot(parameters, null);

		imageView.setClip(null);
		Button refreshButton = new Button();
		refreshButton.setGraphic(new ImageView("file:" + RESOURCES_PATH + "refresh.png"));
		refreshButton.setContentDisplay(ContentDisplay.GRAPHIC_ONLY);
		refreshButton.setStyle(REFRESH_BUTTON_STYLE);
		refreshButton.setOnMouseEntered(event -> refreshButton.setStyle(HOVERED_REFRESH_BUTTON_STYLE));
		refreshButton.setOnMouseExited(event -> refreshButton.setStyle(REFRESH_BUTTON_STYLE));
		refreshButton.setOnMousePressed(event -> refreshButton.setStyle(ON_CLICK_REFRESH_BUTTON));
		refreshButton.setOnMouseReleased(event -> refreshButton.setStyle(HOVERED_REFRESH_BUTTON_STYLE));

		imageView.setImage(image);

		getChildren().add(imageView);
		setRight(refreshButton);
	}

	public void addService(String service, WeatherView view, StateController controller) {
		weatherViewMap.put(service, view);
		controllerMap.put(service, controller);
	}

	public void updateDataFromService(String service) {
		controllerMap.get(service).updateState(SPB_LON, SPB_LAT);
	}

	public void outputDataFromService(String service) {
		weatherViewMap.get(service).outputData();
		setCenter(weatherViewMap.get(service));
		// getChildren().add(weatherViewMap.get(service));
	}
}
