package model.WeatherService;

import model.WeatherService.IWeatherAPIRequest;

import java.net.URI;
import java.net.http.HttpRequest;
import java.time.Duration;
import java.time.Instant;
import java.util.ArrayList;

public class StormglassWeather implements IWeatherAPIRequest {
	private static final String STORMGLASS_URL = "https://api.stormglass.io/v2/weather/point";

	private final ArrayList<String> params;

	public StormglassWeather(ArrayList<String> params) {
		this.params = params;
	}

	@Override
	public HttpRequest createRequest(double lat, double lon) {
		final int DATE_LENGTH = 10;

		return HttpRequest.newBuilder()
				.GET()
				.header("Authorization", System.getenv("STORMGLASS_API_KEY"))
				.uri(URI.create(
						STORMGLASS_URL
								+ "?lat=" + lat
								+ "&lng=" + lon
								+ "&params=" + String.join(",", params)
								+ "&start=" + Instant.now().toString().substring(0, DATE_LENGTH)
								+ "&end=" + Instant.now().toString().substring(0, DATE_LENGTH)
				))
				.timeout(Duration.ofSeconds(10))
				.build();
	}
}
