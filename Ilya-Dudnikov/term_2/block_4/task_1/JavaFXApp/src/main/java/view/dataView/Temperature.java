package view.dataView;

import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.layout.BorderPane;
import javafx.scene.paint.Color;
import javafx.scene.text.Text;
import model.WeatherData.WeatherData;

public class Temperature extends BorderPane {
	private Double temperatureC;
	private Double temperatureF;

	private Metrics currentMetric;

	public Temperature(WeatherData data) {
		temperatureC = data.airTemperatureC();
		temperatureF = data.airTemperatureF();
		currentMetric = Metrics.DEGREES_CELSIUS;
	}

	public void outputData() {
		Text text = new Text();
		if (temperatureC != null)
			text.setText(temperatureC + "\u00B0C");
		text.setOnMouseClicked(event -> {
			if (currentMetric == Metrics.DEGREES_CELSIUS) {
				currentMetric = Metrics.DEGREES_FAHRENHEIT;
				text.setText(temperatureF + "\u00B0F");
			} else {
				currentMetric = Metrics.DEGREES_CELSIUS;
				text.setText(temperatureC + "\u00B0C");
			}
		});
		text.setStyle(
				"-fx-font-size: 32pt;" +
				"-fx-font-family: 'Roboto Light'"
		);
		text.setFill(Color.WHITE);
		setMargin(text, new Insets(0, 0, 0, 10.));

		setCenter(text);
		setAlignment(text, Pos.CENTER);
	}
}
