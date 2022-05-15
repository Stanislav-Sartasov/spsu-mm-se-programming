package view.dataView;

import javafx.geometry.Pos;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.BorderPane;
import javafx.scene.layout.Pane;
import javafx.scene.layout.VBox;
import javafx.scene.paint.Color;
import javafx.scene.text.Text;
import model.WeatherData.WeatherData;

public class Temperature extends BorderPane {
	private enum metricsToggle {
		TEMPERATURE_CELSIUS,
		TEMPERATURE_FAHRENHEIT;
	};

	private Double temperatureC;
	private Double temperatureF;

	private metricsToggle currentMetric;

	public Temperature(WeatherData data) {
		temperatureC = data.airTemperatureC();
		temperatureF = data.airTemperatureF();
		currentMetric = metricsToggle.TEMPERATURE_CELSIUS;
	}

	public void outputData() {
		Text text = new Text();
		if (temperatureC != null)
			text.setText(temperatureC + "\u00B0C");
		text.setOnMouseClicked(event -> {
			if (currentMetric == metricsToggle.TEMPERATURE_CELSIUS) {
				currentMetric = metricsToggle.TEMPERATURE_FAHRENHEIT;
				text.setText(temperatureF + "\u00B0F");
			} else {
				currentMetric = metricsToggle.TEMPERATURE_CELSIUS;
				text.setText(temperatureC + "\u00B0C");
			}
		});
		text.setStyle(
				"-fx-font-size: 30pt;" +
				"-fx-font-family: 'Roboto Light'"
		);
		text.setFill(Color.WHITE);

		setLeft(text);
		setAlignment(text, Pos.CENTER);
	}
}
