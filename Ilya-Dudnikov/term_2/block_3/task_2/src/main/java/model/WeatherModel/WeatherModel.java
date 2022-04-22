package model.WeatherModel;

import model.WeatherData.WeatherData;

import java.util.Map;

public abstract class WeatherModel {
	protected WeatherData data;

	public WeatherModel() {
		this.data = null;
	}

	public abstract void updateState(Map<String, Integer> newStats);
}
