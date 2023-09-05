package model.WeatherService;

import model.WeatherService.IWeatherAPIRequest;

import java.net.URI;
import java.net.http.HttpRequest;
import java.time.Duration;

public class OpenWeatherMap implements IWeatherAPIRequest {
	private static final String OPENWEATHERMAP_URL = "https://api.openweathermap.org/data/2.5/weather";

	@Override
	public HttpRequest createRequest(double lat, double lon) {
		return HttpRequest.newBuilder()
				.GET()
				.uri(URI.create(
						OPENWEATHERMAP_URL
								+ "?lat=" + lat
								+ "&lon=" + lon
								+ "&appid=" + System.getenv("OPENWEATHERMAP_API_KEY")
				))
				.timeout(Duration.ofSeconds(10))
				.build();
	}
}