package model.WeatherService.OpenWeatherMap;

import org.junit.jupiter.api.Test;

import java.net.URI;
import java.net.http.HttpHeaders;
import java.net.http.HttpRequest;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;

import static org.junit.jupiter.api.Assertions.*;

class OpenWeatherMapTest {

	@Test
	void createRequest() {
		double lat = 60;
		double lon = 30;
		String key = "hahahahaPutinPutin";

		OpenWeatherMap requestCreator = new OpenWeatherMap();

		HttpRequest request = requestCreator.createRequest(lat, lon, key);

		assertEquals("GET", request.method());
		assertEquals(
				URI.create("https://api.openweathermap.org/data/2.5/weather?lat=60.0&lon=30.0&appid=" + key),
				request.uri()
		);
	}
}