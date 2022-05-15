package view;

import javafx.geometry.Pos;
import javafx.scene.layout.*;
import javafx.scene.paint.Color;
import javafx.scene.text.Text;
import model.WeatherData.WeatherData;
import model.WeatherModel.WeatherModel;
import view.dataView.Temperature;

public class WeatherView extends BorderPane {
	protected WeatherModel weatherModel;

	public WeatherView(WeatherModel weatherModel) {
		this.weatherModel = weatherModel;
	}

	public void outputData() {
		// var data = weatherModel.getData();
		var data = new WeatherData(10., 15., 10., 10., 10., 10., 10.);

		Temperature temperature = new Temperature(data);
		setLeft(temperature);
		temperature.outputData();
	}
}
