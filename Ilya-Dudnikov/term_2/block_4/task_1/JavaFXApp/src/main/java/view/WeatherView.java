package view;

import javafx.collections.FXCollections;
import javafx.geometry.Pos;
import javafx.scene.Group;
import javafx.scene.control.Label;
import javafx.scene.image.ImageView;
import javafx.scene.layout.BorderPane;
import javafx.scene.layout.HBox;
import javafx.scene.paint.Color;
import model.WeatherData.WeatherData;
import model.WeatherModel.WeatherModel;
import view.dataView.DataView;
import view.dataView.Metrics;
import view.dataView.Temperature;

import java.util.ArrayList;

public class WeatherView extends BorderPane {
	private static final String RESOURCES_PATH = "src/main/resources/";

	protected WeatherModel weatherModel;

	public WeatherView(WeatherModel weatherModel) {
		this.weatherModel = weatherModel;
	}

	private ImageView chooseWeatherIcon(WeatherData data) {
		String path = "file:" + RESOURCES_PATH;

		if (data.precipitation() != null && data.precipitation() >= 0.2)
			return new ImageView(path + "rainy.png");

		if (data.cloudCover() == null || data.cloudCover() < 30) {
			if (data.airTemperatureC() == null)
				path += "fallback_weather.png";
			else if (data.airTemperatureC() > 0)
				path += "sunny.png";
			else
				path += "cold.png";
		} else if (data.cloudCover() < 70) {
			path += "cloudyWithSun.png";
		} else {
			path += "fullCloudy.png";
		}

		return new ImageView(path);
	}

	private void drawWeatherStatus(ImageView icon) {
		Label label = new Label();
		label.setGraphic(icon);;

		HBox hBox = new HBox();
		hBox.getChildren().addAll(label, getCenter());
		hBox.setAlignment(Pos.CENTER);
		hBox.setSpacing(5.);
		setCenter(hBox);
	}

	private void unavailableDataMessage() {
		Label label = new Label("Data unavailable, try again soon");
		label.getStyleClass().add("error_text");
		setCenter(label);
	}
	public void outputData() {
		var data = weatherModel.getData();
		if (data == null || data.isNull()) {
			unavailableDataMessage();
			return;
		}

		Temperature temperature = new Temperature(data);

		setCenter(temperature);
		temperature.outputData();
		drawWeatherStatus(chooseWeatherIcon(data));

		ArrayList<DataView> dataList = new ArrayList<>() {{
			add(new DataView("Cloud Cover", data.cloudCover(), RESOURCES_PATH + "cloudCover.png", Metrics.PERCENT));
			add(new DataView("Humidity", data.humidity(), RESOURCES_PATH + "humidity.png", Metrics.PERCENT));
			add(new DataView("Precipitation", data.precipitation(), RESOURCES_PATH + "precipitation.png", Metrics.MM));
			add(new DataView("Wind Direction", data.windDirection(), RESOURCES_PATH + "windDirection.png", Metrics.DEGREES));
			add(new DataView("Wind Speed", data.windSpeed(), RESOURCES_PATH + "windSpeed.png", Metrics.M_S));
		}};

		dataList.forEach(DataView::outputData);

		HBox hBox = new HBox();
		hBox.getChildren().addAll(dataList);
		setTop(hBox);
	}
}
