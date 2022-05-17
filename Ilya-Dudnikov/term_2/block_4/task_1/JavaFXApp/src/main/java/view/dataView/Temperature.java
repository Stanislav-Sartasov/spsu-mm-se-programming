package view.dataView;

import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.Cursor;
import javafx.scene.control.Label;
import javafx.scene.control.Tooltip;
import javafx.scene.layout.BorderPane;
import javafx.scene.paint.Color;
import javafx.scene.text.Text;
import model.WeatherData.WeatherData;

import java.text.DecimalFormat;

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
		Label label = new Label();

		if (temperatureC == null)
			return;

		label.setText(new DecimalFormat("#.##").format(temperatureC) + "\u00B0C");

		Tooltip tooltip = new Tooltip("Click here to see temperature in " + Metrics.DEGREES_FAHRENHEIT);
		tooltip.setStyle("-fx-font-size: 0.4em");
		label.setTooltip(tooltip);

		label.setOnMouseClicked(event -> {
			tooltip.setText("Click here to see temperature in " + currentMetric);
			if (currentMetric == Metrics.DEGREES_CELSIUS) {
				currentMetric = Metrics.DEGREES_FAHRENHEIT;
				label.setText(new DecimalFormat("#.##").format(temperatureF) + "\u00B0F");
			} else {
				currentMetric = Metrics.DEGREES_CELSIUS;
				label.setText(new DecimalFormat("#.##").format(temperatureC) + "\u00B0C");
			}
		});
		label.getStyleClass().add("temperature");
		setMargin(label, new Insets(0, 0, 0, 10.));

		setCenter(label);
		setAlignment(label, Pos.CENTER);
	}

	private Metrics changedMetric() {
		if (currentMetric == Metrics.DEGREES_CELSIUS)
			return Metrics.DEGREES_FAHRENHEIT;
		return Metrics.DEGREES_CELSIUS;
	}
}
