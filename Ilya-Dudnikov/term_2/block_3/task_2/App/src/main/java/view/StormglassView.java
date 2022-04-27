package view;

import controller.StateController;
import model.WeatherData.WeatherData;

public class StormglassView extends WeatherView {
	public StormglassView(WeatherData data) {
		super(data);
	}

	public void outputData() {
		System.out.println("Stormglass Weather");
		super.outputData();
	}
}
