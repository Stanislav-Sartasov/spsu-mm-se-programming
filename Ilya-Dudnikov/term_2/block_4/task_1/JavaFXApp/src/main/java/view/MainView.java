package view;

import controller.StateController;
import javafx.geometry.Pos;
import javafx.scene.control.*;
import javafx.scene.image.ImageView;
import javafx.scene.layout.*;

import java.util.HashMap;
import java.util.Map;

public class MainView extends BorderPane {
	private static final double SPB_LAT = 60;
	private static final double SPB_LON = 30;
	private static final String RESOURCES_PATH = "src/main/resources/";

	private Map<String, WeatherView> weatherViewMap;
	private Map<String, StateController> controllerMap;
	private ServiceSelector menuBar;

	public MainView() {
		getStylesheets().add("styles.css");

		weatherViewMap = new HashMap<>();
		controllerMap = new HashMap<>();

		// Background image
		ImageView imageView = new ImageView("file:" + RESOURCES_PATH + "bg_img.jpg");
		imageView.setPreserveRatio(true);
		imageView.setFitWidth(600);
		getChildren().add(imageView);

		// Menu bar
		menuBar = new ServiceSelector();
		setTop(menuBar);

		// Refresh button
		Button refreshButton = new Button();
		refreshButton.setGraphic(new ImageView("file:" + RESOURCES_PATH + "refresh.png"));
		refreshButton.setContentDisplay(ContentDisplay.GRAPHIC_ONLY);
		refreshButton.getStyleClass().add("button");
		refreshButton.setTooltip(new Tooltip("Refresh"));

		refreshButton.setOnMouseClicked(event -> {
			updateDataFromService(menuBar.getSelectedService());
			outputDataFromService(menuBar.getSelectedService());
		});

		setRight(refreshButton);
		setAlignment(refreshButton, Pos.TOP_RIGHT);
	}

	public void addService(String service, WeatherView view, StateController controller) {
		weatherViewMap.put(service, view);
		controllerMap.put(service, controller);
		menuBar.addService(service);

		if (weatherViewMap.size() == 1) {
			updateDataFromService(service);
			outputDataFromService(service);
		}
	}

	public void updateDataFromService(String service) {
		controllerMap.get(service).updateState(SPB_LON, SPB_LAT);
	}

	public void outputDataFromService(String service) {
		weatherViewMap.get(service).outputData();
		setCenter(weatherViewMap.get(service));
	}
}
