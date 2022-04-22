package model.WeatherService.StormglassWeather;

import org.junit.jupiter.api.Test;

import java.net.URI;
import java.net.http.HttpHeaders;
import java.net.http.HttpRequest;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;

import static org.junit.jupiter.api.Assertions.*;

class StormglassWeatherTest {
	@Test
	void createRequest() {
		double lat = 60;
		double lon = 30;
		String key = "hahahahaPutinPutin";

		ArrayList<String> params = new ArrayList<>();
		params.add("airTemperature");
		params.add("cloudCover");
		params.add("humidity");
		params.add("precipitation");
		params.add("windDirection");
		params.add("windSpeed");

		StormglassWeather requestCreator = new StormglassWeather(params);

		HttpRequest request = requestCreator.createRequest(lat, lon, key);

		assertEquals("GET", request.method());
		assertEquals(
				URI.create("https://api.stormglass.io/v2/weather/point?lat=60.0&lng=30.0&params=airTemperature,cloudCover,humidity,precipitation,windDirection,windSpeed"),
				request.uri()
		);

		assertEquals(HttpHeaders.of(
				new HashMap<String, List<String>>() {{
					put("Authorization", Collections.singletonList(key));
				}}, (first, second) -> true),
				request.headers()
		);
	}
}