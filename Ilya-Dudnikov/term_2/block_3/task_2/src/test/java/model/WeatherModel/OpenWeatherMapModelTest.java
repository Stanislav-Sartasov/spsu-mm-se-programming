package model.WeatherModel;

import model.WeatherData.WeatherData;
import org.junit.jupiter.api.Test;

import java.util.HashMap;

import static org.junit.jupiter.api.Assertions.*;

class OpenWeatherMapModelTest {

	@Test
	void OpenWeatherMapModelInitializationExpectNull() {
		assertNull(new StormglassModel().data);
	}

	@Test
	void updateState() {
		WeatherModel weatherModel = new OpenWeatherMapModel();

		HashMap<String, Integer> newStats = new HashMap<>();

		newStats.put("airTemperature", 10);
		newStats.put("cloudCover", 10);
		newStats.put("humidity", 12);
		newStats.put("precipitation", 4);
		newStats.put("windDirection", 51);
		newStats.put("windSpeed", 1);

		WeatherModel expectedModel = new OpenWeatherMapModel();
		expectedModel.data = new WeatherData(
				10,
				10,
				12,
				4,
				51,
				1
		);
		weatherModel.updateState(newStats);

		assertEquals(expectedModel, weatherModel);
	}
}