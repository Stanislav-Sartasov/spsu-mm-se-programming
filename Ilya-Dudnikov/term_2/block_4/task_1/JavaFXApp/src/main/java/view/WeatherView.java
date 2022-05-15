package view;

import javafx.collections.FXCollections;
import javafx.geometry.Pos;
import javafx.scene.Group;
import javafx.scene.control.Label;
import javafx.scene.image.ImageView;
import javafx.scene.layout.BorderPane;
import javafx.scene.layout.HBox;
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

	private void drawWeatherStatus(WeatherData data) {
		ImageView imageView = new ImageView("file:" + RESOURCES_PATH + "sunny.png");
		Label label = new Label();
		label.setGraphic(imageView);

		HBox hBox = new HBox();
		hBox.getChildren().addAll(label, getCenter());
		hBox.setFillHeight(true);
		hBox.setAlignment(Pos.CENTER);
		hBox.setSpacing(5.);
		setCenter(hBox);
	}

	public void outputData() {
		// var data = weatherModel.getData();
		var data = new WeatherData(10., 15., 11., 12., 13., 14., 10.);

		Temperature temperature = new Temperature(data);

		setCenter(temperature);
		temperature.outputData();
		drawWeatherStatus(data);

		ArrayList<DataView> dataList = new ArrayList<>() {{
			add(new DataView("cloudCover", data.cloudCover(), RESOURCES_PATH + "cloudCover.png", Metrics.PERCENT));
			add(new DataView("humidity", data.humidity(), RESOURCES_PATH + "humidity.png", Metrics.PERCENT));
			add(new DataView("precipitation", data.precipitation(), RESOURCES_PATH + "precipitation.png", Metrics.PERCENT));
			add(new DataView("windDirection", data.windDirection(), RESOURCES_PATH + "windDirection.png", Metrics.DEGREES));
			add(new DataView("windSpeed", data.windSpeed(), RESOURCES_PATH + "windSpeed.png", Metrics.MM_S));
		}};

		dataList.forEach(DataView::outputData);

		HBox hBox = new HBox();
		hBox.getChildren().addAll(dataList);
		setTop(hBox);
	}
}
