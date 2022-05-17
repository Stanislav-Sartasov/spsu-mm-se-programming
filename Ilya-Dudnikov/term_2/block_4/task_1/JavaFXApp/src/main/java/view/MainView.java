package view;

import controller.StateController;
import javafx.geometry.Pos;
import javafx.scene.SnapshotParameters;
import javafx.scene.control.Button;
import javafx.scene.control.ContentDisplay;
import javafx.scene.control.Label;
import javafx.scene.control.Tooltip;
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

	private Map<String, WeatherView> weatherViewMap;
	private Map<String, StateController> controllerMap;

	public MainView() {
		setMaxWidth(600);
		setMaxHeight(500);
		getStylesheets().add("styles.css");

		setBackground(Background.EMPTY);

		weatherViewMap = new HashMap<>();
		controllerMap = new HashMap<>();

		ImageView imageView = new ImageView("file:" + RESOURCES_PATH + "bg_img.jpg");
		imageView.setPreserveRatio(true);
		imageView.setFitWidth(600);

		SnapshotParameters parameters = new SnapshotParameters();
		parameters.setFill(Color.TRANSPARENT);
		WritableImage image = imageView.snapshot(parameters, null);

		Button refreshButton = new Button();
		refreshButton.setGraphic(new ImageView("file:" + RESOURCES_PATH + "refresh.png"));
		refreshButton.setContentDisplay(ContentDisplay.GRAPHIC_ONLY);
		refreshButton.getStyleClass().add("button");
		refreshButton.setTooltip(new Tooltip("Refresh"));

		imageView.setImage(image);

		getChildren().add(imageView);
		setRight(refreshButton);
		setAlignment(refreshButton, Pos.TOP_RIGHT);
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
	}
}
