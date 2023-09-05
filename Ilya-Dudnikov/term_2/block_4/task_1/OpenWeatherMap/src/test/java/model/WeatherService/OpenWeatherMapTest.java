package model.WeatherService;

import org.junit.jupiter.api.Test;

import java.net.URI;
import java.net.http.HttpRequest;

import static org.junit.jupiter.api.Assertions.*;

class OpenWeatherMapTest {

	@Test
	void createRequest() {
		double lat = 60;
		double lon = 30;

		OpenWeatherMap requestCreator = new OpenWeatherMap();

		HttpRequest request = requestCreator.createRequest(lat, lon);

		assertEquals("GET", request.method());
		assertEquals(
				URI.create("https://api.openweathermap.org/data/2.5/weather?lat=60.0&lon=30.0&appid=" + System.getenv("OPENWEATHERMAP_API_KEY")),
				request.uri()
		);
	}
}