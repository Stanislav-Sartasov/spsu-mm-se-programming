package model.WeatherModel;

import model.WeatherData.WeatherData;

import java.util.Map;
import java.util.Objects;

public abstract class WeatherModel {
	protected WeatherData data;

	public WeatherModel() {
		this.data = null;
	}

	public abstract void updateState(Map<String, Double> newStats);

	@Override
	public boolean equals(Object o) {
		if (this == o) return true;
		if (o == null || getClass() != o.getClass()) return false;
		WeatherModel that = (WeatherModel) o;
		return Objects.equals(data, that.data);
	}

	@Override
	public int hashCode() {
		return Objects.hash(data);
	}

	public WeatherData getData() {
		return data;
	}
}
