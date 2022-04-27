package view;

import model.WeatherModel.WeatherModel;

public class StormglassView extends WeatherView {
	public StormglassView(WeatherModel weatherModel) {
		super(weatherModel);
	}

	public void outputData() {
		System.out.println("Stormglass Weather");
		super.outputData();
	}
}
